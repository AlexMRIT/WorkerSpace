using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Entities;
using WorkerSpace;
using WorkerSpace.Interfaces;

namespace Worker.Buffs
{
    internal class BuffBaseImplement : TaskBaseImplement
    {
        public BuffBaseImplement(IAbstractCounter abstractCounter)
            : base(abstractCounter)
        {}

        public virtual void SetBuffOwner(Character character)
        {
            Owner = character;
        }

        public Character Owner { get; private set; }
    }
}
