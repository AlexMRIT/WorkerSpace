using commonlib.Interfaces;
using commonlib.Templates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace commonlib.Models
{
    public class TaskBuilder
    {

        public TaskBuilder()
        {
            StartFuncs = new List<Action>();
            ExecuteFuncs = new List<KeyValuePair<TaskExecutionMethod, Action>>();
            EndFuncs = new List<Action>();

        }

        private readonly List<Action> StartFuncs;
        private readonly List<KeyValuePair<TaskExecutionMethod, Action>> ExecuteFuncs;
        private readonly List<Action> EndFuncs;
        private Func<Task> TerminationCondition;

        public TaskBuilder AppendStartDelegateFunc(Action func)
        {
            StartFuncs.Add(func);
            return this;
        }

        public TaskBuilder AppendExecuteDelegateFunc(Action func, float sleep = 1)
        {
            ExecuteFuncs.Add(new KeyValuePair<TaskExecutionMethod,Action>(new TaskExecutionMethod(sleep), func));
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
        protected internal IReadOnlyCollection<KeyValuePair<TaskExecutionMethod, Action>> GetExecuteFuncs() => ExecuteFuncs;
        protected internal IReadOnlyCollection<Action> GetEndFuncs() => EndFuncs;
        protected internal Func<Task> GetTerminationCondition() => TerminationCondition;

    }
}