using commonlib.Enums;
using commonlib.Models;
using commonlib.WinUtils;
using System.Threading;
using System.Threading.Tasks;

namespace commonlib.Interfaces
{
    public interface ITask
    {
        CancellationTokenSource CancellationToken { get; }

        TaskBits GetBits { get; }

        Task<HANDLE> StartAsync();
        Task<HANDLE> ExecuteAsync();
        Task<HANDLE> StopAsync();
    }
}