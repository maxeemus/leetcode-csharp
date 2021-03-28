using System;
using System.Linq;
using System.Numerics;

public class MaxAreaOfPiece {
    public int MaxArea(int h, int w, int[] horizontalCuts, int[] verticalCuts) {
      //Array.Sort(horizontalCuts);
      //Array.Sort(verticalCuts);
      
      var maxH = MaxCutSize2(h, horizontalCuts);
      var maxW = MaxCutSize2(w, verticalCuts);
                                  
      return (int)((maxH * maxW) % new BigInteger(Math.Pow(10, 9) + 7));
    }
  
    private BigInteger MaxCutSize(int edge, int[] cuts)
    {
      int max = cuts[0];
      for(int i = 0; i < cuts.Length - 1; i++)
      {        
        max = Math.Max(max, cuts[i + 1] - cuts[i]);
      }
      max = Math.Max(max, edge - cuts[cuts.Length - 1]);
      return max;
    }
  
    private BigInteger MaxCutSize2(int edge, int[] cuts)
    {
      var cutSize = (s: 0, e: 0);      
      foreach(int c in cuts.Concat(new[]{edge}))
      {        
        if(c > cutSize.s && c < cutSize.e)
            cutSize.s = c;
        else if(c > cutSize.s &&  c > cutSize.e && c - cutSize.e >= cutSize.e - cutSize.s)
            cutSize = (cutSize.e, c);        
      }
      
      return cutSize.e - cutSize.s;
    }
}
