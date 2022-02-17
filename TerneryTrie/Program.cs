using System;

namespace TerneryTrie
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
            var tst = new TST();

            tst.Add("she");
            tst.Add("sells");
            tst.Add("shells");
            tst.Add("by");
            tst.Add("the");
            tst.Add("sea");
            tst.Add("share");
            tst.Add("surely");

            tst.Contains("sea");
        }
    }

    public class Node
    {
        public char c;
        public string val;
        public Node less;
        public Node equal;
        public Node more;
    }

    public class TST
    {
        public Node root;

        public void Add(string str)
        {
            if (root == null) root = Add(root, str, 0);
            else Add(root, str, 0);
        }

        public Node Add(Node node, string str, int index)
        {
            var currChar = str[index];

            if (node == null)
            {
                node = new Node { c = currChar };
            }

            if (currChar < node.c)
            {
                node.less = Add(node.less, str, index);
            }
            else if (currChar > node.c)
            {
                node.more = Add(node.more, str, index);
            }
            else if (currChar == node.c && index < str.Length-1)
            {
                node.equal = Add(node.equal, str, index + 1);
            }

            node.val = str;

            return node;
        }

        public bool Contains(string str)
        {
            if (root == null) return false;
            var node = Find(root, str, 0);
            return node.val == str;
        }

        public Node Find(Node node, string str, int index)
        {
            if (node == null) return null;

            var currChar = str[index];

            if (currChar < node.c)
            {
                return Find(node.less, str, index);
            }
            else if (currChar > node.c)
            {
                return Find(node.more, str, index);
            }
            else if (currChar == node.c && index < str.Length-1)
            {
                return Find(node.equal, str, index + 1);
            }

            return node;
        }
    }
}
