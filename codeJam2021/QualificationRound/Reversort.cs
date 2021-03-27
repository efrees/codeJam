using System;
using System.Linq;

namespace QualificationRound
{
    class Reversort
    {
        static void MainA(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            foreach (var k in Enumerable.Range(1, T))
            {
                var _n = int.Parse(Console.ReadLine());
                var array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                var answer = ScoreReverseSort(array);
                Console.WriteLine($"Case #{k}: {answer}");
            }
        }

        private static object ScoreReverseSort(int[] array)
        {
            var score = 0;
            for (var i = 0; i < array.Length - 1; i++)
            {
                var min = array[i];
                var min_j = i;
                for (var j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < min)
                    {
                        min = array[j];
                        min_j = j;
                    }
                }

                score += 1 + min_j - i;

                Reverse(array, i, min_j);
            }

            return score;
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