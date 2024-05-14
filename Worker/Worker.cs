using System;
using System.Threading.Tasks;
using WorkerSpace.Interfaces;

namespace WorkerSpace
{
    internal sealed class Worker : IDisposable
    {
#pragma warning disable IDE0290
        public Worker(ITaskListDispatcher taskList) {
            Dispatcher = taskList;
        }
#pragma warning restore IDE0290

        public ITaskListDispatcher Dispatcher { get; private set; }

        public async Task<HANDLE> StartAsync(CancellationToken cancellationToken) {
            Dispatcher.Create();
            await Dispatcher.StartTasks();

            while (!cancellationToken.IsCancellationRequested) {
                foreach (TaskBaseImplement task in Dispatcher.GetTasks()) {
                    HANDLE handle = await task.ExecuteAsync();
                    if (WinAPIAssert.Fail(handle, out string error)) {
                        cancellationToken.ThrowIfCancellationRequested();
                        throw new Exception(error);
                    }
                    if (handle.HandleResult == Result.E_END)
                        await task.EndAsync();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }

            return await Task.FromResult(new HANDLE(Result.E_END));
        }

        public void Dispose()
        {
            Dispatcher.Dispose();
        }
    }
}