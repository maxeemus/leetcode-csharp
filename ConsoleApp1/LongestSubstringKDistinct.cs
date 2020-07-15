using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    // 7_Fruits_into_Baskets__medium_
    // https://leetcode.com/problems/longest-substring-with-at-most-k-distinct-characters
    class LongestSubstringKDistinct
    {
        public void Test()
        {
            Debug.Assert(LengthOfLongestSubstringKDistinct("araaci", 2) == 4);
            Debug.Assert(LengthOfLongestSubstringKDistinct("araaci", 1) == 2);
            Debug.Assert(LengthOfLongestSubstringKDistinct("cbbebi", 3) == 5);
        }
 
        public int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            if ((s?.Length ?? 0) == 0)
                return 0;
            if (k == 0)
                return 0;

            Dictionary<char, int> distCharCounts = new Dictionary<char, int>();
            int left = 0, righ = 0, max = 0;
            for (righ = 0; righ < s.Length; righ++)
            {
                if (!distCharCounts.ContainsKey(s[righ]))
                    distCharCounts[s[righ]] = 0;
                distCharCounts[s[righ]] += 1;

                while (distCharCounts.Count > k && righ > left)
                {
                    distCharCounts[s[left]] -= 1;
                    if (distCharCounts[s[left]] == 0)
                        distCharCounts.Remove(s[left]);
                    left++;
                }
                max = Math.Max(max, righ - left + 1);
            }

            return max;
        }

        public int TotalFruit(int[] tree)
        {
            int k = 2;
            if ((tree?.Length ?? 0) < k)
                return 0;

            Dictionary<int, int> distCharCounts = new Dictionary<int, int>();
            int start = 0, end = 0, max = 0;
            for (end = 0; end < tree.Length; end++)
            {
                if (!distCharCounts.ContainsKey(tree[end]))
                    distCharCounts[tree[end]] = 0;
                distCharCounts[tree[end]] += 1;

                while (distCharCounts.Count > k && end > start)
                {
                    distCharCounts[tree[start]] -= 1;
                    if (distCharCounts[tree[start]] == 0)
                        distCharCounts.Remove(tree[start]);
                    start++;
                }
                max = Math.Max(max, end - start + 1);
            }

            return max;
        }
    }
}