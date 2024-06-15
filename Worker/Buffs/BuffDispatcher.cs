using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Entities;
using WorkerSpace;
using WorkerSpace.Interfaces;
using WorkerSpace.Tasks;

namespace Worker.Buffs
{
    internal class BuffDispatcher : ITaskListDispatcher
    {
        public BuffDispatcher() 
        {}

        private List<TaskBaseImplement> taskBaseImplements = new List<TaskBaseImplement>();

        public void Create()
        {
            IAbstractCounter abstractCounter = new RegisterCounter();
            abstractCounter.Create(4);
            taskBaseImplements.Add(new PushEventMessage(abstractCounter));
            IAbstractCounter abstractCounter1 = new RegisterCounter();
            abstractCounter1.Create(10);
            taskBaseImplements.Add(new PushEventMessage(abstractCounter1));
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
