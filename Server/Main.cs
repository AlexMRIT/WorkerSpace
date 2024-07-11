using commonlib;
using commonlib.Interfaces;
using commonlib.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Server
{
    internal class Application
    {
        private static void Main()
        {
            CLIBTask.CreateWorker("Main Worker");

            TaskBuilder taskBuilder = new TaskBuilder();
            taskBuilder.AppendStartDelegateFunc(() => { Console.WriteLine("Эта функция выполнилась при старте"); });
            taskBuilder.AppendExecuteDelegateFunc(() => { Console.WriteLine("А эта функция выполняется раз в 5sec"); }, sleep: 5f);
            taskBuilder.AppendExecuteDelegateFunc(() => { Console.WriteLine("А эта функция выполняется раз в 3sec"); }, sleep: 3f);
            taskBuilder.AppendExecuteDelegateFunc(() => { Console.WriteLine("А эта функция выполняется раз в секунду"); });
            taskBuilder.AppendEndFuncs(() => { Console.WriteLine("Тут уже таска прекратила свою жизнь :D"); });

            taskBuilder.AppendTerminationCondition(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
            });

            ITask task = CLIBTask.NewTask(taskBuilder);
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}