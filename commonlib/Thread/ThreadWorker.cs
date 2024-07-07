using System;
using System.Threading;
using commonlib.WinUtils;
using System.Threading.Tasks;

namespace commonlib.Thread
{
    internal sealed class ThreadWorker : IDisposable
    {
        public Worker CurrentWorker { get; private set; }
        public CancellationTokenSource CurrentCancelationToken { get; private set; }

        internal ThreadWorker()
        {
            CurrentCancelationToken = new CancellationTokenSource();

            TaskScheduler.UnobservedTaskException += (sender, e) => {
                CurrentCancelationToken.Cancel();
                foreach (Exception exception in e.Exception.InnerExceptions)
                    WinAPIAssert.Handle(exception);
            };

            CurrentWorker = new Worker();
        }

        public Worker Start()
        {
            try
            {
                CancellationToken cancellationToken = CurrentCancelationToken.Token;
                TaskScheduler scheduler = TaskScheduler.Current;

                Task.Factory.StartNew(async () => {
                    await CurrentWorker.ExecuteTaskAsync();
                }, cancellationToken, TaskCreationOptions.LongRunning, scheduler);

                return CurrentWorker;
            }
            catch (Exception exception)
            {
                WinAPIAssert.Handle(exception);
                return null;
            }
        }

        public void Dispose()
        {
            CurrentWorker.Dispose();
        }
    }
}