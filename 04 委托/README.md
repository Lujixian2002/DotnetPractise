# 04 委托

# C++函数指针

```cpp
#include<iostream>
using namespace std;

void myFunction(int n,int m)
{
	cout << "this is test for n: " << n << " m:" << m << endl;
}

int main()
{
	void (*funp)(int,int);

	funp = &myFunction;

	(*funp)(12, 14);

}
```

# 委托 —— 安全的函数指针

委托是一种类型

- .NET系统中类型安全的函数指针
    - 类型安全：确保程序在编译时或运行时不会发生类型错误。
    - 支持更加灵活的晚绑定
    - .NET中事件的支持机制
- 但是，委托不仅仅限于函数指针。如果所绑定函数为实例方法，委托将可以维护其所依赖的对象的状态信息。
- 空间分析：委托对象的内存模型

## 委托定义方法：

表示有n个参数（参数类型可以不同），但没有返回值的委托

public delegate void Action<T1,T2,T3,T4,...>(T1 arg1,T2 arg2)

表示有n个参数（参数类型可以不同）且有一个返回值的委托（返回值类型为TResult）

public delegate TResult Func<T1,T2,T3,T4,...,TResult>(T1 arg1,)

## 委托操作

1. **委托链——有序的可调用函数序列**
2. **委托运算：支持合并、移除（+,-）运算**
3. **Delegate与MulticastDelegate**

## 利用委托实现打招呼

```csharp
using System;

// 利用delegate定义了一个委托类型，实例化对象会是一个函数指针
public delegate void GreetingDelegate(string name);

public class Program
{
    /*
     * 定义了两个方法，分别是用英语打招呼以及用中文打招呼
     * 可以用指针指向这两个函数
     */
    static void EnglishGreeting(string name)
    {
        Console.WriteLine("Morning: " + name);
    }
    static void ChineseGreeting(string name)
    {
        Console.WriteLine("早上好: " + name);
    }

    static void GreetPeople(string name, GreetingDelegate fn)
    {
        // fn是一个函数指针
        fn(name);
    }

    static void Main()
    {
        Console.WriteLine("利用委托进行打招呼");
        GreetPeople("Fred", EnglishGreeting);
        GreetPeople("Fred", ChineseGreeting);

        Console.WriteLine("/n测试委托各种操作");
        GreetingDelegate d;
        d = EnglishGreeting;
        d += ChineseGreeting;
        GreetPeople("Fred", d);
        Console.WriteLine("-----------------");
        d += EnglishGreeting;
        GreetPeople("Fred", d);
    }
}

```

# 匿名方法

允许在需要时直接声明方法的实现。匿名方法通常用于在事件处理、委托和 LINQ 查询等情况下提供简洁的语法。

没有名称只有主体的代码

- 匿名方法允许我们以一种“内联”的方式来编写方法代码，将代码直接与委托实例相关联，从而使得委托实例化的工作更加直观和方便。
- 匿名方法的几个相关问题：
    - 参数列表
    - 返回值
    - 外部变量

# Lambda

Lambda 表示式是匿名方法的进一步简化，可以用于定义一个匿名函数，并将其传递给一个委托变量

两种格式：

1. （Input 参数）=> 表达式
2. （Input 参数）=> {语句 1；语句 2；……}

Note:只有一个参数，括号可选，只有一条 return 语句，return可以省略，没有参数，直接使用空（），多个参数直接使用逗号分开

# 逆变与协变

https://www.zhihu.com/question/38861374

---

# Interface 接口

https://www.runoob.com/csharp/csharp-interface.html

接口是为了实现类而存在的，而不是为了实例化接口本身。

无论传入的对象是什么类的实例，只要它实现了 IGreeting 接口，都可以调用 GreetingPeople 方法来向人们打招呼。也就是说实参可以是任意一个实现接口类的对象

```csharp
    /*
     * IGreeting makegreeting接口类型的参数，表示一个实现了 IGreeting 接口的对象。
     */
    static void GreetPeopleWithInterface(string name, IGreeting makegreeting)
    {
        makegreeting.GreetingPeople(name);
    }

    static void TestInterface()
    {
        GreetPeopleWithInterface("Fred", new EnglishGreeting_cls());
        GreetPeopleWithInterface("Fred", new ChineseGreeting_cls());
    }

```

# 泛型，逆变和协变