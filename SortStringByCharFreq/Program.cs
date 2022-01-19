using System;
using System.Collections.Generic;
using System.Text;

namespace SortStringByCharFreq
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

            var input = Console.ReadLine();

            var res = p.SortByFreq(input);

            Console.WriteLine($"sorted: {res}");
        }

        public static Dictionary<char, int> _charToFreq;

        public void InitCharFreqDict()
        {
            if (_charToFreq == null)
            {
                _charToFreq = new Dictionary<char, int>();
            }
            else
            {
                _charToFreq.Clear();
            }
        }

        string SortByFreq(string str)
        {
            InitCharFreqDict();

            foreach (var c in str)
            {
                if (_charToFreq.ContainsKey(c)) _charToFreq[c]++;
                else _charToFreq.Add(c, 1);
            }

            var sd = new SortedDictionary<char, int>(new MyComp());
            foreach (var k in _charToFreq.Keys)
            {
                sd.Add(k, _charToFreq[k]);
            }

            var sb = new StringBuilder();
            foreach (var kvp in sd)
            {
                for (int j = 0; j < kvp.Value; j++)
                {
                    sb.Append(kvp.Key);
                }
            }

            return sb.ToString();
        }

        private class MyComp : IComparer<char>
        {
            int IComparer<char>.Compare(char x, char y)
            {
                var xCount = Program._charToFreq[x];
                var yCount = Program._charToFreq[y];
                return (xCount - yCount == 0) ? 1 : yCount - xCount;
            }
        }
    }
}
