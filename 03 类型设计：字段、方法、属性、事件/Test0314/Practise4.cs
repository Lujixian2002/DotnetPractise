using System;

/*
 *  当一个类被声明为抽象类时，意味着该类不能被实例化，即不能创建该类的对象。
 *  抽象类通常用作其他类的基类，它可能包含抽象方法和非抽象方法。
 *  抽象类可以包含实现了的方法，也可以包含未实现的 抽象方法 。
 */
abstract class BaseClass
{
    public virtual void vf()
    {
        Console.WriteLine("In BaseClass vf");
    }
    public virtual void vf_new()
    {
        Console.WriteLine("In BaseClass vf_new");
    }

    /*
     * 抽象方法是一种没有实现的方法，它只包含方法的声明而没有方法体。
     * 抽象方法必须在抽象类中声明，而且抽象类中至少要有一个抽象方法。
     * 子类必须实现父类中的所有抽象方法，否则子类也必须声明为抽象类。
     */
    public abstract void vf_abstract();

    public sealed override string ToString()
    {
        Console.WriteLine("In BaseClass vf_sealed");
        return "BaseClass_ToString";
    }

}

internal class DerivedClass : BaseClass
{
    public override void vf()
    {
        Console.WriteLine("In DerivedClass vf");
    }

    /*
     * 在当前类中重新定义了一个名为vf_new()的方法，
     * 而不是对基类中的同名方法进行重写。
     */
    public new void vf_new()
    {
        Console.WriteLine("In DerivedClass vf_new");
    }

    // 实现抽象函数
    public override void vf_abstract()
    {
        Console.WriteLine("In DerivedClass vf_abstract");
    }

}



class Test
{
    static void Main()
    {
        System.Console.WriteLine("Hi");
        DerivedClass o2 = new DerivedClass();
        BaseClass o = o2;

        o.vf();
        o.vf_new();
        o.vf_abstract();
        o.ToString();


        o2.vf();
        o2.vf_new();
        o2.vf_abstract();
        o2.ToString();

    }
}