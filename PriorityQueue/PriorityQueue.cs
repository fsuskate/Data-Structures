using System;
using System.Collections.Generic;

namespace PriorityQueue
{
    class PriorityQueue<T>
    {
        private T[] _queue = null;
        private int _size = 10;
        private int _currentIndex = 0;
        public enum PQType { Min, Max };
        private PQType _type = PQType.Max;

        public PriorityQueue() { }

        public PriorityQueue(int size)
        {
            _size = size;
        }

        public PriorityQueue(int size, PQType type)
        {
            _size = size;
            _type = type;
        }

        public int Count() => _currentIndex;

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
            _queue[++_currentIndex] = value;
            Swim(_currentIndex);
        }

        public T Remove()
        {
            if (_currentIndex < 0) throw new IndexOutOfRangeException();
            T max = _queue[1];
            Swap(1, _currentIndex--);
            // _queue[_n + 1] = int.MinValue;
            Sink(1);
            return max;
        }

        public void Swim(int childIndex)
        {
            while (childIndex > 1 && Compare(ParentIndex(childIndex), childIndex))
            {
                Swap(ParentIndex(childIndex), childIndex);
                childIndex = ParentIndex(childIndex);
            }
        }

        public void Sink(int parentIndex)
        {
            while (LeftChildIndex(parentIndex) <= _currentIndex)
            {
                int leftChildIndex = LeftChildIndex(parentIndex);
                if (leftChildIndex < _currentIndex && Compare(leftChildIndex, leftChildIndex + 1)) leftChildIndex++;
                if (!Compare(parentIndex, leftChildIndex)) break;
                Swap(parentIndex, leftChildIndex);
                parentIndex = leftChildIndex;
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
