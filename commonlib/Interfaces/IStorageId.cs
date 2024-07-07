using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commonlib.Interfaces
{
    public interface IStorageId
    {
        int GenerateId();

        int GetId();
    }
}