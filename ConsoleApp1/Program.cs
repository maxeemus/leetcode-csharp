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
            var originNums = new[]{17,25,-45, 35,51,-18,33,41,19,10,40,25,44,55,25};
            
            
            var nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, 0);

            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, nums.Length);

            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, nums.Length + 1);


            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, 1);

            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, 2);

            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, nums.Length - 1);

            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, nums.Length - 2);



            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, 4);
            
            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, 6);

            nums = originNums.Clone() as int[];
            new QuickSelect<int>().Select(nums, 8);            
        }

        public static void QuickSelect(int[] nums, int k)
        {
            if(k < 1 || k > nums.Length - 1)
                return;

            QuickSelect(nums, k, 0, nums.Length - 1);
        }

        private static void QuickSelect(int[] nums, int k, int l, int r)
        {
            int p = Pivot(nums, l - 1, l, r);
            if(p == k) 
                return;
            else if(k > p)
                QuickSelect(nums, k, p + 1, r);
            else
                QuickSelect(nums, k, l, p - 1);
        }

        public static int Pivot(int[] nums, int i, int j, int p)
        {
            for(; j < p; j++)
            {
                if(nums[j] <= nums[p])
                {
                    i++;
                    if(i != j) (nums[i], nums[j]) = (nums[j], nums[i]);
                }                
            }      
            i++;
            (nums[i], nums[p]) = (nums[p], nums[i]);
            return i;
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
