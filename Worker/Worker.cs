using System;
using System.Threading;
using System.Threading.Tasks;
using TaskImplaments;
using Win32Handlers;
using WorkerSpace.Interfaces;

namespace WorkerSpace
{
    internal sealed class Worker : IDisposable
    {
        private List<TaskBaseImplement> _taskList = new List<TaskBaseImplement>();

        public async Task<HANDLE> StartAsync()
        {
            Console.WriteLine("Worker is Runing");
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        public async Task<HANDLE> ExecuteTaskAsync(TaskBaseImplement task) {

            await task.StartAsync();

            _taskList.Add(task);

            CancellationTokenSource CurrentCancelationToken = new CancellationTokenSource();
            CancellationToken cancellationToken = CurrentCancelationToken.Token;

            TaskScheduler scheduler = TaskScheduler.Current;

            Task executor = Task.Factory.StartNew(async () => {
                while (!cancellationToken.IsCancellationRequested)
                {
                    HANDLE handle = await task.ExecuteAsync();
                    if (WinAPIAssert.Fail(handle, out string error))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        throw new Exception(error);

                    }
                    if (handle.TaskEnded())
                    {
                        await task.EndAsync();
                        CurrentCancelationToken.Cancel();
                    }
                    else
                        await Task.Delay(TimeSpan.FromSeconds(task.TimeOut), cancellationToken);
                }
            }, cancellationToken, TaskCreationOptions.LongRunning, scheduler);
            return await Task.FromResult(new HANDLE(Result.E_END));
        }
        public void Dispose(){
            //Тут нужно остановить все задачи воркера
        }
    }
}