using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ConsoleApp1
{
    class SearchInRotatedSortedArray
    {
        public void Test()
        {
            Debug.Assert(GetRotatedArrayMin(new int[] { 1, 2 , 3, 4, 5 }) == 0);
            Debug.Assert(GetRotatedArrayMin(new int[] { 4, 5, 6, 7 }) == 0);
            Debug.Assert(GetRotatedArrayMin(new int[] { 8, 4, 5, 6, 7 }) == 1);
            Debug.Assert(GetRotatedArrayMin(new int[] { 4, 5, 6, 3 }) == 3);
            Debug.Assert(GetRotatedArrayMin(new int[] { 1, 2, 3 }) == 0);
            Debug.Assert(GetRotatedArrayMin(new int[] { 5, 1, 3 }) == 1);
            Debug.Assert(GetRotatedArrayMin(new int[] { 4, 5, 6, 7, 0, 1, 2 }) == 4);
            Debug.Assert(GetRotatedArrayMin(new int[] { 6, 7, 8, 1, 2, 3, 4, 5 }) == 3);
            Debug.Assert(GetRotatedArrayMin(new int[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 }) == 5);

            
            //Debug.Assert(Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0) == 4, "Test 1");
            //Debug.Assert(Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0) == 4, "Test 1");
            //Debug.Assert(Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 3) == -1, "Test 2");            

            //Debug.Assert(Search(new int[] { 5,  1, 3 }, 5) == 0, "Test 2");
        }

        public int Search(int[] nums, int target)
        {
            if (!(nums?.Any() ?? false))
                return -1;

            int shift = GetRotatedArrayMin(nums);
            int length = nums.Length;
            int left = 0;
            int right = length - 1;

            while(right >= left)
            {                
                int m = left + (right - left) / 2;
                int mActual = GetActualIndex(m, shift, nums.Length);
                int mElement = nums[mActual];
                if (target == mElement)
                    return mActual;
                else if (target < mElement)
                    right = m - 1;
                else
                    left = m + 1;
            }
            return -1;
        }

        private int GetActualIndex(int normalIndex, int shift, int numsLenght) => ((normalIndex + shift) % numsLenght);
        private int GetRotatedArrayMin(int[] nums)
        {
            if (!(nums?.Any() ?? false))
                return -1;
            if (nums.Length == 1)
                return 1;

            int length = nums.Length;
            int left = 0;
            int right = length - 1;
            while (right >= left)
            {
                int m = left + (right - left) / 2;
                if (m < right && nums[m] > nums[m + 1])
                    return m + 1;
                if (m > left && nums[m - 1] > nums[m])
                    return m;
                if (nums[left] < nums[m])
                    left = m + 1;
                if (nums[m] < nums[right])
                    right = m - 1; 
            }            
            return 0;
        }
    }
}
