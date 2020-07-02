using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;

namespace ConsoleApp1.Amazon
{
    // https://leetcode.com/problems/partition-labels/
    public class PartitionLabelsSolution
    {
        public static void Test()
        {
            new PartitionLabelsSolution().PartitionLabels("").Should().BeEquivalentTo(new int[0]);
            new PartitionLabelsSolution().PartitionLabels("a").Should().BeEquivalentTo(new[] {1});
            new PartitionLabelsSolution().PartitionLabels("aaa").Should().BeEquivalentTo(new[] {3});
            new PartitionLabelsSolution().PartitionLabels("aaab").Should().BeEquivalentTo(new[] {3, 1});
            new PartitionLabelsSolution().PartitionLabels("aaaba").Should().BeEquivalentTo(new[] {5});
            new PartitionLabelsSolution().PartitionLabels("ababcbacadefegdehijhklij").Should().BeEquivalentTo(new[] { 9, 7, 8 });
            new PartitionLabelsSolution().PartitionLabels("ababcbacadefegdehijhklija").Should().BeEquivalentTo(new[] { 25 });
            System.Console.WriteLine("PartitionLabels is done.");
        }

        public IList<int> PartitionLabels(string S)
        {
            var result = new List<int>();
            if(string.IsNullOrEmpty(S))
                return result;

            // Get all symbold max pos
            var charMaxPos = new Dictionary<char, int>();            
            for(int i = 0; i < S.Length; i++)
            {
                if(!charMaxPos.ContainsKey(S[i]))   
                    charMaxPos.Add(S[i], i);
                else
                    charMaxPos[S[i]] =  i;
            }

            // define partitions
            int max = 0;
            int anchor = 0;
            for(int i = 0; i < S.Length; i++)
            {
                max = Math.Max(max, charMaxPos[S[i]]);
                if(max == i)
                {
                    result.Add(i - anchor + 1);
                    anchor = i + 1;
                }
            }

            return result;
        }
    }
}