using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionTests
{
    public interface ISomeInterface
    {
        int GetNum();
    }

    public class SomeClass : ISomeInterface
    {
        public int GetNum()
        {
            return 5;
        }
    }

    public class SomeClass2<TSomeType> where TSomeType : ISomeInterface
    {
        TSomeType data;

        public SomeClass2(TSomeType data1)
        {
            data = data1;
        }

        public int GetNum()
        {
            return data.GetNum();
        }
    }

    public interface ITest
    {
        void Print();
    }

    public class Test1 : ITest
    {
        public Test2 example { get; set; }

        public Test1(Test2 example)
        {
            this.example = example;
        }

        public Test1()
        {
        }

        public void Print()
        {
            Console.WriteLine("Test1");
            if (example != null)
                example.Print();
        }
    }

    public class Test2 : ITest
    {
        public Test3 example { get; set; }

        public Test2(Test3 example)
        {
            this.example = example;
        }

        public Test2()
        {
        }

        public void Print()
        {
            Console.WriteLine("Test2");
            if (example != null)
                example.Print();
        }
    }

    public class Test3
    {
        public Test1 example { get; set; }

        public Test3(Test1 example)
        {
            this.example = example;
        }

        public Test3()
        {
        }

        public void Print()
        {
            Console.WriteLine("Test3 with link to Test1");
        }
    }
}
