using System.Diagnostics;
using System.Threading.Tasks;
using TaskImplaments;
using TaskImplaments.Interfaces;
using TaskImplaments.Tasks;
using WorkerSpace.Interfaces;

#pragma warning disable IDE0090
#pragma warning disable IDE0063

namespace WorkerSpace
{
    internal sealed class Application
    {
        private static async Task Main() {
            using (ListDispatcher listDispatcher = new ListDispatcher()){
                listDispatcher.CreateWorker();

                /////////////
                for (int i = 0; i < 5; i++)
                {
                    CancellationTokenSource CurrentCancelationToken = new CancellationTokenSource();
                    CancellationToken cancellationToken = CurrentCancelationToken.Token;

                    TaskScheduler scheduler = TaskScheduler.Current;
                    Task executor = Task.Factory.StartNew(async () =>
                    {
                        Random rng = new Random();

                        IAbstractCounter abstractCounter = new RegisterCounter();
                        int count = rng.Next(5);
                        abstractCounter.Create(count);
                        TaskBaseImplement task = new PushEventMessage(abstractCounter);
                        task.SetTimeOut(rng.Next(5));
                        Console.WriteLine($"Task add. TimeOut:{task.TimeOut}| Counter: {count}");
                        await ListDispatcher.Create(task);

                        

                    }, cancellationToken, TaskCreationOptions.LongRunning, scheduler);
                    await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
                }
                //////////////

                Process.GetCurrentProcess().WaitForExit();
            }
            }
    }
}