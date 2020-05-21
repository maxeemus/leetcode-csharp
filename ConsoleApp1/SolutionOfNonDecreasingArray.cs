using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// https://leetcode.com/problems/non-decreasing-array/
    /// </summary>
    public class SolutionOfNonDecreasingArray
    {
        public void Test()
        {
            Debug.Assert(CheckPossibility(new[] { 4, 2, 3 }));        // true
            Debug.Assert(CheckPossibility(new[] { 4, 2, 1 }));        //false
            Debug.Assert(CheckPossibility(new[] { 3, 4, 2, 3 }));     // false   

        }
        public bool CheckPossibility(int[] nums)
        {
            var l = nums.Length;
            for (int i = 0; i < l - 1; i++)
            {
                if (nums[i] > nums[i + 1])
                {
                    for (int j = i + 2; j < l; j++)
                    {
                        if (nums[j] <= nums[i + 1])
                            return false;
                    }
                    return true;
                }
            }
            return true;
        }

    }
}
