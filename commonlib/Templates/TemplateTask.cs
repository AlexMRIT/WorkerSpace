using System;
using commonlib.Interfaces;
using commonlib.Models;
using commonlib.WinUtils;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace commonlib.Templates
{
    internal class TemplateTask : ITask
    {
        internal TemplateTask(TaskBuilder builder)
        {
            taskBuilder = builder;
        }

        private bool _destroy = false;
        private bool _isRunning = false;
        private CancellationTokenSource _cancellationToken;
        private readonly TaskBuilder taskBuilder;

        public CancellationTokenSource CancellationToken => _cancellationToken;

        public bool TaskHasDelete { set => _destroy = value; }

        public bool TaskIsRunning => _isRunning;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> StartAsync()
        {
            _cancellationToken = new CancellationTokenSource();

            if (!_cancellationToken.IsCancellationRequested || _destroy)
            {
                _ = Task.Run(async () =>
                {
                    Func<Task> func = taskBuilder.GetTerminationCondition();
                    await func();
                    _cancellationToken.Cancel();
                });

                foreach (Action func in taskBuilder.GetStartFuncs())
                    func();
            }

            _isRunning = true;
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> ExecuteAsync()
        {
            if (!_cancellationToken.IsCancellationRequested || _destroy)
            {
                foreach (KeyValuePair<TaskExecutionMethod, Action> func in taskBuilder.GetExecuteFuncs())
                {
                    func.Value();
                    await Task.Delay(TimeSpan.FromSeconds(func.Key.Delay));
                }
            }
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> StopAsync()
        {
            if (!_cancellationToken.IsCancellationRequested || _destroy)
                foreach (Action func in taskBuilder.GetEndFuncs())
                    func();

            return await Task.FromResult(new HANDLE(Result.S_OK));
        }
    }
}