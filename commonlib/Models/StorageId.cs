using commonlib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commonlib.Models
{
    internal class StorageId : IStorageId
    {
        private Guid guid;

        public int GenerateId()
        {
            guid = Guid.NewGuid();
            return guid.GetHashCode();
        }

        public int GetId()
        {
            return guid.GetHashCode();
        }
    }
}