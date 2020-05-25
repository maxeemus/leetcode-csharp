using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1
{
    class NoRepeatSubstring
    {
        public void Test()
        {
            Debug.Assert(LengthOfLongestSubstring("") == 0);
            Debug.Assert(LengthOfLongestSubstring("a") == 1);
            Debug.Assert(LengthOfLongestSubstring("aabccbb") == 3);
            Debug.Assert(LengthOfLongestSubstring("abbbb") == 2);
            Debug.Assert(LengthOfLongestSubstring("abccde") == 3);
            Debug.Assert(LengthOfLongestSubstring("abcabcbb") == 3);
            Debug.Assert(LengthOfLongestSubstring("bbbbb") == 1);
            Debug.Assert(LengthOfLongestSubstring("pwwkew") == 3);
        }   

        public int LengthOfLongestSubstring(string s) 
        {
            if(string.IsNullOrEmpty(s))
                return 0;
            int start = 0;
            int end = 0;
            int max = 0;
            Dictionary<char, int> charsLastPositions = new Dictionary<char, int>();
            for(end = 0; end < s.Length; end++)
            {
                char c = s[end];
                if(charsLastPositions.ContainsKey(c))
                    start = Math.Max(start, charsLastPositions[c] + 1);
                charsLastPositions[c] = end;
                max = Math.Max(max, end - start + 1);
            }
            return max;
        }     
    }
}