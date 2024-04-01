//using System;

//class Test
//{
//    static void Main()
//    {
//        Console.WriteLine("Hello C#");
//        Type t = 7.GetType();
//        string s = 7.ToString();
//        Console.WriteLine("Type: {0} {1}", t, s);


//        int i = 5;
//        System.Int32 k = 6;
//        uint kk = 70;

//        System.Char u = 'c';
//        Console.WriteLine("Type: {0} {1}", u.GetType(), kk.GetType());
//        Console.WriteLine("Type: {0} {1}", i, k);

//        int[] numbers = { 1, 2, 3, 4, 5 };

//        Console.WriteLine("numbers: {0} {1}", numbers.Length, numbers[3]);

//        const int nArray = 6;

//        int[] arr = new int[nArray];
//        int[] arr2 = new int[3] { 1, 2, 3 };
//        int[] arr3 = new int[nArray] { 1, 2, 3, 4, 5, 6 };

//        foreach(var v in arr3)
//        {
//            Console.WriteLine("Arr {0}", v);
//        }

//        string[] strArr = { "fred", "dela", "nina" };
//        foreach (var v in strArr)
//        {
//            Console.WriteLine("Arr {0}", v);
//        }

//        for (int ia = 0; ia < strArr.Length; ia++)
//        {
//            Console.WriteLine("Arr {0}", strArr[ia]);
//        }

//        string[] ss = new string[4];
//        foreach (var v in ss)
//        {
//            //Console.WriteLine("Arr {0}", v.ToString());
//        }

//        int?[] intArr = { 1, 2, 3, 4, null, 6 };
//        for (int ib =0;ib<strArr.Length;ib++)
//        {
//            Console.WriteLine("int? {0}", ib);
//        }
//        string sFred = "Fred";
//        if(sFred.ToLower()=="fred")
//        {
//            Console.WriteLine("sFred{0}", sFred);
//        }

//    }
//}