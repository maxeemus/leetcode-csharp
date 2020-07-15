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
            new SwapMaxRepeatedCharSubstring().MaxLength("aabacbbbbbb").Should().Be(7);
            new SwapMaxRepeatedCharSubstring().MaxLength("acaa").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength("aabaaabaaaba").Should().Be(7);
            new SwapMaxRepeatedCharSubstring().MaxLength("bbababaaaa").Should().Be(6);
            new SwapMaxRepeatedCharSubstring().MaxLength("abacda").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength("").Should().Be(0);
            new SwapMaxRepeatedCharSubstring().MaxLength("a").Should().Be(1);
            new SwapMaxRepeatedCharSubstring().MaxLength("ab").Should().Be(1);
            new SwapMaxRepeatedCharSubstring().MaxLength("abb").Should().Be(2);
            new SwapMaxRepeatedCharSubstring().MaxLength("abcdef").Should().Be(1);
            new SwapMaxRepeatedCharSubstring().MaxLength("aba").Should().Be(2);
            new SwapMaxRepeatedCharSubstring().MaxLength("aabac").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength("ababa").Should().Be(3);
            new SwapMaxRepeatedCharSubstring().MaxLength("aaabaaa").Should().Be(6);
            new SwapMaxRepeatedCharSubstring().MaxLength("aaabbaaa").Should().Be(4);
            new SwapMaxRepeatedCharSubstring().MaxLength("aaaaa").Should().Be(5);
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
            int left = 0, righ = 0, max = 0;
            bool isSwapped = false;
            int swappingPos = -1;
            int swappedPos = -1;
            for (righ = 0; righ < text.Length; righ++)
            {
                // if been swapped and right is on swapped value
                if (isSwapped && righ == swappingPos)
                {
                    max = Math.Max(max, charCounts.Select(a => a.Value).Max());
                    righ++;
                    if (righ >= text.Length)
                        return max;
                    left = righ;
                    charCounts.Clear();
                    isSwapped = false;
                    swappingPos = -1;
                }
                // increase char count in window
                if (!charCounts.ContainsKey(text[righ]))
                    charCounts[text[righ]] = 0;
                charCounts[text[righ]] += 1;

                if (charCounts.Count > 1) // new char is another
                {
                    // 1 - if not swap -> swap + memorize swapping context
                    if (!isSwapped)
                    {
                        var l = left;
                        var r = righ;
                        // !
                        if (charCounts[text[l]] == charCounts[text[r]] && r + 1 < text.Length && text[r + 1] == text[r])
                            (l, r) = (r, l);
                        //--
                        var (minPos, maxPos) = minmaxPos[text[l]];
                        if (minPos < left) // minPos for swap first
                        {
                            charCounts.Remove(text[r]);
                            charCounts[text[l]] += 1;
                            swappedPos = righ;
                            isSwapped = true;
                        }
                        else if (righ < maxPos) // then maxPos
                        {
                            charCounts.Remove(text[r]);
                            charCounts[text[l]] += 1;
                            swappingPos = maxPos;
                            swappedPos = righ;
                            isSwapped = true;
                        }
                        else
                        {
                            max = Math.Max(max, charCounts.Select(a => a.Value).Max());
                            charCounts.Clear();
                            left = righ;
                            charCounts[text[left]] = 1;
                        }
                    }
                    // 2 - has been swapped -> reset swaping context
                    else
                    {
                        max = Math.Max(max, charCounts.Select(a => a.Value).Max());
                        righ = swappedPos;
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
    }
}