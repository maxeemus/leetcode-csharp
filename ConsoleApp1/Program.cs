using ConsoleApp1;
using ConsoleApp1.Amazon;
using System;
using System.Diagnostics;
using System.Linq;

class Application
{
    public static void Main() => new KMostWords().TopKFrequent(new[]{"i", "love", "leetcode", "i", "love", "coding"}, 2);    
}

// This code is contributed by Sam007 0
