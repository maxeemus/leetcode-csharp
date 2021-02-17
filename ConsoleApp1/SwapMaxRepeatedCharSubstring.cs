using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ConsoleApp1
{
    // https://leetcode.com/problems/swap-for-longest-repeated-character-substring
    class SwapMaxRepeatedCharSubstring
    {
        public static void Test()
        {
            new SwapMaxRepeatedCharSubstring().MaxLength2("aabacbbbbbb").Should().Be(7);
            new SwapMaxRepeatedCharSubstring().MaxLength2("acaa").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength2("aabaaabaaaba").Should().Be(7);
            new SwapMaxRepeatedCharSubstring().MaxLength2("bbababaaaa").Should().Be(6);
            new SwapMaxRepeatedCharSubstring().MaxLength2("abacda").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength2("").Should().Be(0);
            new SwapMaxRepeatedCharSubstring().MaxLength2("a").Should().Be(1);
            new SwapMaxRepeatedCharSubstring().MaxLength2("ab").Should().Be(1);
            new SwapMaxRepeatedCharSubstring().MaxLength2("abb").Should().Be(2);
            new SwapMaxRepeatedCharSubstring().MaxLength2("abcdef").Should().Be(1);
            new SwapMaxRepeatedCharSubstring().MaxLength2("aba").Should().Be(2);
            new SwapMaxRepeatedCharSubstring().MaxLength2("aabac").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength2("ababa").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength2("aaabaaa").Should().Be(6);
            new SwapMaxRepeatedCharSubstring().MaxLength2("aaabbaaa").Should().Be(4);
            new SwapMaxRepeatedCharSubstring().MaxLength2("aaaaa").Should().Be(5);
        }

        public int MaxLength(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            // 1 - get max and min possitions of each symbol in string
            var minmaxPos = new Dictionary<char, (int min, int max)>();
            for (int i = 0; i < text.Length; i++)
            {
                char s = text[i];
                if (!minmaxPos.ContainsKey(s))
                    minmaxPos.Add(s, (i, i));
                else
                    minmaxPos[s] = (minmaxPos[s].min, i);
            }

            // 2 - sliding window to find longest same char substring 
            Dictionary<char, int> charCounts = new Dictionary<char, int>(2);
            int left = 0, right = 0, max = 0;
            bool isSwapped = false;
            int swappingPos = -1;
            int swappedPos = -1;
            for (right = 0; right < text.Length; right++)
            {
                // if been swapped and right is on swapped value
                if (isSwapped && right == swappingPos)
                {
                    max = Math.Max(max, charCounts.Select(a => a.Value).Max());
                    right++;
                    if (right >= text.Length)
                        return max;
                    left = right;
                    charCounts.Clear();
                    isSwapped = false;
                    swappingPos = -1;
                }
                // increase char count in window
                if (!charCounts.ContainsKey(text[right]))
                    charCounts[text[right]] = 0;
                charCounts[text[right]] += 1;

                if (charCounts.Count > 1) // new char is another
                {
                    // 1 - if not swap -> swap + memorize swapping context
                    if (!isSwapped)
                    {
                        var l = left;
                        var r = right;
                        // !
                        if (charCounts[text[l]] == charCounts[text[r]] && r + 1 < text.Length && text[r + 1] == text[r])
                            (l, r) = (r, l);
                        //--
                        var (minPos, maxPos) = minmaxPos[text[l]];
                        if (minPos < left) // minPos for swap first
                        {
                            charCounts.Remove(text[r]);
                            charCounts[text[l]] += 1;
                            swappedPos = right;
                            isSwapped = true;
                        }
                        else if (right < maxPos) // then maxPos
                        {
                            charCounts.Remove(text[r]);
                            charCounts[text[l]] += 1;
                            swappingPos = maxPos;
                            swappedPos = right;
                            isSwapped = true;
                        }
                        else
                        {
                            max = Math.Max(max, charCounts.Select(a => a.Value).Max());
                            charCounts.Clear();
                            left = right;
                            charCounts[text[left]] = 1;
                        }
                    }
                    // 2 - has been swapped -> reset swaping context
                    else
                    {
                        max = Math.Max(max, charCounts.Select(a => a.Value).Max());                         
                        right = swappedPos;
                        left = swappedPos;                        
                        charCounts.Clear();
                        charCounts[text[left]] = 1;
                        isSwapped = false;
                        swappingPos = -1;                        
                    }
                }
                else
                    max = Math.Max(max, charCounts.Select(a => a.Value).Max());
            }

            return max;
        }

        // better performance
        public int MaxLength2(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            
            // get same char groups: aaabaaa -> {"a", 3}, {"b", 1}, {"c", 3}
            var charGroups = new List<(char c, int l)>();
            char curC = '\0';
            int li = 0;            
            foreach(var c in text)
            {
                if(curC != c)
                {                    
                    curC = c;
                    charGroups.Add((curC, 0));
                    li = charGroups.Count - 1;                    
                }
                charGroups[li] = (charGroups[li].c, charGroups[li].l + 1);
            }
            
            // get chars total count: aaabaaa -> {"a", 6}, {"b", 1}
            var charCounts = new Dictionary<char, int>();
            foreach(var c in text)
            {
                if(!charCounts.ContainsKey(c))
                    charCounts.Add(c, 0);
                charCounts[c]++;
            }
            
            int max = 0;
            // for each group get min of group length + 1 and total symbol for group
            // and then max of all such minimums
            // aaabaaa -> {"a", 3}, {"b", 1}, {"c", 3} / {"a", 6}, {"b", 1}
            // for 'a' min(3 + 1, 6) = 3
            // for 'b' min(1 + 1, 1) = 1
            // result = max(3, 1) = 3
            max = charGroups.Select(s => Math.Min(s.l + 1, charCounts[s.c])).Max();            

            // case when two same char groups are splited by one char group
            // aaabaaa -> min(3 + 1 + 3, 6) = 6
            for(int i = 1; i < charGroups.Count - 1; i++)
            {
                if(charGroups[i].l == 1 && charGroups[i - 1].c == charGroups[i + 1].c)
                    max = Math.Max(max, Math.Min(charGroups[i - 1].l + charGroups[i + 1].l + 1, charCounts[charGroups[i + 1].c]));
            }

            return max;
        }

    }
}