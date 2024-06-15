using WorkerSpace.Interfaces;

namespace WorkerSpace
{
    internal sealed class RegisterCounter : IAbstractCounter {
        private bool _infinity = false;
        private int _counter;

        public void Create(int count) {
            _counter = count;
            if (_counter == 0)
                _infinity = true;
        }

        public bool IsAliveUpdate() {
            if (!_infinity)
                _counter--;
            return !(_counter == 0 && !_infinity);
        }
    }
}