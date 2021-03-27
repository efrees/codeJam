using System;
using System.Linq;

namespace QualificationRound
{
    class MoonsAndUmbrellas
    {
        static void MainB(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            foreach (var k in Enumerable.Range(1, T))
            {
                var inputTokens = Console.ReadLine().Split(' ').ToArray();
                var x = int.Parse(inputTokens[0]);
                var y = int.Parse(inputTokens[1]);

                var stateOfArt = inputTokens[2];

                var answer = MinimizeCostAssumingPositive(x, y, stateOfArt);
                Console.WriteLine($"Case #{k}: {answer}");
            }
        }

        private static int MinimizeCostAssumingPositive(int cjCost, int jcCost, string stateOfArt)
        {
            // Blanks at start and end of string can always be filled to be the same as the adjacent ones (no infringement)
            // If we have a current character and the next is the same, we'd fill all blanks with the same (adding no additional infringements)
            // If we have a current character and the next is different, we can match either one with the same result as if the blanks aren't there.
            // Conclusion: all blanks can be ignored. But that bonus point... ;-)

            var cost = 0;
            var previous = '-';
            foreach (var next in stateOfArt.Where(c => c != '?'))
            {
                if (previous != '-')
                {
                    if (previous == 'C' && next == 'J')
                    {
                        cost += cjCost;
                    }
                    else if (previous == 'J' && next == 'C')
                    {
                        cost += jcCost;
                    }
                }

                previous = next;
            }

            return cost;
        }
    }
}