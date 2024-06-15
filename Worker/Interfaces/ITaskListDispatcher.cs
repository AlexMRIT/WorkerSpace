using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorkerSpace.Interfaces
{
    internal interface ITaskListDispatcher
    {
        void Create();
        Task StartTasks();
        Task Dispose();
        IEnumerable<TaskBaseImplement> GetTasks();
    }
}