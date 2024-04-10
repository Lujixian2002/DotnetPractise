using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

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