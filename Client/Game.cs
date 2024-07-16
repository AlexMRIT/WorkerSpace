using commonlib.Enums;
using commonlib.WinUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Game
    {
        public void Start()
        {
            Drawer drawer = new Drawer();

            PlayerTemplate player = new PlayerTemplate(drawer);
            player.Drawer.PaintElement(1, 1, "@");
        }
    }
}
