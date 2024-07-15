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
        public Task Start()
        {
            Console.WriteLine("Эта функция выполнилась при старте");
            return Task.CompletedTask;
        }

        public async Task Execute()
        {
            Console.WriteLine("А эта функция выполняется раз в 3sec");
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine("Функция с 3 секундами закончила свое выполнение");
        }

        public async Task Execute1()
        {
            Console.WriteLine("А эта функция выполняется раз в 5sec");
            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine("Функция с 5 секундами закончила свое выполнение");
        }

        public Task End()
        {
            Console.WriteLine("Тут уже таска прекратила свою жизнь :D");
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
            taskBuilder.AppendExecuteDelegateFunc(example.Execute1);
            taskBuilder.AppendEndFuncs(example.End);
            
           
            
            ITask task = CLIBTask.NewTask(taskBuilder);
           
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}