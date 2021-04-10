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

            if (cjCost > 0 && jcCost > 0)
            {
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
            else
            {
                // General algorithm, maximizing sequences that earn money.
                // Any sequence of blanks in the middle can either add n/2 pairs of changes (+1 pair if boundaries are the same character) or add zero
                // Any sequence of blanks at the start can add n/2 pairs

                // C???J
                // CCCCJ One CJ
                // CJCJJ Two CJs, one JC
                // CCJCJ Two CJs, one JC

                // C??J
                // CCCJ One CJ
                // CJCJ Two CJs, one JC
                // CJCJ Two CJs, one JC

                // C???C
                // CCCCC - none
                // CJCJC Two CJs, two JCs
                // CCJCC One CJ, one JC
                // CJCCC One CJ, one JC
                return 0;

            }
        }
    }
}