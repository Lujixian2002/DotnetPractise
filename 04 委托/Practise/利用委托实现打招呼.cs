//using System;

////2. 利用委托实现打招呼

//// 利用delegate定义了一个委托类型，实例化对象会是一个函数指针
//public delegate void GreetingDelegate(string name);


//public class Program
//{
//    /*
//     * 定义了两个方法，分别是用英语打招呼以及用中文打招呼
//     * 可以用指针指向这两个函数
//     */
//    static void EnglishGreeting(string name)
//    {
//        Console.WriteLine("Morning: " + name);
//    }
//    static void ChineseGreeting(string name)
//    {
//        Console.WriteLine("早上好: " + name);
//    }

//    static void GreetPeople(string name, GreetingDelegate fn)
//    {
//        // fn是一个函数指针
//        fn(name);
//    }


//    static void Main()
//    {
//        Console.WriteLine("利用委托进行打招呼");
//        GreetPeople("Fred", EnglishGreeting);
//        GreetPeople("Fred", ChineseGreeting);

//        Console.WriteLine("/n测试委托各种操作");
//        GreetingDelegate d;
//        d = EnglishGreeting;
//        d += ChineseGreeting;
//        GreetPeople("Fred", d);
//        Console.WriteLine("-----------------");
//        d += EnglishGreeting;
//        GreetPeople("Fred", d);
//    }
//}
