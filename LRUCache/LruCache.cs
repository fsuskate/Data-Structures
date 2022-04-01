using System.Collections.Generic;

namespace LRUCache
{
    public class LruCache<T,T2>
    {
        private class CacheItem<Tk,Tv> { public Tk k; public Tv v; }
        private int capacity;
        private Dictionary<T, LinkedListNode<CacheItem<T,T2>>> d;
        private LinkedList<CacheItem<T, T2>> ll;
        /* 

        */
        public LruCache(int capacity)
        {
            this.capacity = capacity;
            d = new Dictionary<T, LinkedListNode<CacheItem<T,T2>>>();
            ll = new LinkedList<CacheItem<T,T2>>();
        }

        public T2 Get(T key)
        {
            T2 r = default(T2);
            if (d.ContainsKey(key))
            {
                r = d[key].Value.v;
                Promote(key);
            }
            return r;
        }

        public void Put(T key, T2 value)
        {
            if (d.ContainsKey(key))
            {
                Update(key, value);
                Promote(key);
            } 
            else
            {
                if (d.Keys.Count == capacity)
                {
                    Evict();
                }

                var lln = new LinkedListNode<CacheItem<T, T2>>(new CacheItem<T,T2> { k=key, v=value } );
                d.Add(key, lln);
                ll.AddFirst(lln);
            }
        }

        public List<T2> GetLruList()
        {
            List<T2> r = new List<T2>();
            foreach (var i in ll)
            {
                r.Add(i.v);
            }
            return r;
        }

        private void Update(T k, T2 v)
        {
            var lln = d[k];
            lln.Value.v = v;
        }

        private void Promote(T k)
        {
            var lln = d[k];
            ll.Remove(lln);
            ll.AddFirst(lln);
        }

        private void Evict()
        {
            var lln = ll.Last;
            ll.Remove(lln);
            d.Remove(lln.Value.k);
        }
    }

    /**
     * Your LRUCache object will be instantiated and called as such:
     * LRUCache obj = new LRUCache(capacity);
     * int param_1 = obj.Get(key);
     * obj.Put(key,value);
     */
}
