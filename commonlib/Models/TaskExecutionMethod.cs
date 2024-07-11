namespace commonlib.Models
{
    public readonly struct TaskExecutionMethod 
    {
        public TaskExecutionMethod(float sleep)
        {
            Delay = sleep;
        }

        public readonly float Delay;
    }
}
