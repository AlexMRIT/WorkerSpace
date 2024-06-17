using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskImplaments.Interfaces
{
    public interface IAbstractCounter
    {
        void Create(int count);
        bool IsAliveUpdate();
    }
}
