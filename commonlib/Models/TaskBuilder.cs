using commonlib.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace commonlib.Models
{
    public class TaskBuilder
    {
        private readonly List<Action> StartFuncs;
        private readonly List<Action> ExecuteFuncs;
        private readonly List<Action> EndFuncs;
        private Func<Task> TerminationCondition;

        public TaskBuilder()
        {
            StartFuncs = new List<Action>();
            ExecuteFuncs = new List<Action>();
            EndFuncs = new List<Action>();
        }

        public TaskBuilder AppendStartDelegateFunc(Action func)
        {
            StartFuncs.Add(func);
            return this;
        }

        public TaskBuilder AppendExecuteDelegateFunc(Action func)
        {
            ExecuteFuncs.Add(func);
            return this;
        }

        public TaskBuilder AppendEndFuncs(Action func)
        {
            EndFuncs.Add(func);
            return this;
        }

        public void AppendTerminationCondition(Func<Task> func)
        {
            TerminationCondition = func;
        }

        protected internal IReadOnlyCollection<Action> GetStartFuncs() => StartFuncs;
        protected internal IReadOnlyCollection<Action> GetExecuteFuncs() => ExecuteFuncs;
        protected internal IReadOnlyCollection<Action> GetEndFuncs() => EndFuncs;
        protected internal Func<Task> GetTerminationCondition() => TerminationCondition;
    }
}