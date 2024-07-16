using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Tasks
{
    public class PlayerMovementAwaiter
    {
        public PlayerMovementAwaiter(PlayerTemplate player) 
        {
            _player = player;
        }
        private readonly PlayerTemplate _player;
        public async Task Execute()
        {
            try
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (_player.Drawer.PaintElement(_player.x, _player.y - 1, "@"))
                            _player.y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (_player.Drawer.PaintElement(_player.x, _player.y + 1, "@"))
                        _player.y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (_player.Drawer.PaintElement(_player.x - 1, _player.y, "@"))
                        _player.x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (_player.Drawer.PaintElement(_player.x + 1, _player.y, "@"))
                        _player.x++;
                        break;
                }

                await Task.Delay(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
