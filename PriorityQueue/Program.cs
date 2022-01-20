using System;

namespace PriorityQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var max = 20;
            var r = new Random();
            var p = new PriorityQueue<int>(max);
            for (int i = 1; i < max; i++)
            {
                int val = r.Next(1, 10000);
                Console.WriteLine($"In: {val}");
                p.Push(val);
            }

            for (int i = 1; i < max; i++)
            {
                Console.WriteLine($"Out: {p.Remove()}");
            }

            var p1 = new PriorityQueue<string>(max, PriorityQueue<string>.PQType.Min);
            p1.Push("liz");
            p1.Push("cisco");
            p1.Push("tr");
            p1.Push("ty");
            p1.Push("dog");
            p1.Push("zzzz");
            p1.Push("laaaaiz");
            p1.Push("aaaaa");
            p1.Push("gggg");
            p1.Push("kkk");
            p1.Push("ddd");
            p1.Push("off");
            p1.Push("fran");
            p1.Push("test");

            while (p1.Count() > 0)
            {
                Console.WriteLine($"{p1.Remove()}");
            }
        }
    }
}
