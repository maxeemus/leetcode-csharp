
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

class MaxLengthOfConcatStrings
{
    public void Test()
    {
        Debug.Assert(MaxLength(new List<string>(){"un", "aba", "iq","ue"}) == 4);
        Debug.Assert(MaxLength(new List<string>(){"un","iq","ue"}) == 4);
        Debug.Assert(MaxLength(new List<string>(){"cha","r","act","ers"}) == 6);
        Debug.Assert(MaxLength(new List<string>(){"abcdefghijklmnopqrstuvwxyz"}) == 26);
        Debug.Assert(MaxLength(new List<string>(){"abcdeaf"}) == 0);
        Debug.Assert(MaxLength(new List<string>(){}) == 0);
        Debug.Assert(MaxLength(null) == 0);
    }   
    
    public int MaxLength(IList<string> arr) 
    {
        if (!(arr?.Any() ?? false))
            return 0;            
        int max = 0;
        List<int> combs = new List<int>(){0};
        foreach(var str in arr) 
        {
            int mask = 0;
            int dup = 0;
            // build bitmask for each string in collection
            foreach(var c in str)
            {
                // if there are duplicates in string? - break if it is                
                dup |= mask & (1 << (c -'a'));                
                if(dup != 0)
                    break;
                mask |= 1 << (c -'a');
            }
            if(dup != 0) 
                continue;            

            // add all new co
            for(int i = combs.Count - 1; i >= 0; --i)
            {
                if((combs[i] & mask) != 0)
                    continue;
                combs.Add(combs[i] | mask);
                max = Math.Max(max, BitCount(combs[i] | mask));
            }            
        }

        return max;
    } 

    private int BitCount(int v)
    {
        int c = 0;
        while(v != 0)
        {
            c += v & 1;
            v >>= 1;
        }
        return c;
    }
}