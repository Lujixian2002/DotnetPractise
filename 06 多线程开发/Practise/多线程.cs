using System;
using System.Threading;

using static System.Console;
using static System.Threading.Thread;

class Program
{
    static void PrintNumbers()
    {
        WriteLine("Starting printing numbers ...");
        for (int i = 0; i < 10; i++)
            WriteLine(i);
    }

    // Sleep 暂停线程
    static void PrintNumbersWithDelay()
    { 
        try
        {
            WriteLine("Starting printing numbers with delay ...");
            // 1.5 线程运行状态
            WriteLine("The state now is:" + Thread.CurrentThread.ThreadState.ToString());


            for (int i = 0; i < 10; i++)
            {
                // 1.2 暂停线程 Sleep
                Sleep(TimeSpan.FromSeconds(2));
                WriteLine(i);
            }
        }
        catch
        {
            Console.WriteLine("Here ThreadAbortException");
        }
        finally
        {
            WriteLine("\r\nPrintNumbersWithDelay exit 1");
        }
        WriteLine("\r\nPrintNumbersWithDelay exit 2");

    }

    static void DoNothing()
    {
            Sleep(TimeSpan.FromSeconds(2));
    }
    //// 线程运行状态
    //static void PrintNumbersWithStatus()
    //{
    //    WriteLine("Starting printing numbers with status ...");
    //    WriteLine(Thread.CurrentThread.ThreadState.ToString());
    //    for (int i = 0; i < 10; i++) 
    //    {
    //        Sleep(TimeSpan.FromSeconds(2));
    //        WriteLine(i);
    //    }
    //}



    static void Main(string[] args)
    {
        WriteLine("Starting program");
        //Thread t = new Thread(PrintNumbersWithDelay);

        // 1.1 创建线程
        Thread t = new Thread(PrintNumbersWithDelay);
        Thread t2 = new Thread(DoNothing);

        // 1.5 线程运行状态
        WriteLine("The state now is : " + t.ThreadState.ToString());
        t2.Start();
        t.Start();


        Sleep(TimeSpan.FromSeconds(6));


        // 1.4 终止线程，需要配合try，catch使用
        t.Interrupt();
        // 1.3 等待线程结束Join
        t.Join(); // 等待线程结束
        WriteLine("Thread Completed");

    }
}

