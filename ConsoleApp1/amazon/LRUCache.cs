using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1.Amazon
{

    public class LRUCache
    {
        internal static void Test()
        {
            var cache = new LRUCache(2);
            cache.Put(1, 100);
            cache.Put(2, 200);
            Debug.Assert(cache.Get(1) == 100);
            cache.Put(3, 300);    // evicts key 2
            Debug.Assert(cache.Get(2) == -1);
            cache.Put(4, 400);    // evicts key 1
            Debug.Assert(cache.Get(1) == -1);
            Debug.Assert(cache.Get(3) == 300);
            Debug.Assert(cache.Get(4) == 400);

        }
        private readonly int capacity;
        private readonly LinkedList<(int k, int v)> linkedList;
        private readonly Dictionary<int, LinkedListNode<(int k, int v)>> keyListNodeMap;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            this.linkedList = new LinkedList<(int k, int v)>();            
            this.keyListNodeMap = new Dictionary<int, LinkedListNode<(int k, int v)>>(capacity);
        }

        public int Get(int key)
        {
            if(!keyListNodeMap.ContainsKey(key))
                return -1;

            var value = keyListNodeMap[key].Value.v;
            // move requested at the end of linked list as recently used
            linkedList.Remove(keyListNodeMap[key]);
            keyListNodeMap[key] = linkedList.AddLast((key, value));
            return value;
        }

        public void Put(int key, int value)
        {        
            if(!keyListNodeMap.ContainsKey(key))            
            {
                // add new at the end of linked list as recently used
                keyListNodeMap.Add(key, linkedList.AddLast((key, value)));
            }
            else
            {                
                // move existing at the end of linked list as recently used
                linkedList.Remove(keyListNodeMap[key]);
                keyListNodeMap[key] = linkedList.AddLast((key, value));
            }

            // remove least recently used if over capacity
            if(linkedList.Count > capacity)
            {
                keyListNodeMap.Remove(linkedList.First.Value.k);
                linkedList.RemoveFirst();
            }
            
        }
    }
}