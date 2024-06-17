using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskImplaments.Interfaces;
using Win32Handlers;

namespace TaskImplaments.Tasks
{
    public class PushEventMessage : TaskBaseImplement
    {
        public PushEventMessage(IAbstractCounter abstractCounter)
            : base(abstractCounter)
        { }
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
