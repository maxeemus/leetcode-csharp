using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace ConsoleApp1
{
    public class IntReverse
    {
        public void Test()
        {
            Reverse(-12835);
            Reverse(0);
            Reverse(Int32.MaxValue);
            Reverse(Int32.MinValue);
        }

        public int Reverse(int x)
        {
            int res = 0;
            int ml = Int32.MaxValue / 10;
            int ll = Int32.MinValue / 10;
            int mlLim = Int32.MaxValue - ml * 10;
            int llLim = Int32.MinValue - ll * 10;
            while (x != 0)
            {
                int c = x % 10;
                x /= 10;
                if (res > ml || (res == ml && c > mlLim) || res < ll || (res == ll && c < llLim))
                    return 0;
                res = res * 10 + c;
            }
            return res;
        }
    }
}
