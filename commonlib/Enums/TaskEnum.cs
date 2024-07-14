namespace commonlib.Enums
{
    public enum CustomTaskStatus : byte
    {
        TS_HASDELETE = 1 << 1,
        TS_RUNING = 1 << 2,
        TS_BUSY = 1 << 3
    }
}