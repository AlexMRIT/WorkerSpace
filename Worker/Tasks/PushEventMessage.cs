using System;
using WorkerSpace.Interfaces;
using System.Threading.Tasks;

namespace WorkerSpace.Tasks
{
    internal sealed class PushEventMessage : TaskBaseImplement {
#pragma warning disable IDE0290
        public PushEventMessage(IAbstractCounter abstractCounter) 
            : base(abstractCounter)
        { }
#pragma warning restore IDE0290

        public override async Task<HANDLE> StartAsync() {
            Console.WriteLine("Start");
            return await base.StartAsync();
        }

        public override async Task<HANDLE> ExecuteAsync() {
            Console.WriteLine("Execute");
            return await base.ExecuteAsync();
        }

        public override async Task<HANDLE> EndAsync() {
            Console.WriteLine("End");
            return await base.EndAsync();
        }
    }
}