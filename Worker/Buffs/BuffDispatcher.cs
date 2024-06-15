using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Entities;
using WorkerSpace;
using WorkerSpace.Interfaces;

namespace Worker.Buffs
{
    internal class BuffDispatcher
    {
        public BuffDispatcher() 
        {
            for (int i = 0; i < 100; i++)
                _characterList.Add(new Character());

        }
        private List<Character> _characterList = new List<Character>();

        public async Task<HANDLE> StartAsync() 
        {
            try
            {
                Task executor = Task.Factory.StartNew(() =>
                {     
                    foreach (var character in _characterList)
                    {
                        IAbstractCounter abstractCounter = new RegisterCounter();
                        abstractCounter.Create(1);
                        BuffBaseImplement buff = new BuffAddPower(abstractCounter);
                        buff.SetBuffOwner(character);
                        ListDispatcher.Create(buff);
                    }
                    System.Threading.Thread.Sleep(1);
                });
            }
            catch (Exception exception)
            {
                WinAPIAssert.Handle(exception);
            }
            
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        
    }
}
