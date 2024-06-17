using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskImplaments.Interfaces;

namespace TaskImplaments
{
    public sealed class RegisterCounter : IAbstractCounter
    {
        private bool _infinity = false;
        private int _counter;

        public void Create(int count)
        {
            _counter = count;
            if (_counter == 0)
                _infinity = true;
        }

        public bool IsAliveUpdate()
        {
            if (!_infinity)
                _counter--;
            return !(_counter == 0 && !_infinity);
        }
    }
}
