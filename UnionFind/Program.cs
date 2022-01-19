using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFind
{
    class Program
    {
        static void Main(string[] args)
        {
            var uf = new UnionFind(10);
            uf.Union(0, 1);
            uf.Union(1, 8);
            Console.WriteLine($"0, 8 are connected?: {uf.Connected(0, 8)}");
            Console.WriteLine($"6, 7 are connected?: {uf.Connected(6, 7)}");

            // make a graph 1-2-5-6-7 | 3-8-9 | 4
            var g = new Dictionary<int, List<int>>();
            g[0] = new List<int>();
            g[1] = new List<int> { 2, 7 };
            g[2] = new List<int> { 5, 6 };
            g[3] = new List<int> { 8 };
            g[4] = new List<int>();
            g[5] = new List<int> { 2 };
            g[6] = new List<int> { 2 };
            g[7] = new List<int> { 1 };
            g[8] = new List<int> { 3, 9 };
            g[9] = new List<int> { 8 };

            PrintGraph(g);

            var uf2 = new UnionFind(g.Count);
            var seen = new HashSet<int>();
            foreach (var k in g.Keys)
            {
                foreach(var c in g[k])
                {
                    if (!seen.Contains(c)) uf2.Union(k, c);
                }
                seen.Add(k);
            }

            uf2.Print();

            /*
             
            0 1 1 3 4 2 2 1 3 8
            0 1 2 3 4 5 6 7 8 9

            count connected components?
             
            */

            int connectedComponents = uf2.CountConnectedComponents();
            Console.WriteLine($"connectedComponents: {connectedComponents}");
        }

        static void PrintGraph(Dictionary<int, List<int>> g)
        {
            // print graph
            foreach (var k in g.Keys)
            {
                Console.Write($"{k} -> ");
                foreach (var c in g[k])
                {
                    Console.Write($"{c}, ");
                }
                Console.WriteLine();
            }
        }
    }

    public class UnionFind
    {
        private int[] _parents;

        public UnionFind(int size)
        {
            _parents = new int[size];
            Init();
        }

        /* 
         * Set all of the node's parents to themselves. Every node is its own parent to start
         */
        private void Init()
        {
            for (int i = 0; i < _parents.Length; i++)
            {
                _parents[i] = i;
            }
        }

        public void Print()
        {
            foreach(var p in _parents)
            {
                Console.Write($"{p} ");
            }
            Console.WriteLine();
            for (int i = 0; i < _parents.Length; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }

        public bool Connected(int x, int y) => FindParent(x) == FindParent(y);

        public int CountConnectedComponents()
        {
            int count = 0;
            for (int i = 0; i < _parents.Length; i++)
            {
                if (_parents[i] == i) count++;
            }
            return count;
        }

        /*
        p 0 0 2 3 4 5 6 7 1 9 
        c 0 1 2 3 4 5 6 7 8 9 
         */
        public int FindParent(int child)
        {
            int parent = _parents[child];
            while (parent != child)
            {
                child = parent;
                parent = _parents[child];
            }
            return parent;
        }

        public void Union(int x, int y)
        {
            int parentX = FindParent(x);
            int parentY = FindParent(y);

            if (x == parentX && y == parentY)
            {
                // both are self parented, just pick on and make that the root of a new union
                _parents[y] = x;
                return;
            } 

            if (x == parentX)
            {
                // only x is self parented so y must be part of a union
                _parents[x] = y;
                return;
            }

            // only y is self parented so x must be part of a union
            _parents[y] = x;
        }
    }
}
