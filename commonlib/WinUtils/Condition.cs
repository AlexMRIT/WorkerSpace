using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commonlib.WinUtils
{
    public enum TaskStatus
    { 
        AVALIABLE = 1 << 1,
        WORK = 1 << 2
    }

    public class Conduction
    {
        public Conduction()
        {
            _status = 0b00000000;
        }
        private int _status;
        public bool IsFlagEnable(byte offset) 
        {
            int flagWithOffset  = _status |= 1 << offset;
            Console.WriteLine(flagWithOffset);
                if((TaskStatus)flagWithOffset == TaskStatus.WORK)
                    return true;

                return false;
        }
    }
}
