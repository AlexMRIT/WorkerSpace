using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
   public class Drawer
   {
       public Drawer()
       {
           _world = new string[]{"###################################################################################################",
                                 "#                                                                                                 #",
                                 "#                          #                                                                      #",
                                 "#                            #                                                                    #",
                                 "#                           #                                                                     #",
                                 "#                                                                                                 #",
                                 "#                                #                                                                #",
                                 "#                                #                #                                               #",
                                 "#                                                 #                                               #",
                                 "#                                                 #                                               #",
                                 "#                        ####                     #                                               #",
                                 "#                                                 #                                               #",
                                 "#                                           ##############                                        #",
                                 "#                                                                                                 #",
                                 "#                                                                                                 #",
                                 "#                     #                                                                           #",
                                 "#                     #                                                                           #",
                                 "#                     #                                      #####################                #",
                                 "#                     #                                                                           #",
                                 "#                     #                                                                           #",
                                 "#                     #                                                                           #",
                                 "#                     #                                                                           #",
                                 "#                     #                                                                           #",
                                 "#                     #                                                                           #",
                                 "#                     #                                                                           #",
                                 "#                                                                                                 #",
                                 "#                                                                                                 #",
                                 "#                                                                                                 #",
                                 "###################################################################################################" };
       }
       private readonly string[] _world;

       public void PaintMap()
       {
           Console.Clear();
           for (int i = 0; i < _world.Length; i++)
               Console.WriteLine(_world[i]);
       }

        public bool PaintElement(int x, int y, string element)
        {
            if (_world[y][x] == '#')
                return false;
            
            Console.Clear();
            PaintMap();
            Console.SetCursorPosition(x, y);
            Console.Write(element);

            Console.SetCursorPosition(101, 4);
            Console.Write($"X: {x}  Y: {y}");
            return true;
        }
   }
    
}
