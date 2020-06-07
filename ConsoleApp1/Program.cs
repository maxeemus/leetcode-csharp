using ConsoleApp1;
using ConsoleApp1.Amazon;
using System;
using System.Diagnostics;
using System.Linq;

class Application
{
    public static void Main() => new KMostRequired().popularNFeatures(5, 3, new[]{"anacell", "betacellular", "cetracular", "deltacellular", "eurocell"}.ToList(), 5, new[]{
        "I love anacell Best services; Best services provided by anacell",
    "betacellular has great services",
    "deltacellular provides much better services than betacellular",
    "cetracular is worse than anacell",
    "Betacellular is better than deltacellular."}.ToList());
}

// This code is contributed by Sam007 0
