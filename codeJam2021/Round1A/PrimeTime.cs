using System;
using System.Collections.Generic;
using System.Linq;

namespace Round1A
{
    class PrimeTime
    {
        public static void Main()
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var M = int.Parse(Console.ReadLine());

                var countedPrimes = new Dictionary<int, int>();
                while (M-- > 0)
                {
                    var parts = Console.ReadLine().Split().ToArray();
                    countedPrimes[int.Parse(parts[0])] = int.Parse(parts[1]);
                }

                //Set 1 solution (brute force, backtracking)
                var summedPrimes = new Dictionary<int, int>(countedPrimes);
                var multipliedPrimes = countedPrimes.ToDictionary(pair => pair.Key, pair => 0);
                //Max for set 2: 49,900
                var sum = SumDict(summedPrimes);
                var product = 1;
                var answer = FindMaxMatchRecursively(summedPrimes, sum, multipliedPrimes, product);

                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        private static int FindMaxMatchRecursively(Dictionary<int, int> summedPrimes,
            int sum,
            Dictionary<int, int> multipliedPrimes,
            int product)
        {
            if (product > sum)
            {
                return 0;
            }

            if (product == sum)
            {
                return sum;
            }

            var maxScore = 0;
            var nextSelectionOptions = summedPrimes.Where(pair => pair.Value > 0).Select(pair => pair.Key).ToList();
            foreach (var prime in nextSelectionOptions)
            {
                summedPrimes[prime]--;
                multipliedPrimes[prime]++;
                product *= prime;
                sum -= prime;

                var bestFromRecursion = FindMaxMatchRecursively(summedPrimes, sum, multipliedPrimes, product);
                if (bestFromRecursion > maxScore)
                {
                    maxScore = bestFromRecursion;
                }

                summedPrimes[prime]++;
                multipliedPrimes[prime]--;
                product /= prime;
                sum += prime;
            }

            return maxScore;
        }

        private static int SumDict(Dictionary<int, int> summedPrimes)
        {
            return summedPrimes.Select(pair => pair.Key * pair.Value).Sum();
        }
    }
}