using System;
using System.Collections.Generic;
using FluentAssertions;

namespace Challenges.Aug2020
{
    public class MyHashSet
    {
        public static void Test()
        {
            MyHashSet hashSet = new MyHashSet();
            hashSet.Add(1);
            hashSet.Add(2);
            hashSet.Contains(1).Should().Be(true);
            hashSet.Contains(3).Should().Be(false);
            hashSet.Add(2);
            hashSet.Contains(2).Should().Be(true);
            hashSet.Remove(2);
            hashSet.Contains(2).Should().Be(false);

            hashSet.Add(12653445);
            hashSet.Contains(12653445).Should().Be(true);
            hashSet.Remove(12653445);
            hashSet.Contains(12653445).Should().Be(false);
        }
        
        private const int hashBase = 100;
        private readonly LinkedList<int>[] buckets = new LinkedList<int>[hashBase];
        
        /** Initialize your data structure here. */
        public MyHashSet()
        {

        }

        public void Add(int key)
        {
            if(Contains(key))
                return;

            var hash = GetKeyHash(key);
            var b = buckets[hash];
            if(b == null)
            {
                buckets[hash] = new LinkedList<int>();
                b = buckets[hash];
            }
            b.AddLast(key);
        }

        public void Remove(int key)
        {
            if(!Contains(key))
                return;

            var b = GetKeyBucket(key);
            b.Remove(b.Find(key));
        }

        /** Returns true if this set contains the specified element */
        public bool Contains(int key)
        {
            var b = GetKeyBucket(key);
            if(b == null)
                return false;
            return b.Find(key) != null;            
        }

        private int GetKeyHash(int key) => Math.Abs(key) % hashBase;
        private LinkedList<int> GetKeyBucket(int key) => buckets[GetKeyHash(key)];
    }
}