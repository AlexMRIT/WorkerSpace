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
            ExecuteFuncs = new List<Func<Task>>();
            EndFuncs = new List<Func<Task>>();
        }

        private readonly List<Func<Task>> StartFuncs;
        private readonly List<Func<Task>> ExecuteFuncs;
        private readonly List<Func<Task>> EndFuncs;

        private Func<Task> TerminationCondition;

        public TaskBuilder AppendStartDelegateFunc(Func<Task> func)
        {
            StartFuncs.Add(func);
            return this;
        }

        public TaskBuilder AppendExecuteDelegateFunc(Func<Task> func)
        {
            ExecuteFuncs.Add(func);
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
        protected internal IReadOnlyList<Func<Task>> GetExecuteFuncs() => ExecuteFuncs;
        protected internal IEnumerable<Func<Task>> GetEndFuncs() => EndFuncs;
        protected internal Func<Task> GetTerminationCondition() => TerminationCondition;
    }
}