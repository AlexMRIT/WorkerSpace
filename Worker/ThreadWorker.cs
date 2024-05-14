using System;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable IDE0090
#pragma warning disable CS8618

namespace WorkerSpace
{
    internal sealed class ThreadWorker : IDisposable
    {
        public Task CurrentTaskWorker { get; private set; }
        public Worker CurrentWorker { get; private set; }
        public CancellationTokenSource CurrentCancelationToken { get; private set; }

        public ThreadWorker() {
            CurrentCancelationToken = new CancellationTokenSource();

            TaskScheduler.UnobservedTaskException += (sender, e) => {
                CurrentCancelationToken.Cancel();
                foreach (Exception exception in e.Exception.InnerExceptions)
                    WinAPIAssert.Handle(exception);
            };

            ListDispatcher dispatcher = new ListDispatcher();
            CurrentWorker = new Worker(dispatcher);
        }

        public async Task Start() {
            try {
                CancellationToken cancellationToken = CurrentCancelationToken.Token;
                TaskScheduler scheduler = TaskScheduler.Current;

                CurrentTaskWorker = Task.Factory.StartNew(async () => {
                    await CurrentWorker.StartAsync(cancellationToken);
                }, cancellationToken, TaskCreationOptions.LongRunning, scheduler);

                await CurrentTaskWorker.WaitAsync(cancellationToken);
            }
            catch (Exception exception) {
                WinAPIAssert.Handle(exception);
            }
        }

        public void Dispose() {
            CurrentWorker.Dispose();
        }
    }
}