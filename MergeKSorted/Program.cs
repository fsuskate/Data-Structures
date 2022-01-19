using System;
using System.Collections.Generic;

namespace MergeKSorted
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Demo();
        }

        void Demo()
        {
            var l = new List<List<int>>
            {
                new List<int> { 10, 30, 50, 70, 90 },
                new List<int> { 12, 16, 100 },
                new List<int> { 40, 80 },
                new List<int> { 1, 6, 17, 23, 71, 99 },
                new List<int> { 3, 14, 45, 62, 77 }
            };

            var res = MergedKSorted(l);
            foreach (var n in res)
            {
                Console.Write($"{n}, ");
            }
            Console.WriteLine();
        }

        List<int> MergedKSorted(List<List<int>> lists)
        {
            var sorted = new SortedSet<int>(new MyComp());

            foreach (var l in lists)
            {
                foreach (var n in l)
                {
                    sorted.Add(n);
                }
            }

            var res = new List<int>();
            while (sorted.Count > 0)
            {
                var curr = sorted.Min;
                res.Add(curr);
                sorted.Remove(curr);
            }

            return res;
        }
    }

    class MyComp : IComparer<int>
    {
        int IComparer<int>.Compare(int x, int y)
        {
            return x - y;
        }
    }
}
