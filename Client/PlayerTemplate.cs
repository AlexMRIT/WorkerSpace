using Client.Tasks;
using commonlib.Interfaces;
using commonlib;
using commonlib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class PlayerTemplate
    {

        public PlayerTemplate(Drawer drawer) 
        {
            Drawer = drawer;

            PlayerMovementAwaiter playerMovementAwaiter = new PlayerMovementAwaiter(this);
            TaskBuilder taskBuilder = new TaskBuilder();
            taskBuilder.AppendExecuteDelegateFunc(playerMovementAwaiter.Execute);

            ITask task = CLIBTask.NewTask(taskBuilder);
        }
        public readonly Drawer Drawer;
        public int x = 1;
        public int y = 1;
    }
}
