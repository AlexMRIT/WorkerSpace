using commonlib;
using commonlib.Interfaces;
using commonlib.Models;
using plib;
using plib.Enums;
using plib.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Server
{
    public class ExampleInplTask
    {
       // [Profiled(nameof(Start), ProfiledOperationImplement.POI_SYNC)]
        public void Start()
        {
            Console.WriteLine("Эта функция выполнилась при старте");
        }

        //[Profiled(nameof(Execute), ProfiledOperationImplement.POI_SYNC)]
        public void Execute()
        {
            Console.WriteLine("А эта функция выполняется раз в 3sec");
        }
        //[Profiled(nameof(Execute1), ProfiledOperationImplement.POI_SYNC)]
        public void Execute1()
        {
            Console.WriteLine("А эта функция выполняется раз в 5sec");
        }

       // [Profiled(nameof(End), ProfiledOperationImplement.POI_SYNC)]
        public void End()
        {
            Console.WriteLine("Тут уже таска прекратила свою жизнь :D");
        }
    }

    internal class Application
    {
        private static void Main()
        {
            CLIBTask.CreateWorker("Main Worker");

            ExampleInplTask example = new ExampleInplTask();
            TaskBuilder taskBuilder = new TaskBuilder();
            taskBuilder.AppendStartDelegateFunc(example.Start);
            taskBuilder.AppendExecuteDelegateFunc(example.Execute, sleep: 3f);
            taskBuilder.AppendExecuteDelegateFunc(example.Execute1, sleep: 5f);
            taskBuilder.AppendEndFuncs(example.End);
            
            taskBuilder.AppendTerminationCondition(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
            });
            
            ITask task = CLIBTask.NewTask(taskBuilder);
            //ProfiledRecursive.Run(example);
           
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}