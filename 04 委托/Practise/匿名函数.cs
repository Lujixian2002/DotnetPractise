//using System;

////通用委托,.NET 3.5定义一组通用委托

////表示有n个参数（参数类型可以不同），但没有返回值的委托
////public delegate void Action<T1,T2,T3,T4,...>(T1 arg1,T2 arg2) 

////表示有n个参数（参数类型可以不同）且有一个返回值的委托（返回值类型为TResult）
////public delegate TResult Func<T1,T2,T3,T4,...,TResult>(T1 arg1,) 

//class Test
//{
//    static Func<int, int, int> someFunc(int one)
//    {
//        int two = 500;

//        //Func<int, int, int> fn = (o1, o2) =>
//        //  {
//        //      two += one;
//        //      return one + two + o1 + o2;
//        //  };
//        //return fn;

//        //返回函数
//        return (o1, o2) =>
//        {
//            // 使用外部变量,one,two是外部变量
//            // 每次调用函数 fnOne 时，外部变量 two 都会被修改，
//            // 因为它是在 someFunc 方法内部定义的，并且在函数内部被修改。
//            // 因为共享一个作用域

//            two += one;
//            return one + two + o1 + o2;
//        };
//    }

//    static void Main()
//    {
//        Console.WriteLine("返回函数,使用外部变量");
//        Func<int, int, int> fnOne = someFunc(500);     // 500+=500 + 500 + o1 + o2  => 1500 + o1 +o2 ( two =1000 )
//        Func<int, int, int> fnTwo = someFunc(1000);    // 2500 + o1 + o2 (two = 1500)
//        int r1 = fnOne(1, 2);  
//        int r2 = fnTwo(1, 2);
//        Console.WriteLine($"r1: {r1}  r2: {r2}");
//        r1 = fnOne(1, 2);                              // two += 500, 2000+o1+o2
//        r2 = fnTwo(1, 2);                              // two +=1000 3500+o1+o2
//        Console.WriteLine($"r1: {r1}  r2: {r2}");
//    }
//}