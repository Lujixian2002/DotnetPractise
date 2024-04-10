执行Main()方法的线程通常被称为主线程 (Main thread)

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/eac6e0f8-33e5-4322-b025-4e398e0c0667/Untitled.png)

# 1. 线程基本内容

1. 创建线程 new Thread
2. 暂停线程 Sleep （如何实现？）

   Sleep()⽅法，以及后⾯要介绍的Join()⽅法，均为阻塞⽅法（Blockmethod），意思是当处于阻塞时，它不会再占⽤CPU时间，直到阻塞结束，并开始执⾏后续代码的时候。

3. 等待线程结束 Join
4. 终止线程 Abort

   .net新版本不支持abort：

   System.PlatformNotSupportedException:“Thread abort is not supported on this platform.”

   利用Interrupt进行替代

    ```
        // 1.4 终止线程，需要配合try，catch使用
        t.Interrupt();
    ```

5. 线程运行状态

    ```csharp
    /*
    	1-5 放在一起进行试验
    */
    
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
    
    ```

6. 线程优先集

    ```csharp
    class Program
    {
    	/*
    	 * 这个类的对象在stop前一直被计数
    	 * 如果stop了则会输出计数的值
    	 * 同时输出该线程的优先级
    	 */
    	class ThreadSample
    	{
    		private bool _isStopped = false;
    		public void Stop()
    		{
    			_isStopped = true;
    		}
    		public void CountNumbers()
    		{
    			long counter = 0;
    			while (!_isStopped)
    				counter++;
    			WriteLine($"{CurrentThread.Name} with " +
    				$"{CurrentThread.Priority,11} priority " +
    				$"has a count = {counter,13:N0}");
    		}
    	}
    	static void RunThreads()
    	{
    		/*
    		 * threadOne优先级高于threadTwo，所以会被更频繁地执行
    		 */
    		var sample = new ThreadSample();
    		var threadOne = new Thread(sample.CountNumbers); 
    		threadOne.Name = "ThreadOne";
    		var threadTwo = new Thread(sample.CountNumbers);
    		threadTwo.Name = "ThreadTwo";
    		threadOne.Priority = ThreadPriority.Highest;
    		threadTwo.Priority = ThreadPriority.Lowest;
    		threadOne.Start();   //threadOne开始计数
    		threadTwo.Start();   //threadTwo开始计数
    		// 实际上是两个不同的线程
    		Sleep(TimeSpan.FromSeconds(2));  // 等待两秒后停止
    		sample.Stop();
    	}
    	static void Main(string[] args)
    	{
    		WriteLine($"Current thread priority: {CurrentThread.Priority}");  //主线程的优先级
    		WriteLine("Running on all cores available");
    		RunThreads();
    		Sleep(TimeSpan.FromSeconds(2));
    		WriteLine("Running on a single core");
    		//每个处理器都表示为一个位。 位 0 是处理器 1，位 1 是处理器 2，等等
    		//试试 1 ,3 ,7，看看有什么效果
    		Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(3);
    		RunThreads();
    		WriteLine("Main End.");
    		Console.ReadKey();
    	}
    }
    
    ```

   ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/a2369f6b-3b35-4162-a947-a3831e254523/Untitled.png)

7. 前后台线程

   所有前台线程执行完毕之后，应用程序进程结束，而不论后台进程是否结束。

    ```csharp
    class Program
    {
    	static void Main(string[] args)
    	{
    		var sampleForeground = new ThreadSample(10);
    		var sampleBackground = new ThreadSample(20);
    		var threadOne = new Thread(sampleForeground.CountNumbers);
    		threadOne.Name = "ForegroundThread";
    		var threadTwo = new Thread(sampleBackground.CountNumbers);
    		threadTwo.Name = "BackgroundThread";
            //threadTwo.IsBackground = true;
            threadTwo.IsBackground = false;
            threadOne.Start();
    		threadTwo.Start();
    	}
    
    	class ThreadSample
    	{
    		private readonly int _iterations;
    		public ThreadSample(int iterations)
    		{
    			_iterations = iterations;
    		}
    		public void CountNumbers()
    		{
    			for (int i = 0; i < _iterations; i++)
    			{
    				Sleep(TimeSpan.FromSeconds(0.5));
    				WriteLine($"{CurrentThread.Name} prints {i}");
    			}
    		}
    	}
    }
    
    ```

   左图是不设置后台线程，有图为设置后台线程。

   ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/4e123755-9220-4774-b245-e3b2a10d8a04/Untitled.png)

8. 向线程传递参数
    1. **`ref`**:用于声明方法参数是按引用传递的
    2. **`out`**: 用于声明方法参数是输出参数。**`out`** 参数在方法内必须在使用前被赋值
    3. **`params`**: 用于指定一个参数数组。

   ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/498e8409-d8bb-4ee7-96da-349cc02066dc/Untitled.png)

9. 使用线程局部存储

   静态变量，被所有线程共享。这意味着所有线程都可以访问和修改这个变量，而不会为每个线程维护一个单独的副本。

   ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/5509bff1-679f-4273-b654-5542b8f86950/Untitled.png)

10. 线程函数要处理异常

    ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/9e2e360b-a1ec-4af5-be55-9e76fc3e990b/Untitled.png)


# 2. 多线程基本同步

1. 使用Monitor lock

   System.Threading.Monitor对资源进⾏保护的思路很简单，即使⽤排他锁（Exclusive Lock）。当线程A需要访问某⼀资源（对象、⽅法、类型成员、代码段）时，对其进⾏加锁，线程A获取到锁以后，任何其他线程如果再次对资源进⾏访问，则将其放到等待队列中，直到线程A释放锁之后，再将线程从队列中取出。

   假设主线程先执行ThreadEntry()⽅法，那么当Record()方法抛出异常之后，将不会执行Monitor.Exit()方法。程序的输出结果为：Main[278]，并且会⼀直等待下去。因为worker线程是前台线程，它⼀直在等待释放锁，而主线程直到结束都未释放锁。解决这个问题的⼀个办法是将worker线程设为后台线程，但前面已经说过这是不妥当的做法，⽤这种⽅式结束线程是不够优雅的。还有⼀种⽅法是将Monitor.Exit()方法放到finally块中：

    ```csharp
    void ThreadEntry() {
    	Monitor.Enter(res);
    	try {
    		res.Record();
    	}
    	finally {
    		Monitor.Exit(res);
    	}
    }
    ```

   由于Monitor的这种使用模式太常⽤了，.NET提供了lock语句进行简化，上⾯的代码等价于：

    ```csharp
    void ThreadEntry() {
    	lock (res) {
    		res.Record();
    	}
    }
    ```

   lock语句只专注于获取锁、释放锁，并不提供处理异常的简写⽅法，如果要处理异
   常，还需要使⽤try/catch块：

    ```csharp
    void ThreadEntry() {
    	lock (res) {
    		try {
    			res.Record();
    		} catch {
    		// Anything you like
    		}
    	}
    }
    ```

   ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/b85a9075-b7f6-4ce3-87c5-6ce8586d175f/Untitled.png)

2. 死锁

    ```csharp
    class Program
    {
    	static void LockTooMuch(object lock1, object lock2)
    	{
    		lock (lock1)
    		{
    			Sleep(1000);
    			lock (lock2) { }
    		}
    	}
    	static void Main(string[] args)
    	{
    		object lock1 = new object();
    		object lock2 = new object();
    
    		// 主线程和新线程都在等待对方释放锁，导致它们互相等待对方，从而陷入了死锁状态。
    		// 主线程和新线程都无法继续执行下去，程序被永久阻塞。
    		new Thread(() => LockTooMuch(lock1, lock2)).Start();
    
    		lock (lock2)
    		{
    			Thread.Sleep(1000);
    			WriteLine("Monitor.TryEnter returning false after a specified timeout is elapsed");
    			if (Monitor.TryEnter(lock1, TimeSpan.FromSeconds(5)))
    				WriteLine("Acquired a protected resource succesfully");
    			else
    				WriteLine("Timeout acquiring a resource!");
    		}
    
    		WriteLine("----------------------------------");
    		new Thread(() => LockTooMuch(lock1, lock2)).Start();
    
    		lock (lock2)
    		{
    			WriteLine("This will be a deadlock!");
    			Sleep(1000);
    			lock (lock1)
    				WriteLine("Acquired a protected resource succesfully");
    		}
    	}
    }
    ```

   ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/73cd3153-f000-4832-a277-c3910630b6e0/Untitled.png)

3. 使用互锁函数

    ```csharp
    	class CounterNoLock : CounterBase
    	{
    		private int _count;
    		public int Count => _count;
    		public override void Increment()
    		{
    			// 互斥锁
    			Interlocked.Increment(ref _count);
    		}
    		public override void Decrement()
    		{
    			Interlocked.Decrement(ref _count);
    		}
    	}
    ```

   ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/ec88a9bc-406f-4f61-8ce5-7a5581ce484c/fbddffbf-6719-467f-a01b-cac0bff4df76/Untitled.png)


1. 使用生产者消费者队列