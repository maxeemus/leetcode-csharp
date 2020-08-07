using System;
using System.Collections.Generic;

namespace ConsoleApp1.Common
{
    public class MinHeap<T>
    {        
        private readonly IComparer<T> comparer;
        public readonly List<T> list = new List<T>();
        public int Count => list.Count;

        public MinHeap(IComparer<T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));
            this.comparer = comparer;
        }

        public void Enqueue(T value)
        {
            list.Add(value);
            int last = list.Count - 1;
            while (last > 0)
            {
                int p = (last - 1) / 2;
                if (comparer.Compare(list[last], list[p]) >= 0)
                    break;
                (list[last], list[p]) = (list[p], list[last]);
                last = p;
            }
        }

        public T Dequeue()
        {
            int last = list.Count - 1;
            T min = Peek();
            list[0] = list[last];
            list.RemoveAt(last);

            last = list.Count - 1;
            int p = 0;
            while (true)
            {
                int c = p * 2 + 1;
                if (c > last)
                    break;
                int rc = c + 1;
                if (rc <= last && comparer.Compare(list[rc], list[c]) < 0)
                    c = rc;
                if (comparer.Compare(list[p], list[c]) <= 0)
                    break;
                (list[p], list[c]) = (list[c], list[p]);
                p = c;
            }
            return min;
        }
        public T Peek()
        {
            if (list.Count == 0) throw new InvalidOperationException("Queue is empty.");
            return list[0];
        }
    }
}