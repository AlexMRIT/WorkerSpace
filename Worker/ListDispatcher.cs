using System;
using WorkerSpace.Tasks;
using WorkerSpace.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

#pragma warning disable CS8618
#pragma warning disable IDE0028
#pragma warning disable CA1859

namespace WorkerSpace
{
    internal sealed class ListDispatcher : ITaskListDispatcher
    {
        private List<TaskBaseImplement> taskBaseImplements;

        public void Create()
        {
            taskBaseImplements = new List<TaskBaseImplement>();

            // Тут лучше всего реализовать статичный метод, чтобы можно было создать любую таску и впихнуть в очередь
            // Но для примера создадим задачу прямо тут :D
            IAbstractCounter abstractCounter = new RegisterCounter();
            abstractCounter.Create(4);
            taskBaseImplements.Add(new PushEventMessage(abstractCounter));
        }

        public async Task Dispose()
        {
            foreach (TaskBaseImplement task in taskBaseImplements)
                await task.EndAsync();
        }

        public async Task StartTasks()
        {
            foreach (TaskBaseImplement task in taskBaseImplements)
                await task.StartAsync();
        }

        public IEnumerable<TaskBaseImplement> GetTasks()
        {
            return taskBaseImplements.Where(task => task.StateTask.HandleResult != Result.E_END);
        }
    }
}