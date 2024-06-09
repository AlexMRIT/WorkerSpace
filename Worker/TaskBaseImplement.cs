using System;
using System.Threading.Tasks;
using WorkerSpace.Interfaces;

namespace WorkerSpace
{
    internal class TaskBaseImplement
    {
#pragma warning disable IDE0290
        public TaskBaseImplement(IAbstractCounter abstractCounter, int timeOut)
        {
            AbstractCounter = abstractCounter;
            TimeOut = timeOut;

        }
#pragma warning restore IDE0290

        public HANDLE StateTask { get; private set; }
        public IAbstractCounter AbstractCounter { get; private set; }

        public int TimeOut { get; private set; }

        public virtual async Task<HANDLE> StartAsync()
        {
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        public virtual async Task<HANDLE> ExecuteAsync()
        {
            if (!AbstractCounter.IsAliveUpdate())
            {
                StateTask = new HANDLE(Result.E_END);
                return await Task.FromResult(StateTask);
            }
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        public virtual async Task<HANDLE> EndAsync()
        {
            StateTask = new HANDLE(Result.E_END);
            return await Task.FromResult(StateTask);
        }
    }
}