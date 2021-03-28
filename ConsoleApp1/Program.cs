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
            var a = new MaxAreaOfPiece().MaxArea(5, 4, new[]{1,2,4}, new[]{1, 3});
            a = 0;
        }        
    }
}

// This code is contributed by Sam007 0
