using commonlib.Models;
using commonlib.WinUtils;
using System.Threading;
using System.Threading.Tasks;

namespace commonlib.Interfaces
{
    public interface ITask
    {
        CancellationTokenSource CancellationToken { get; }
        bool TaskHasDelete { set; }
        bool TaskIsRunning { get; }

        Task<HANDLE> StartAsync();
        Task<HANDLE> ExecuteAsync();
        Task<HANDLE> StopAsync();
        TaskBuilder GetTaskBuilder();
    }
}