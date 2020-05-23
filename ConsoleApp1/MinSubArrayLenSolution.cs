using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class MinSubArrayLenSolution
    {
        public void Test()
        {
            Debug.Assert(MinSubArrayLen(0, new int[0]) == 0);
            Debug.Assert(MinSubArrayLen(100, new[] { 1, 2, 0, 3 }) == 0);
            Debug.Assert(MinSubArrayLen(0, new[] { 1, 2, 0, 3 }) == 1);
            Debug.Assert(MinSubArrayLen(10, new[] { 1, 2, 3, 4 }) == 4);
            Debug.Assert(MinSubArrayLen(8, new[] { 1, 2, 3, 4 }) == 3);
            Debug.Assert(MinSubArrayLen(7, new[] { 2, 3, 1, 2, 4, 3 }) == 2);
            Debug.Assert(MinSubArrayLen(7, new[] { 2, 1, 5, 2, 8 }) == 1);
            Debug.Assert(MinSubArrayLen(7, new[] { 2, 1, 5, 2, 8, 1 }) == 1);
            Debug.Assert(MinSubArrayLen(8, new[] { 3, 4, 1, 1, 6 }) == 3);
            Debug.Assert(MinSubArrayLen(8, new[] { 3, 4, 1, 1, 6, 1}) == 3);
        }

        public int MinSubArrayLen(int s, int[] nums)
        {
            if((nums?.Length ?? 0 )== 0)
                return 0;
            
            int min = int.MaxValue;
            int winSum = 0;
            int winStart = 0;
            for(int winEnd = 0; winEnd < nums.Length; winEnd++)
            {
                winSum += nums[winEnd];
                while(winSum >= s && winStart <= winEnd)
                {
                    min = Math.Min(min, winEnd - winStart + 1);
                    winSum -= nums[winStart];                    
                    winStart++;                    
                }
            }
            
            return min == int.MaxValue ? 0 : min;
        }
    }

}