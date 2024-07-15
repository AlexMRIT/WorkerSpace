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
    public class ExampleImplTask
    {
        public int Time = 10;

        public Task Start()
        {
            Console.WriteLine("Мы навесили баф: увеличить силу +15");
            return Task.CompletedTask;
        }

        public async Task Execute()
        {
            Console.WriteLine($"Бафу осталось времени: {Time}");
            await Task.Delay(1000);
            Time--;
        }

        public Task End()
        {
            Console.WriteLine("Мы сняли баф: увелчить силу +15");
            return Task.CompletedTask;
        }
    }

    internal class Application
    {
        private static void Main()
        {
            CLIBTask.CreateWorker("Main Worker");

            ExampleImplTask example = new ExampleImplTask();
            TaskBuilder taskBuilder = new TaskBuilder();
            taskBuilder.AppendStartDelegateFunc(example.Start);
            taskBuilder.AppendExecuteDelegateFunc(example.Execute);
            taskBuilder.AppendEndFuncs(example.End);

            taskBuilder.AppendTerminationCondition(async () =>
            {
                int time = example.Time;
                await Task.Delay(TimeSpan.FromSeconds(time));
            });
            
            ITask task = CLIBTask.NewTask(taskBuilder);
           
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}