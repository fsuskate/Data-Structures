using System;
using System.Collections.Generic;

namespace DjistraAlgo
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var p = new Program();
            p.Djistra();

            
        }

        void Print<T>(IList<T> a, string title)
        {
            Console.WriteLine(title);
            for (int i = 0; i < a.Count; i++)
            {
                Console.Write($"{i},");
            }
            Console.WriteLine();

            for (int i = 0; i < a.Count; i++)
            {
                Console.Write($"{a[i]},");
            }
            Console.WriteLine();
        }

        void Djistra()
        {
            var g = BuildGraph();

            int[] distances = new int[g.Keys.Count+1];
            int[] previouses = new int[g.Keys.Count+1];
            bool[] visited = new bool[g.Keys.Count+1];


            Array.Fill(distances, int.MaxValue);
            Array.Fill(visited, false);
            Array.Fill(previouses, -1);

            var heap = new SortedSet<Node>(new MyComp());
            distances[1] = 0; // distance to source is always 0
            heap.Add(new Node { id = 1, cost = 0 });

            while (heap.Count > 0)
            {
                var currNode = heap.Min;
                heap.Remove(heap.Min);

                if (visited[currNode.id ]) continue;
                visited[currNode.id ] = true;
                foreach (var neighbor in g[currNode.id])
                {
                    var nextDistance = currNode.cost + neighbor.cost;
                    if (!visited[neighbor.id] && nextDistance < distances[neighbor.id])
                    {
                        distances[neighbor.id ] = nextDistance;
                        previouses[neighbor.id ] = currNode.id;
                        heap.Add(new Node { id = neighbor.id, cost = nextDistance });
                    }
                }
            }

            Print(distances, "Distances");
            Print(previouses, "previouses");
            Print(visited, "visited");
        }

        Dictionary<int, List<Node>> BuildGraph()
        {
            var g = new Dictionary<int, List<Node>>();
            g.Add(1, new List<Node>());
            g.Add(2, new List<Node>());
            g.Add(3, new List<Node>());
            g.Add(4, new List<Node>());

            g[1].Add(new Node { id = 2, cost = 7 });
            g[1].Add(new Node { id = 3, cost = 9 });
            g[1].Add(new Node { id = 4, cost = 14 });

            g[2].Add(new Node { id = 3, cost = 10 });

            g[3].Add(new Node { id = 4, cost = 2 });
            return g;
        }
    }

    class Node
    {
        public int id;
        public int cost;
    }    

    class MyComp : IComparer<Node>
    {
        int IComparer<Node>.Compare(Node x, Node y)
        {
            return x.cost - y.cost; // cannot ever equal 0, must decide who wins if equal, take x
        }
    }
}
