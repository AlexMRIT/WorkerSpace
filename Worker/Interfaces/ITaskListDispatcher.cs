using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorkerSpace.Interfaces
{
    internal interface ITaskListDispatcher
    {
        static abstract void Create(TaskBaseImplement task);
        Task StartTasks();
        Task Dispose();
        IEnumerable<TaskBaseImplement> GetTasks();
    }
}