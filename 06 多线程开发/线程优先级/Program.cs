﻿using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;
using System.Diagnostics;

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
