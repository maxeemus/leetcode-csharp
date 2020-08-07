using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApp1.Common;

namespace ConsoleApp1.Amazon
{
    public class KMostWords
    {
        public IList<string> TopKFrequent(string[] words, int k) 
        {            
            if(!(words?.Any() ?? false) || k <= 0)
                return new List<string>(0);
            
            IEnumerable<(string w, int f)> freqs = words.GroupBy(w => w.ToLowerInvariant()).Select(g => (g.Key, g.Count()));

            var pq = new MinHeap<(string w, int f)>(Comparer<(string w, int f)>.Create((x, y) => 
            {
                var c = x.f - y.f;
                if(c == 0)
                    return StringComparer.CurrentCultureIgnoreCase.Compare(y.w, x.w);
                else
                    return c;
            }));
            foreach(var wf in freqs)
            {
                pq.Enqueue(wf);
                if(pq.Count > k)
                    pq.Dequeue();
            }

            var result = new List<string>(pq.Count);
            while(pq.Count > 0)
            {                
                result.Add(pq.Dequeue().w);
            }

            result.Reverse();
            return result;
        }
    }
}