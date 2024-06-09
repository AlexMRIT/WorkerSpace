using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerSpace;
using WorkerSpace.Interfaces;

namespace Worker.Tasks
{
    internal sealed class PublishConsoleMessage : TaskBaseImplement
    {
        public PublishConsoleMessage(IAbstractCounter abstractCounter, int timewait)
            : base(abstractCounter, timewait)
        {}
        public override async Task<HANDLE> StartAsync()
        {
            Console.WriteLine("StartTask");
            return await base.StartAsync();
        }

        public override async Task<HANDLE> ExecuteAsync()
        {
            Console.WriteLine("ExecuteTask");
            return await base.ExecuteAsync();
        }

        public override async Task<HANDLE> EndAsync()
        {
            Console.WriteLine("EndTask");
            return await base.EndAsync();
        }
    }
}
