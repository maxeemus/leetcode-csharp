using Challenges.Aug2020;
using ConsoleApp1;
using ConsoleApp1.Amazon;
using ConsoleApp1.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1
{
    class Application
    {
        public static void Main()
        {
            
        }

        public static int FindMin(int[] nums)
        {
           // --- Pivot point is min value - so search in part contains pivot
            // ----
            int left = 0;
            int right = nums.Length - 1;
            int mid = 0;            
            while (left < right)
            {
              mid = left + (right - left) / 2;

              if (mid == 0 || nums[mid - 1] < nums[mid])
                    return nums[mid - 1];
              if (nums[mid] < nums[mid + 1])
                    return nums[mid];  
                
                if (nums[left] > nums[mid])
                    right = mid;                    
                else if (nums[left] < nums[mid])
                    left = mid;
                else
                    left--;
            }
            return nums[mid];
        }
    }
}

// This code is contributed by Sam007 0
