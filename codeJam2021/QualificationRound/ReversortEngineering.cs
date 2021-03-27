using System;
using System.Linq;

namespace QualificationRound
{
    class ReversortEngineering
    {
        static void MainC(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            foreach (var k in Enumerable.Range(1, T))
            {
                var inputTokens = Console.ReadLine().Split(' ').ToArray();
                var size = int.Parse(inputTokens[0]);
                var targetCost = int.Parse(inputTokens[1]);

                var iterations = GetIterationCosts(size, targetCost);

                var answer = iterations.Length == 0 ? "IMPOSSIBLE" : EngineerListToMatchIterations(size, iterations);

                Console.WriteLine($"Case #{k}: {answer}");
            }
        }

        private static int[] GetIterationCosts(int size, int targetCost)
        {
            if (size - 1 > targetCost)
            {
                return new int[0];
            }

            // For instance for size=4, we'd have three iterations, with max of 4, 3, 2
            var maxCost = (size + 2) * (size - 1) / 2;
            if (maxCost < targetCost)
            {
                return new int[0];
            }

            var iterations = new int[size - 1];
            for (var i = 0; i < iterations.Length; i++)
            {
                iterations[i] = size - i;
            }

            var totalReductionFromMax = maxCost - targetCost;
            for (var i = iterations.Length - 1; i >= 0 && totalReductionFromMax > 0; i--)
            {
                var adjustment = Math.Min(totalReductionFromMax, iterations[i] - 1);
                totalReductionFromMax -= adjustment;
                iterations[i] -= adjustment;
            }

            return iterations;
        }

        private static string EngineerListToMatchIterations(int size, int[] iterations)
        {
            var array = Enumerable.Range(1, size).ToArray();

            // iteration index should match starting index in the original array
            for (var i = iterations.Length - 1; i >= 0; i--)
            {
                Reverse(array, i, i + iterations[i] - 1);
            }

            return string.Join(" ", array);
        }

        private static void Reverse(int[] array, int i, int j)
        {
            while (i < j)
            {
                // It looked like Tuples aren't supported properly in Mono,
                // and the (x,y) = (y,x) swap gave wrong answer.
                var t = array[i];
                array[i] = array[j];
                array[j] = t;
                i++;
                j--;
            }
        }
    }
}