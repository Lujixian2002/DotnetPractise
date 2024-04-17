using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using static System.Console;

public class Program
{
    const string Item = "Dictionary item";
    const int Iterations = 10000000;
    public static string CurrentItem;

    public static void Main()
    {
        Console.WriteLine("Running single-threaded test...");
        var singleThreadTime = SingleThreadTest();
        Console.WriteLine($"Single-threaded test completed in {singleThreadTime} ms.");


        Console.WriteLine("Running multi-threaded test with 6 threads...");
        var multiThreadTime = MultiThreadTest(6);
        Console.WriteLine($"Multi-threaded test completed in {multiThreadTime} ms.");
        ReadKey();
    }

    private static long SingleThreadTest()
    {
        var concurrentDictionary = new ConcurrentDictionary<int, string>();

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < Iterations; i++)
               concurrentDictionary[i] = Item;

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    private static long MultiThreadTest(int numThreads)
    {
        var concurrentDictionary = new ConcurrentDictionary<int, string>();
        Stopwatch stopwatch = new Stopwatch();

        Task[] tasks = new Task[numThreads];
        int operationsPerThread = Iterations / numThreads;  // Divide operations evenly among threads

        stopwatch.Start();
        for (int t = 0; t < numThreads; t++)
        {
            int threadStartIndex = t * operationsPerThread;
            tasks[t] = Task.Run(() =>
            {
                for (int i = 0; i < operationsPerThread; i++)
                    concurrentDictionary[threadStartIndex + i] = Item;
            });
        }

        Task.WaitAll(tasks); // Wait for all tasks to complete
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}