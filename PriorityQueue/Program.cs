using System;
using System.Collections.Generic;

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
            p1.Push("butt");
            p1.Push("fran");
            p1.Push("test");

            while (p1.Count() > 0)
            {
                Console.WriteLine($"{p1.Remove()}");
            }
        }
    }

    class PriorityQueue<T>
    {
        private T[] _queue = null;
        private int _size = 10;
        private int _n = 0;
        public enum PQType { Min, Max };
        private PQType _type = PQType.Max;

        public PriorityQueue() {}

        public PriorityQueue(int size)
        {
            _size = size;
        }

        public PriorityQueue(int size, PQType type)
        {
            _size = size;
            _type = type;
        }

        public int Count() => _n;

        public void Push(T value)
        {
            if (_queue == null) _queue = new T[_size];
            // don't use 0 index
            // [0, 1, 2, 3, 4, 5, 6, 7]
            // [0, 1, 2, 3, 4, 5, 6, 7]
            //     1
            //  2     3
            // 4 5   6 7
            // left child = 2*parent_index + 1
            // right child = 2*parent_index + 2

            // Add to end of _queue
            _queue[++_n] = value;
            Swim(_n);
        }

        public T Remove()
        {
            if (_n < 0) throw new IndexOutOfRangeException();
            T max = _queue[1];
            Swap(1, _n--);
            // _queue[_n + 1] = int.MinValue;
            Sink(1);
            return max;
        }

        public void Swim(int k)
        {
            while (k > 1 && Compare(k/2, k))
            {
                Swap(k / 2, k);
                k = k / 2;
            }
        }

        public void Sink(int k)
        {
            while (2*k <= _n)
            {
                int j = 2 * k;
                if (j < _n && Compare(j, j + 1)) j++;
                if (!Compare(k, j)) break;
                Swap(k, j);
                k = j;
            }
        }

        public bool Compare(int parentIndex, int childIndex)
        {
            int result = Comparer<T>.Default.Compare(_queue[parentIndex], _queue[childIndex]);
            return _type == PQType.Max ? result == -1 : result == 1;
        }

        public void Swap(int parentIndex, int childIndex)
        {
            T t = _queue[parentIndex];
            _queue[parentIndex] = _queue[childIndex];
            _queue[childIndex] = t;
        }

        public int LeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex;
        }

        public int RightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        public int ParentIndex(int childIndex)
        {
            return childIndex / 2;
        }        

        public T Peek()
        {
            return _queue[1];
        }
    }
}
