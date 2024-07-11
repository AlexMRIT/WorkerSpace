﻿using System;
using System.Threading;
using commonlib.WinUtils;
using System.Threading.Tasks;
using commonlib.Interfaces;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace commonlib.Thread
{
    internal sealed class Worker : IDisposable
    {
        private readonly ConcurrentDictionary<IStorageId, ITask> Tasks;
        private readonly CancellationTokenSource CurrentCancelationToken;
        private readonly TaskScheduler ThreadSheduler;

        private volatile int countRegisteredTasks;

        public Worker()
        {
            Tasks = new ConcurrentDictionary<IStorageId, ITask>();
            ThreadSheduler = TaskScheduler.Current;
            CurrentCancelationToken = new CancellationTokenSource();
            countRegisteredTasks = 0;
        }

        public async Task InsertTask(IStorageId storageId, ITask task)
        {
            if (!Tasks.TryAdd(storageId, task))
                Console.WriteLine("Ошибка добавления! Возможно ключ задачи уже был определен.");
            countRegisteredTasks++;

            await task.StartAsync();
        }

        public int CountRegistered => countRegisteredTasks;

        public async Task<HANDLE> ExecuteTaskAsync()
        {
            try
            {
                Task executor = Task.Factory.StartNew(async () =>
                {
                    List<IStorageId> tasksToRemove = new List<IStorageId>(capacity: 256);
                    while (!CurrentCancelationToken.IsCancellationRequested)
                    {
                        foreach (KeyValuePair<IStorageId, ITask> task in Tasks)
                        {
                            if (!task.Value.TaskIsRunning)
                                continue;

                            HANDLE result = await task.Value.ExecuteAsync();
                            if (WinAPIAssert.Fail(result, out string message))
                            {
                                await task.Value.StopAsync();
                                CurrentCancelationToken.Token.ThrowIfCancellationRequested();
                                Console.WriteLine(message);
                            }

                            if (task.Value.IsTaskCanceled())
                            {
                                task.Value.TaskHasDelete = true;
                                tasksToRemove.Add(task.Key);
                            }
                        }

                        foreach (IStorageId id in tasksToRemove)
                        {
                            await Tasks[id].StopAsync();
                            Tasks.TryRemove(id, out _);
                            countRegisteredTasks = Math.Max(0, countRegisteredTasks--);
                        }
                        tasksToRemove.Clear();
                        await Task.Delay(TimeSpan.FromSeconds(1), CurrentCancelationToken.Token);
                    }
                }, CurrentCancelationToken.Token, TaskCreationOptions.LongRunning, ThreadSheduler);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Отмена задачи: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            return await Task.FromResult(new HANDLE(Result.E_END));
        }

        public void Dispose()
        {
            foreach (KeyValuePair<IStorageId, ITask> task in Tasks)
                task.Value.StopAsync();
            Tasks.Clear();
        }
    }
}