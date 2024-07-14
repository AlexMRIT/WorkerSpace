﻿using System;
using commonlib.Interfaces;
using commonlib.Models;
using commonlib.WinUtils;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using commonlib.Enums;

namespace commonlib.Templates
{
    internal class TemplateTask : ITask
    {
        internal TemplateTask(TaskBuilder builder)
        {
            taskBuilder = builder;
            _bits = new TaskBits();
        }

        private CancellationTokenSource _cancellationToken;
        private readonly TaskBuilder taskBuilder;

        protected internal readonly TaskBits _bits;

        public CancellationTokenSource CancellationToken => _cancellationToken;
        public TaskBits GetBits => _bits;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> StartAsync()
        {
            _cancellationToken = new CancellationTokenSource();

            if (!_cancellationToken.IsCancellationRequested || _bits.IsBitSet(CustomTaskStatus.TS_HASDELETE))
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

            _bits.SetBit(CustomTaskStatus.TS_RUNING);
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> ExecuteAsync()
        {
            if (!_cancellationToken.IsCancellationRequested || _bits.IsBitSet(CustomTaskStatus.TS_HASDELETE))
            {
                foreach (KeyValuePair<TaskExecutionMethod, Action> func in taskBuilder.GetExecuteFuncs())
                {
                    _bits.SetBit(CustomTaskStatus.TS_BUSY);
                    func.Value();
                    await Task.Delay(TimeSpan.FromSeconds(func.Key.Delay));
                    //_bits.ClearBit(CustomTaskStatus.TS_BUSY);
                }
            }

            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task<HANDLE> StopAsync()
        {
            if (!_cancellationToken.IsCancellationRequested || _bits.IsBitSet(CustomTaskStatus.TS_HASDELETE))
                foreach (Action func in taskBuilder.GetEndFuncs())
                    func();

            _bits.ClearBit(CustomTaskStatus.TS_RUNING);
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }
    }
}