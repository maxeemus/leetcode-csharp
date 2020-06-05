using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Amazon
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }


    public class MergeKListsSolution
    {
        public static void Test()
        {
            var merged = new MergeKListsSolution().MergeKLists(new[]
            {
                new ListNode(2, new ListNode(6)),
                new ListNode(1, new ListNode(4, new ListNode(5))),
                new ListNode(1, new ListNode(3, new ListNode(4))),
            });
        }
        public ListNode MergeKLists(ListNode[] lists)
        {
            if(lists == null || lists.Length == 0)
                return null;
            var pq = new PriorityQueue<ListNode>(Comparer<ListNode>.Create((x, y) => x.val - y.val));
            foreach(var l in lists.Where(i => i != null))
            {
                pq.Enqueue(l);
            }
            var current = new ListNode(0);
            var head = current;
            while(pq.Count > 0)
            {
                var minNode = pq.Dequeue();
                current.next = new ListNode(minNode.val);
                current = current.next;
                if(minNode.next != null)                
                    pq.Enqueue(minNode.next);                
            }

            return head.next;
        }
    }
}