using System;
using commonlib.Interfaces;
using commonlib.Models;
using commonlib.WinUtils;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using commonlib.Enums;
using System.Linq;

namespace commonlib.Templates
{
    internal class TemplateTask : ITask
    {
        internal TemplateTask(TaskBuilder builder)
        {
            taskBuilder = builder;
            _bits = new TaskBits();
            _cancellationToken = new CancellationTokenSource();
        }

        protected internal readonly CancellationTokenSource _cancellationToken;
        protected internal readonly TaskBuilder taskBuilder;
        protected internal readonly TaskBits _bits;

        public CancellationTokenSource CancellationToken => _cancellationToken;
        public TaskBits GetBits => _bits;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> StartAsync()
        {
            if (!_cancellationToken.IsCancellationRequested || _bits.IsBitSet(CustomTaskStatus.TS_HASDELETE))
            {
                _ = Task.Run(async () =>
                {
                    Func<Task> func = taskBuilder.GetTerminationCondition();
                    if (func != null) await func().ContinueWith(_ => _cancellationToken.Cancel());
                });

                await Task.WhenAll(taskBuilder.GetStartFuncs().Select(async action => await Task.Run(action)).ToArray());
            }

            _bits.SetBit(CustomTaskStatus.TS_RUNING);
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> ExecuteAsync()
        {
            if (!_cancellationToken.IsCancellationRequested || _bits.IsBitSet(CustomTaskStatus.TS_HASDELETE))
            {
                _bits.SetBit(CustomTaskStatus.TS_BUSY);
                await Task.WhenAll(taskBuilder.GetExecuteFuncs().Select(async action => await Task.Run(action)).ToArray());
            _bits.ClearBit(CustomTaskStatus.TS_BUSY);
            }

            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> StopAsync()
        {
            if (!_cancellationToken.IsCancellationRequested || _bits.IsBitSet(CustomTaskStatus.TS_HASDELETE))
                await Task.WhenAll(taskBuilder.GetEndFuncs().Select(async action => await Task.Run(action)).ToArray());

            _bits.ClearBit(CustomTaskStatus.TS_RUNING);
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }
    }
}