using System;
using WorkerSpace.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using TaskImplaments.Interfaces;
using TaskImplaments;
using TaskImplaments.Tasks;

#pragma warning disable CS8618
#pragma warning disable IDE0028
#pragma warning disable CA1859

namespace WorkerSpace
{
    internal sealed class ListDispatcher : IDisposable, ITaskListDispatcher
    {
        private static List<ThreadWorker> _threadsWorkers = new List<ThreadWorker>();

        //Пока заглушка, для создания воркеров
        public async void CreateWorker() {
            ThreadWorker threadWorker = new ThreadWorker();
            await threadWorker.Start();
            _threadsWorkers.Add(threadWorker);

            IAbstractCounter abstractCounter = new RegisterCounter();
            abstractCounter.Create(4);
            TaskBaseImplement task = new PushEventMessage(abstractCounter);

            foreach (var worker in _threadsWorkers){
               await worker.CurrentWorker.ExecuteTaskAsync(task);
            }

        }

        public static async Task Create(TaskBaseImplement task){
            //Сюда прокидываем таску Пока отдаем на работу всем воркерам

            foreach (var worker in  _threadsWorkers)
            {
                await worker.CurrentWorker.ExecuteTaskAsync(task);
            }
        }

        public void Dispose()
        {
            foreach (var threadsWorker in _threadsWorkers)
                threadsWorker.Dispose();
        }

        public List<ThreadWorker> GetThreadsWorkers()
        {
            return _threadsWorkers;
        }

        public Task StartTasks()
        {
            throw new NotImplementedException();
        }
    }
}