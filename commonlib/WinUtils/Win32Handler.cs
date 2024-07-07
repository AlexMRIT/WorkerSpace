﻿using commonlib.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace commonlib.WinUtils
{
    public enum Result : byte
    {
        S_OK = 0x00000000,
        E_ABORT = 0x00000001,
        E_FAIL = 0x000000002,
        E_END = 0x00000003
    }

    public readonly struct HANDLE
    {
        public HANDLE(Result result) : this(result, string.Empty)
        { }

        public HANDLE(Result result, string message)
        {
            HandleResult = result;
            HandleMessage = message;
        }

        public readonly Result HandleResult;
        public readonly string HandleMessage;
    }

    public static class WinAPIAssert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Success(HANDLE handle, out string messageError)
        {
            if (handle.HandleResult != Result.S_OK)
            {
                messageError = handle.HandleMessage;
                return false;
            }
            messageError = string.Empty;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Fail(HANDLE handle, out string messageError)
        {
            if (handle.HandleResult == Result.E_FAIL || handle.HandleResult == Result.E_ABORT)
            {
                messageError = handle.HandleMessage;
                return true;
            }
            messageError = string.Empty;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTaskCanceled(this ITask task)
        {
            return task.CancellationToken.IsCancellationRequested;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Handle(Exception exception)
        {
            Console.WriteLine($"Type Exception: {exception.GetType().FullName}");
            Console.WriteLine($"Message: {exception.Message}");
            Console.WriteLine($"Stack Trace: {exception.StackTrace}");
        }
    }
}