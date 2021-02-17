using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class ListTest
    {
        public static bool FindTwo(int[] list, int s)
        {
            if((list?.Length ?? 0) == 0)
                return false;
            var itemSumDiff = new HashSet<int>();
            for (int i = 0; i < list.Length; i++)
            {                
                if(itemSumDiff.Contains(list[i]))
                    return true;
                itemSumDiff.Add(s - list[i]);
            }

            return false;
        }        
    }

    public class ListTestShould
    {        
        public void ReturnTrueIfElementsExist()
        {
            Debug.Assert(ListTest.FindTwo(new[]{1,2,3,4,5}, 5));
            Debug.Assert(ListTest.FindTwo(new[]{1,2}, 3));
            Debug.Assert(ListTest.FindTwo(new[]{-1,2}, 1));
            Debug.Assert(ListTest.FindTwo(new[]{0,2}, 2));
        }

        public void ReturnFalseIfElementsDontExist()
        {
            Debug.Assert(ListTest.FindTwo(new[]{10,20,30,40,50}, 5));
            Debug.Assert(ListTest.FindTwo(new[]{10,20}, 3));
            Debug.Assert(ListTest.FindTwo(new[]{10}, 10));
            Debug.Assert(ListTest.FindTwo(new int[0], 3));
            Debug.Assert(ListTest.FindTwo(null, 3));
        }
    }
}