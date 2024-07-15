using commonlib.Enums;

namespace commonlib.Models
{
    public readonly struct TaskExecutionMethod 
    {
        public TaskExecutionMethod(float sleep)
        {
            Delay = sleep;
            Bits = new TaskBits();
            Bits.SetBit(CustomTaskStatus.TS_RUNING);
        }

        public readonly float Delay;
        public readonly TaskBits Bits;
    }
}
