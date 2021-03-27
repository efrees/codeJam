using System;
using System.Linq;

namespace QualificationRound
{
    class MedianSort
    {
        // private static readonly int[] test = new[] { 9, 4, 10, 7, 8, 2, 3, 6, 1, 5 };

        static void MainD(string[] args)
        {
            var rawInput = Console.ReadLine().Split(' ').ToArray();
            var T = int.Parse(rawInput[0]);
            var n = int.Parse(rawInput[1]);
            var q = int.Parse(rawInput[2]);

            foreach (var k in Enumerable.Range(1, T))
            {
                var array = Enumerable.Range(1, n).ToArray();

                if (!RunMedianSort(array))
                {
                    break;
                }

                if (!SubmitAnswer(array))
                {
                    break;
                }
            }
        }

        private static bool RunMedianSort(int[] array)
        {
            for (var i = 1; i < array.Length - 1; i++)
            {
                for (var j = i + 1; j > 0; j -= 2)
                {
                    j = Math.Max(j, 2); // Make sure we compare to the first element when inserting
                    var x = array[j - 2];
                    var y = array[j - 1];
                    var z = array[j];

                    var answer = AskForMedian(x, y, z);
                    if (answer == -1)
                    {
                        return false;
                    }

                    if (answer == x)
                    {
                        //insert z below x and continue checking
                        array[j] = y;
                        array[j - 1] = x;
                        array[j - 2] = z;
                        //Console.Error.WriteLine(string.Join(" ", array.Select(x1 => test[x1 - 1])));
                        //Console.Error.WriteLine(string.Join(" ", array));
                    }
                    else if (answer == z)
                    {
                        //insert z below y and stop here
                        array[j] = y;
                        array[j - 1] = z;
                        break;
                    }
                    else
                    {
                        // Already correctly inserted (y is median)
                        break;
                    }
                }

                //Console.Error.WriteLine(string.Join(" ", array.Select(x => test[x - 1])));
                //Console.Error.WriteLine(string.Join(" ", array));
            }

            return true;
        }

        private static int AskForMedian(int x, int y, int z)
        {
            Console.WriteLine($"{x} {y} {z}");
            return int.Parse(Console.ReadLine());
        }

        private static bool SubmitAnswer(int[] array)
        {
            Console.WriteLine(string.Join(" ", array));
            var correct = int.Parse(Console.ReadLine());

            return correct != -1;
        }
    }
}