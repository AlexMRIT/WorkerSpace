namespace WorkerSpace.Interfaces
{
    internal interface IAbstractCounter
    {
        void Create(int count);
        bool IsAliveUpdate();
    }
}