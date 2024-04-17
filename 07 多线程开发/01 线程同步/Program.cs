using System;
using System.Threading;
using static System.Console;

//Mutex 是一种基本的同步方式,只授予一个线程对共享资源的独占访问
class Program
{
	static void Main(string[] args)
	{
		const string MutexName = "CSharpThreadingMutex";

        using (var m = new Mutex(false, MutexName))

            // Mutex(Boolean initiallyOwned, String name)
            // 在这个构造函数里我们除了能指定是否在创建后获得初始拥有权外，
            // 还可以为这个Mutex取一个名字。
            // false 表示创建互斥量时不立即请求拥有它

            //var m = new Mutex(true, MutexName);

		try
		{
			// 如果5s内该实例没有收到信号，该程序没有另外一个实例在运行
			if (!m.WaitOne(TimeSpan.FromSeconds(5)))
				WriteLine("Second instance is running!");
			else
			{
				WriteLine("Running!");
				ReadLine();

				//ReleaseMutex()释放当前Mutex一次
				//m.ReleaseMutex();
			}
		}
		finally
		{
			// 清理资源
			//m.Dispose();
		}
	}
}

//作业(1):
//编程说明如果拥有互斥体的线程没有释放互斥而结束，其他等待线程是否会死等?