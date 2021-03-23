using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Common
{
    // MinHeap - Min Priority Queue
    // use list as a heap tree representation
    public class MinHeap<T>
    {        
        private readonly IComparer<T> comparer;
        public readonly List<T> list = new List<T>();
        public int Count => list.Count;

        public MinHeap() : this(Comparer<T>.Default) {}
        
        public MinHeap(IComparer<T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));
            this.comparer = comparer;
        }

        public void Enqueue(T value)
        {
            // add new value to the end of a heap list
            list.Add(value);
            int valuePos = list.Count - 1;
            while (valuePos > 0)
            {
                int valueParentPosition = (valuePos - 1) / 2;
                // if parent less that new value then stop else swap parent and new value and set valuePos to parent
                if (comparer.Compare(list[valuePos], list[valueParentPosition]) >= 0)
                    break;
                else
                {
                    (list[valuePos], list[valueParentPosition]) = (list[valueParentPosition], list[valuePos]);
                    valuePos = valueParentPosition;
                }
            }
        }        

        public T Dequeue()
        {
            int last = list.Count - 1;
            T min = Peek();
            list[0] = list[last];
            list.RemoveAt(last);

            last = list.Count - 1;
            int parentPos = 0;
            
            while (true)
            {
                int leftChildPos = parentPos * 2 + 1;
                int rightChildPos = leftChildPos + 1;
                int minChildPos = -1;
                
                if (leftChildPos > last)
                    break;
                                
                if (rightChildPos <= last && comparer.Compare(list[rightChildPos], list[leftChildPos]) < 0)
                    minChildPos = rightChildPos;
                else
                    minChildPos = leftChildPos;

                // parent <= minChild then stop else swap parent and min child and set parent to min child
                if (comparer.Compare(list[parentPos], list[minChildPos]) <= 0)
                    break;
                else
                {
                    (list[parentPos], list[minChildPos]) = (list[minChildPos], list[parentPos]);
                    parentPos = minChildPos;
                }
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