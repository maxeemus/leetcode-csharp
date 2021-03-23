using System;
using System.Numerics;

public class MaxAreaOfPiece {
    public int MaxArea(int h, int w, int[] horizontalCuts, int[] verticalCuts) {
      Array.Sort(horizontalCuts);
      Array.Sort(verticalCuts);
      
      var maxH = MaxCutSize(h, horizontalCuts);
      var maxW = MaxCutSize(w, verticalCuts);
                                        
      return (int)BigInteger.ModPow(maxH * maxH, 1, new BigInteger(Math.Pow(10, 9) + 7));
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
}
