using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;

public class Experiment
{
    static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
    static Dictionary<int, int> _items = new Dictionary<int, int>();

    public static void Main()
    {

        Console.WriteLine("Running experiment with UpgradeableReadLock");
        RunExperiment(useUpgradeableReadLock: true);


        Console.WriteLine("Running experiment with separate Read and Write Locks");
        RunExperiment(useUpgradeableReadLock: false);

        ReadKey();
    }

    private static void RunExperiment(bool useUpgradeableReadLock)
    {

        Stopwatch stopwatch = Stopwatch.StartNew();
        int operations = 100;
        Thread thread;
        if (useUpgradeableReadLock)
            thread = new Thread(() => UpgradeableRead("Thread 1", operations));

        else
            thread = new Thread(() => ReadAndWrite("Thread 2", operations));

        thread.Start();
        // 等待线程完成
        thread.Join();
        stopwatch.Stop();
        Console.WriteLine($"Experiment completed in {stopwatch.ElapsedMilliseconds} ms.");
    }

    private static void UpgradeableRead(string threadName, int operations)
    {
        for (int i = 0; i < operations; i++)
        {
            try
            {
                int newKey = new Random().Next(250);

                _rw.EnterUpgradeableReadLock();
                if (!_items.ContainsKey(newKey))
                {
                    try
                    {
                        _rw.EnterWriteLock();
                        _items[newKey] = 1;
                        //WriteLine($"New key {newKey} is added to a dictionary by a {threadName}");
                    }
                    finally
                    {
                        _rw.ExitWriteLock();
                    }
                }
                Sleep(TimeSpan.FromSeconds(0.1));
            }
            finally
            {
                _rw.ExitUpgradeableReadLock();
            }
        }
    }

    private static void ReadAndWrite(string threadName, int operations)
    {
        for (int i = 0; i < operations; i++)
        {
            int newKey = new Random().Next(250);
            _rw.EnterReadLock();
            try
            {
                if (!_items.ContainsKey(newKey))
                {
                    _rw.ExitReadLock();
                    _rw.EnterWriteLock();
                    try
                    {
                        _items[newKey] = 1;
                        //WriteLine($"New key {newKey} is added to the dictionary by {threadName}");
                    }
                    finally
                    {
                        _rw.ExitWriteLock();
                        _rw.EnterReadLock(); // Optional: Only re-enter if more read work needed after write
                    }
                }
            }
            finally
            {
                _rw.ExitReadLock();
            }
            Thread.Sleep(10); // Simulate some work
        }
    }
}
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Diagnostics;

//public class Experiment
//{
//    static ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
//    static int sharedResource = 0;

//    public static void Main()
//    {
//        Console.WriteLine("Running experiment with UpgradeableReadLock...");
//        var timeWithUpgradeableLock = RunExperiment(true);

//        Console.WriteLine("Running experiment with separate Read and Write Locks...");
//        var timeWithSeparateLocks = RunExperiment(false);

//        Console.WriteLine($"Time with UpgradeableReadLock: {timeWithUpgradeableLock} ms.");
//        Console.WriteLine($"Time with separate Read and Write Locks: {timeWithSeparateLocks} ms.");
//    }

//    private static long RunExperiment(bool useUpgradeableReadLock)
//    {
//        int numThreads = 10;
//        Task[] tasks = new Task[numThreads];
//        Stopwatch stopwatch = new Stopwatch();
//        stopwatch.Start();

//        for (int i = 0; i < numThreads; i++)
//        {
//            if (useUpgradeableReadLock)
//                tasks[i] = Task.Run(() => ReadWriteWithUpgradeableLock());
//            else
//                tasks[i] = Task.Run(() => SeparateReadWriteLocks());
//        }

//        Task.WaitAll(tasks);
//        stopwatch.Stop();
//        return stopwatch.ElapsedMilliseconds;
//    }

//    private static void ReadWriteWithUpgradeableLock()
//    {
//        for (int i = 0; i < 100; i++)
//        {
//            rwLock.EnterUpgradeableReadLock();
//            try
//            {
//                if (sharedResource < 100)
//                {
//                    rwLock.EnterWriteLock();
//                    try
//                    {
//                        sharedResource++;
//                    }
//                    finally
//                    {
//                        rwLock.ExitWriteLock();
//                    }
//                }
//            }
//            finally
//            {
//                rwLock.ExitUpgradeableReadLock();
//            }
//        }
//    }

//    private static void SeparateReadWriteLocks()
//    {
//        for (int i = 0; i < 100; i++)
//        {
//            rwLock.EnterReadLock();
//            bool shouldWrite = false;
//            try
//            {
//                if (sharedResource < 100)
//                    shouldWrite = true;
//            }
//            finally
//            {
//                rwLock.ExitReadLock();
//            }

//            if (shouldWrite)
//            {
//                rwLock.EnterWriteLock();
//                try
//                {
//                    sharedResource++;
//                }
//                finally
//                {
//                    rwLock.ExitWriteLock();
//                }
//            }
//        }
//    }
//}
