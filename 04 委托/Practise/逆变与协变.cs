using System;

//方法 字段 类型
//private、protected、public、internal

//仅仅类型
//abstract、sealed

//字段
//static、readonly

//方法修饰
//实例方法
//static、virtual 、new、override、abstract、sealed


// abstract 关键字用于定义抽象类，表示该类不能被实例化，只能被继承。
abstract class BaseClass
{
    // virtual 关键字用于定义虚方法，表示该方法可以被子类重写。
    public virtual void vf()
    {
        Console.WriteLine("In BaseClass vf");
    }

    public virtual void vf_new()
    {
        Console.WriteLine("In BaseClass vf_new");
    }

    // abstract 关键字用于定义抽象方法，表示该方法没有具体实现，必须在子类中被重写。
    public abstract void vf_abstract();


    // sealed 关键字用于防止方法被重写，表示该方法在子类中 不能被重写 。
    public sealed override string ToString()
    {
        Console.WriteLine("In BaseClass vf_sealed");
        return "BaseClass_ToString";
    }
}

// internal 关键字用于限制类的可见性，表示该类只能在当前程序集内部访问。
internal class DerivedClass : BaseClass
{
    public override void vf()
    {
        Console.WriteLine("In DerivedClass vf");
    }
    // vf_new() 方法是一个新的方法，与基类的同名方法不相关
    public new void vf_new()
    {
        Console.WriteLine("In DerivedClass vf_new");
    }
    public override void vf_abstract()
    {
        Console.WriteLine("In DerivedClass vf_abstract");
    }
    /*
	public sealed override string ToString()
	{
		Console.WriteLine("In BaseClass vf_sealed");
		return "BaseClass_ToString";
	}
	*/
}

class Test
{
    // 声明 TestDelegate 委托类型，并将其声明为协变 out
    delegate void TestDelegate<T>(T t);

    // 声明 BaseFn 方法
    static void BaseFn<T>(T b) where T : BaseClass
    {
        Console.WriteLine("In BaseFn");
        // 调用传入对象的 vf 方法
        b.vf();
    }

    // 声明 DerivedFn 方法，接受 out 参数类型，并且要求 T 是 DerivedClass 或其基类
    static void DerivedFn<T>(T d) where T : DerivedClass
    {
        Console.WriteLine("In DerivedFn");
    }

    static void Main()
    {
        DerivedClass o2 = new DerivedClass();
        // 一个子类的引用赋值给子类的引用
        BaseClass o = o2;

        // 泛型与委托
        TestDelegate<BaseClass> bfn = BaseFn;
        TestDelegate<DerivedClass> dfn = DerivedFn;

        //// 实现逆变  -> 失败，无法这样转换
        //TestDelegate<BaseClass> bfn = DerivedFn;
        //// 实现协变
        //TestDelegate<DerivedClass> dfn = BaseFn;

        // 这两个类型之间没啥关系
        // 这是泛型带来的问题
        // 所以提出一个变体(协变[out]/ 逆变[in])来解决这个问题
        // 协变out: 父类【接口或者委托】 <= 子类    变更加特定
        // 逆变in: 子类【接口或者委托】 <= 父类     变更加通用

        Console.WriteLine("bfn(o2)");
        bfn(o2);
        //bfn = dfn;
        // dfn = bfn;

        Console.WriteLine("dfn(o2)");
        dfn(o2);
    }
}

