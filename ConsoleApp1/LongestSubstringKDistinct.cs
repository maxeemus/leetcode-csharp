using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class LongestSubstringKDistinct
    {
        public void Test()
        {
            Debug.Assert(FindLength("araaci", 2) == 4);
            Debug.Assert(FindLength("araaci", 1) == 2);
            Debug.Assert(FindLength("cbbebi", 3) == 5);
        }

        public static int FindLength(string str, int k) 
        {
            if((str?.Length ?? 0) < k)
                return 0;

            Dictionary<char, int> distCharCounts = new Dictionary<char, int>();
            int start = 0, end = 0, max = 0;
            for(end = 0; end < str.Length; end++)
            {                
                if(!distCharCounts.ContainsKey(str[end]))
                    distCharCounts[str[end]] = 0;
                distCharCounts[str[end]] += 1;

                while(distCharCounts.Count > k && end > start)
                {
                    distCharCounts[str[start]] -= 1;
                    if(distCharCounts[str[start]] == 0)
                        distCharCounts.Remove(str[start]);
                    start++;
                }
                max = Math.Max(max, end - start + 1);
            }

            return max;
        }
    }
}