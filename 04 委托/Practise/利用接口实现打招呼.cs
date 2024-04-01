//using System;

//// 1. 利用接口实现打招呼


//// IGreeting接口声明，其中有一个成员方法
//public interface IGreeting
//{
//    // 接口的成员方法声明
//    void GreetingPeople(string name);
//}


///*
// * EnglishGreeting_cls 类声明
// * 实现了 IGreeting 接口
// */
//public class EnglishGreeting_cls : IGreeting
//{
//    // 实现接口
//    public void GreetingPeople(string name)
//    {
//        Console.WriteLine("Morning: " + name);
//    }
//}

///*
// * ChineseGreeting_cls 类声明
// * 实现了 IGreeting 接口
// */
//public class ChineseGreeting_cls : IGreeting
//{
//    public void GreetingPeople(string name)
//    {
//        Console.WriteLine("早上好: " + name);
//    }
//}

//public class Program
//{
//    /*
//     * IGreeting makegreeting接口类型的参数，表示一个实现了 IGreeting 接口的对象。
//     * 接口是为了实现类而存在的，而不是为了实例化接口本身。
//     * 无论传入的对象是什么类的实例，只要它实现了 IGreeting 接口，都可以调用 GreetingPeople 方法来向人们打招呼。
//     * 也就是说实参可以是任意一个实现接口类的对象
//     */
//    static void GreetPeopleWithInterface(string name, IGreeting makegreeting)
//    {
//        makegreeting.GreetingPeople(name);
//    }

//    static void TestInterface()
//    {
//        GreetPeopleWithInterface("Fred", new EnglishGreeting_cls());
//        GreetPeopleWithInterface("Fred", new ChineseGreeting_cls());
//    }

//    static void Main()
//    {
//        TestInterface();
//    }
//}
