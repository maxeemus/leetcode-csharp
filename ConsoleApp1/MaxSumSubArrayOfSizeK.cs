using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1
{
    class MaxSumSubArrayOfSizeK
    {
        public void Test()
        {
            Debug.Assert(FindMaxSumSubArray(3, new[] { 2, 1, 5, 1, 3, 2 }) == 9);
            Debug.Assert(FindMaxSumSubArray(2, new[] { 2, 3, 4, 1, 5 }) == 7);

            MaxSlidingWindow(new[]{1,3,-1,-3,5,3,6,7}, 3);
            MaxSlidingWindow(new[]{1,-1}, 1);
        }

        public int FindMaxSumSubArray(int k, int[] arr)
        {
            if((arr?.Length ?? 0) < k)
                return -1;
            
            var subArraySums = new Dictionary<int, int>(arr.Length);
            subArraySums.Add(0, 0);
            for(int i = 0; i < k; i++)
            {
                subArraySums[0] += arr[i];
            }
            var max = subArraySums[0];

            for(int i = 1; i < arr.Length - k; i++)
            {                
                subArraySums.Add(i, subArraySums[i - 1] - arr[i - 1] + arr[i + k - 1]);
                max = Math.Max(max, subArraySums[i]);
            }

            return max;
        }

        public int[] MaxSlidingWindow(int[] nums, int k) 
        {
            if((nums?.Length ?? 0) < k)
                return new int[0];
            if(k == 0)
                return new int[0];
            if(k == 1)
                return nums;
                        
            var subArrayMax = new Dictionary<int, int>(nums.Length);
            var max = nums[0];            
            for(int i = 0; i < k; i++)
            {                
                max = Math.Max(max, nums[i]);                
            }            
            subArrayMax[0] = max;            

            for(int i = 1; i <= nums.Length - k; i++)
            {                                
                if(nums[i - 1] == max)
                {
                    max = nums[i];
                    for(int j = i; j < i + k; j++)
                    {                
                        max = Math.Max(max, nums[j]);
                    }            
                }
                else
                    max = Math.Max(max, nums[i + k - 1]);
                subArrayMax.Add(i, max);
            }
            return subArrayMax.Values.ToArray();
        }
    }
}