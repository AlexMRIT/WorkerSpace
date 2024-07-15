using commonlib.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace commonlib.Models
{
    public class TaskBuilder
    {
        public TaskBuilder()
        {
            StartFuncs = new List<Func<Task>>();
            //ExecuteFuncs = new List<Func<Task>>();
            ExecuteFuncs = new List<KeyValuePair<Func<Task>, TaskBits>>();
            EndFuncs = new List<Func<Task>>();
        }

        private readonly List<Func<Task>> StartFuncs;
        private readonly List<KeyValuePair<Func<Task>, TaskBits>> ExecuteFuncs;
        private readonly List<Func<Task>> EndFuncs;

        private Func<Task> TerminationCondition;

        public TaskBuilder AppendStartDelegateFunc(Func<Task> func)
        {
            StartFuncs.Add(func);
            return this;
        }

        public TaskBuilder AppendExecuteDelegateFunc(Func<Task> func)
        {
            TaskBits bits = new TaskBits();
            bits.SetBit(CustomTaskStatus.TS_RUNING);
            ExecuteFuncs.Add(new KeyValuePair<Func<Task>, TaskBits>(func, bits));
            return this;
        }

        public TaskBuilder AppendEndFuncs(Func<Task> func)
        {
            EndFuncs.Add(func);
            return this;
        }

        public void AppendTerminationCondition(Func<Task> func)
        {
            TerminationCondition = func;
        }

        protected internal IEnumerable<Func<Task>> GetStartFuncs() => StartFuncs;
        protected internal IReadOnlyList<KeyValuePair<Func<Task>, TaskBits>> GetExecuteFuncs() => ExecuteFuncs;
        protected internal IEnumerable<Func<Task>> GetEndFuncs() => EndFuncs;
        protected internal Func<Task> GetTerminationCondition() => TerminationCondition;
    }
}