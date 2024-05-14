using System.Diagnostics;
using System.Threading.Tasks;

#pragma warning disable IDE0090
#pragma warning disable IDE0063

namespace WorkerSpace
{
    internal sealed class Application
    {
        private static async Task Main() {
            using (ThreadWorker threadWorker = new ThreadWorker()) {
                await threadWorker.Start();
                Process.GetCurrentProcess().WaitForExit();
            }
        }
    }
}