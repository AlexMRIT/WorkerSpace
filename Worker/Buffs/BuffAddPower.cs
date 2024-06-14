using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerSpace;
using WorkerSpace.Interfaces;

namespace Worker.Buffs
{
    internal class BuffAddPower : BuffBaseImplement
    {
        public BuffAddPower(IAbstractCounter abstractCounter)
            : base(abstractCounter)
        {}

        public override async Task<HANDLE> StartAsync()
        {
            Console.WriteLine("StartTask - Add Power");
            return await base.StartAsync();
        }

        public override async Task<HANDLE> ExecuteAsync()
        {
            Console.WriteLine("ExecuteTask  - Add Power");
            Owner.Power += 10;
            return await base.ExecuteAsync();
        }

        public override async Task<HANDLE> EndAsync()
        {
            Console.WriteLine("EndTask - Add Power");
            Owner.Power -= 10;
            return await base.EndAsync();
        }
    }
}
