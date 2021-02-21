using System;

namespace ConsoleApp1.Common
{
    class QuickSelect<T> where T: IComparable
    {
        public void Select(T[] nums, int k)
        {
            if(k < 1 || k > nums.Length - 1)
                return;

            Select(nums, k, 0, nums.Length - 1);
        }

        private void Select(T[] nums, int k, int l, int r)
        {
            int p = Pivot(nums, l - 1, l, r);
            if(p == k) 
                return;
            else if(k > p)
                Select(nums, k, p + 1, r);
            else
                Select(nums, k, l, p - 1);
        }

        private int Pivot(T[] nums, int i, int j, int p)
        {
            for(; j < p; j++)
            {
                if(nums[j].CompareTo(nums[p]) <= 0)
                {
                    i++;
                    if(i != j) (nums[i], nums[j]) = (nums[j], nums[i]);
                }                
            }      
            i++;
            (nums[i], nums[p]) = (nums[p], nums[i]);
            return i;
        }

    }
}