using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerSpace.Interfaces;

namespace WorkerSpace
{
    internal sealed class Worker : IDisposable
    {
#pragma warning disable IDE0290
        public Worker(ITaskListDispatcher taskList)
        {
            Dispatcher = taskList;
        }
#pragma warning restore IDE0290

        public ITaskListDispatcher Dispatcher { get; private set; }

        public async Task<HANDLE> StartAsync(CancellationToken cancellationToken)
        {
            Dispatcher.Create();
            await Dispatcher.StartTasks();

            TaskScheduler scheduler = TaskScheduler.Current;

            foreach (TaskBaseImplement task in Dispatcher.GetTasks())
            {
                Task executor = Task.Factory.StartNew(async () => {
                    await ExecuteTaskAsync(task);
                }, cancellationToken, TaskCreationOptions.LongRunning, scheduler);
            }

            return await Task.FromResult(new HANDLE(Result.E_END));
        }

        public async Task<HANDLE> ExecuteTaskAsync(TaskBaseImplement task)
        {
            CancellationTokenSource CurrentCancelationToken = new CancellationTokenSource();
            CancellationToken cancellationToken = CurrentCancelationToken.Token;
            while (!cancellationToken.IsCancellationRequested)
            {
                HANDLE handle = await task.ExecuteAsync();
                if (WinAPIAssert.Fail(handle, out string error))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    throw new Exception(error);
                }
                if (handle.HandleResult == Result.E_END)
                {
                    await task.EndAsync();
                    CurrentCancelationToken.Cancel();
                }

                await Task.Delay(TimeSpan.FromSeconds(task.TimeOut), cancellationToken);
            }

            return await Task.FromResult(new HANDLE(Result.E_END));
        }
        public void Dispose()
        {
            Dispatcher.Dispose();
        }
    }
}