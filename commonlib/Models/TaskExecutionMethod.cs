using commonlib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commonlib.Models
{
    public readonly struct TaskExecutionMethod 
    {
        public TaskExecutionMethod(float sleep) {
            delay = sleep;
        }
        public readonly float delay;
    }
}
