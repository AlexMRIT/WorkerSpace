using System.Runtime.CompilerServices;

namespace Win32Handlers
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
        public HANDLE() : this(Result.S_OK)
        { }

        public HANDLE(Result result) : this(result, string.Empty)
        { }

#pragma warning disable IDE0290
        public HANDLE(Result result, string message)
        {
            HandleResult = result;
            HandleMessage = message;
        }
#pragma warning restore IDE0290

        public readonly Result HandleResult;
        public readonly string HandleMessage;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TaskEnded()
        {
            return HandleResult == Result.E_END;
        }
    }

    public static class WinAPIAssert
    {
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

        public static void Handle(Exception exception)
        {
            Console.WriteLine($"Type Exception: {exception.GetType().FullName}");
            Console.WriteLine($"Message: {exception.Message}");
            Console.WriteLine($"Stack Trace: {exception.StackTrace}");
        }
    }
}
