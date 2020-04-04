using System;
using System.Text;

namespace QualifyingRound
{
    public class NestingDepth
    {
        public static void NestingDepthMain()
        {
            var t = int.Parse(Console.ReadLine());
            var k = 1;
            while (k <= t)
            {
                var digits = Console.ReadLine();
                var curDepth = 0;
                var resultBuilder = new StringBuilder();

                foreach (var c in digits)
                {
                    var val = c - '0';

                    while (curDepth < val)
                    {
                        resultBuilder.Append('(');
                        curDepth++;
                    }

                    while (curDepth > val)
                    {
                        resultBuilder.Append(')');
                        curDepth--;
                    }

                    resultBuilder.Append(c);
                }

                while (curDepth > 0)
                {
                    resultBuilder.Append(')');
                    curDepth--;
                }

                Console.WriteLine($"Case #{k}: {resultBuilder}");
                k++;
            }
        }
    }
}
