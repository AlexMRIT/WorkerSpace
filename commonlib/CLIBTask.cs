using commonlib.Interfaces;
using commonlib.Models;
using commonlib.Templates;
using commonlib.Thread;
using System;
using System.Collections.Generic;
using System.Linq;

namespace commonlib
{
    internal static class WorkerManager
    {
        private static readonly Dictionary<string, Worker> threadWorkers = new Dictionary<string, Worker>();

        public static void CreateWorker(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ThreadWorker threadWorker = new ThreadWorker();
                threadWorkers.Add(name, threadWorker.Start());
                Console.WriteLine($"Worker \'{name}\' has been added!");
            }
        }

        public static void DeleteWorker(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (threadWorkers.ContainsKey(name))
                    threadWorkers.Remove(name);
            }
        }

        public static void Insert(ITask task)
        {
            IStorageId storageId = new StorageId();
            storageId.GenerateId();

            Worker workerWithMinCount = threadWorkers.Values
                .OrderBy(worker => worker.CountRegistered)
                .FirstOrDefault();

            if (workerWithMinCount == null)
                Console.WriteLine("ВНИМАНИЕ, не найден воркер! Таска не зарегестрирована!");

            workerWithMinCount?.InsertTask(storageId, task);
        }
    }

    public static class CLIBTask
    {
        public static void CreateWorker(string name)
        {
            WorkerManager.CreateWorker(name);
        }

        public static ITask NewTask(TaskBuilder builder)
        {
            TemplateTask template = new TemplateTask(builder);
            WorkerManager.Insert(template);

            return template;
        }
    }
}