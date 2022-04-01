using System;
using LRUCache;

namespace LRUCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Demo();
        }

        public void Demo()
        {
            // [[2],[1,1],[2,2],[1],[3,3],[2],[4,4],[1],[3],[4]]
            var c = new LruCache<int,int>(2);
            c.Put(1, 1);
            c.Put(2, 2);
            c.Get(1);
            c.Put(3,3);
            c.Get(2);
            c.Put(4,4);
            c.Get(1);
            c.Get(3);
            c.Get(4);
            foreach (var i in c.GetLruList())
            {
                Console.Write($"{i},");
            }
            Console.WriteLine();

            var c2 = new LruCache<int, string>(3);
            c2.Put(1, "www.yahoo.com");
            c2.Put(2, "www.google.com");
            c2.Get(1);
            c2.Put(3, "www.cnn.com");
            c2.Get(2);
            c2.Put(4, "www.microsoft.com");
            c2.Get(1);
            c2.Get(3);
            c2.Get(4);
            foreach (var i in c2.GetLruList())
            {
                Console.Write($"{i},");
            }
            Console.WriteLine();
        }
    }
}
