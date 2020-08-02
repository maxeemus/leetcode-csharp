using System.Collections.Generic;
using FluentAssertions;

namespace Challenges.Aug2020
{
    public class LoggerRateLimiter
    {

        private readonly Dictionary<string, int> messages = new Dictionary<string, int>();
        /** Initialize your data structure here. */
        public LoggerRateLimiter()
        {

        }

        public static void Test()
        {
            LoggerRateLimiter logger = new LoggerRateLimiter();

            // logging string "foo" at timestamp 1
            logger.ShouldPrintMessage(1, "foo").Should().Be(true);

            // logging string "bar" at timestamp 2
            logger.ShouldPrintMessage(2, "bar").Should().Be(true);

            // logging string "foo" at timestamp 3
            logger.ShouldPrintMessage(3, "foo").Should().Be(false);

            // logging string "bar" at timestamp 8
            logger.ShouldPrintMessage(8, "bar").Should().Be(false);

            // logging string "foo" at timestamp 10
            logger.ShouldPrintMessage(10, "foo").Should().Be(false);

            // logging string "foo" at timestamp 11
            logger.ShouldPrintMessage(11, "foo").Should().Be(true);
        }

        /** Returns true if the message should be printed in the given timestamp, otherwise returns false.
            If this method returns false, the message will not be printed.
            The timestamp is in seconds granularity. */
        public bool ShouldPrintMessage(int timestamp, string message)
        {
            if (!messages.ContainsKey(message))
            {
                messages.Add(message, timestamp);
                return true;
            }
            bool res = (timestamp - messages[message]) >= 10;
            if (res) messages[message] = timestamp;
            return res;
        }
    }
}