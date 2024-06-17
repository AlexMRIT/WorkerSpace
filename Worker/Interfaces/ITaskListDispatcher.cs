using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorkerSpace.Interfaces
{
    internal interface ITaskListDispatcher
    {
        //static abstract Task Create();
        Task StartTasks();
        List<ThreadWorker> GetThreadsWorkers();
        // Task Dispose();
        //IEnumerable<TaskBaseImplement> GetTasks();
    }
}