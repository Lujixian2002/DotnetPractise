//using System;

//public class SomeType
//{
//    // 嵌套类
//    class SomeNestType
//    {
//        public void f()
//        {
//            Console.WriteLine("f");
//        }
//    }

//    // 常数
//    const Int32 SomeConstant = 1000;

//    // 只读
//    public readonly Int32 SomeReadOnlyFiled = 2;

//    // 静态
//    static Int32 SomeReadWriteFiled = 3;

//    // 类型构造器
//    static SomeType()
//    {
//        Console.WriteLine("Static SomeType");
//    }

//    // 实例构造
//    public SomeType()
//    {
//        Console.WriteLine("Inst SomeType");
//        SomeReadOnlyFiled = 100;
//    }
//    public SomeType(Int32 x)
//    { }

//    // 析构
//    ~SomeType()
//    {
//        Console.WriteLine("~SomeType");
//    }

//    // 实例属性
//    int II
//    {
//        get; set;
//    }

//    int F
//    {
//        get { return SomeReadOnlyFiled; }
//    }

//    // 实例事件
//    public event EventHandler SomeEvent;

//    static void M1(Object sender, EventArgs e)
//    {
//        SomeType o = (SomeType)sender;
//        Console.WriteLine("M1: {0}", o.SomeReadOnlyFiled);
//    }

//    void M2(Object sender, EventArgs e)
//    {
//        SomeType o = (SomeType)sender;
//        Console.WriteLine("M2: {0}", o.SomeReadOnlyFiled);
//    }

//    void Trigger()
//    {
//        SomeEvent(this, null);
//    }




//    static void Main()
//    {
//        SomeType s = new SomeType();

//        Console.WriteLine("Hi");

//        SomeNestType o = new SomeNestType();
//        o.f();

//        Console.WriteLine("ToString: {0}", s);
//        Console.WriteLine("SomereadOnlyFiled: {0}", s.SomeReadOnlyFiled);
//        Console.WriteLine("SomeReadWriteFiled: {0}", SomeReadWriteFiled);
//        Console.WriteLine("SomeConstant: {0}", SomeConstant);


//        s.MyF(300);

//        s.II = 9;
//        Console.WriteLine("Hi {0} {1}", s.II, s.F);

//        s.SomeEvent += SomeType.M1;
//        s.SomeEvent += s.M2;

//        s.Trigger();

//    }

//}

//public static class SomeExtent
//{
//    public static void MyF(this SomeType e, int nn)
//    {
//        Console.WriteLine("MyF {0} {1}", nn, e.SomeReadOnlyFiled);
//    }
//}