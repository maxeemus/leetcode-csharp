using System;
using System.Collections.Generic;

namespace ConsoleApp1.Amazon
{
    public class PriorityQueue<T>
    {
        #region Test Only
        public static void Test(int numOperations)
        {
            Random rand = new Random(0);
            var pq = new PriorityQueue<int>(Comparer<int>.Create((x, y) => x - y));
            for (int op = 0; op < numOperations; ++op)
            {
                int opType = rand.Next(0, 2);

                if (opType == 0) // enqueue
                {
                    int priority = rand.Next(0, 100);
                    pq.Enqueue(priority);
                    if (pq.IsConsistent() == false)
                    {
                        Console.WriteLine(
                          "Test fails after enqueue operation # " + op);
                    }
                }
                else // Dequeue
                {
                    if (pq.Count > 0)
                    {
                        var e = pq.Dequeue();
                        if (pq.IsConsistent() == false)
                        {
                            Console.WriteLine("Test fails after dequeue operation # " + op);
                        }
                    }
                }
            } // for
            Console.WriteLine("\nAll tests passed");
        }

        public bool IsConsistent()
        {
            if (list.Count == 0) return true;
            int li = list.Count - 1; // last index
            for (int pi = 0; pi < list.Count; ++pi) // each parent index
            {
                int lci = 2 * pi + 1; // left child index
                int rci = 2 * pi + 2; // right child index
                if (lci <= li && comparer.Compare(list[pi], list[lci]) > 0) return false;
                if (rci <= li && comparer.Compare(list[pi], list[rci]) > 0) return false;
            }
            return true; // Passed all checks
        }
        #endregion


        private readonly IComparer<T> comparer;
        public readonly List<T> list = new List<T>();
        public int Count => list.Count;

        public PriorityQueue(IComparer<T> comparer)
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