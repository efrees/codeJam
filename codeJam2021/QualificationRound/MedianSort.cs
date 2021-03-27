using System;
using System.Diagnostics;
using System.Linq;

namespace QualificationRound
{
    class MedianSort
    {
        //private static readonly int[] test = new[] { 9, 2, 5, 10, 3, 1, 7, 6, 8, 4 };

        static void Main(string[] args)
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
            for (var i = 2; i < array.Length; i++)
            {
                var x = array[i - 2];
                var y = array[i - 1];
                var z = array[i];

                var median = AskForMedian(x, y, z);
                if (median == -1)
                {
                    return false;
                }

                if (median == y)
                {
                    // Already correctly inserted. Move to next insert (i)
                    continue;
                }

                if (median == z)
                {
                    //insert z below y and move to next insert
                    array[i] = y;
                    array[i - 1] = z;
                    continue;
                }

                // (median == x)
                //insert z below x and work to left
                array[i] = y;
                array[i - 1] = x;
                array[i - 2] = z;


                // 3 [5] 1 --> [5] 3 1
                // BUGFIX: Need to keep the "direction" the same when comparing to first element after an insert
                for (var j = i - 2; j > 0; j--)
                {
                    //Console.Error.WriteLine(string.Join(" ", array.Select(x1 => test[x1 - 1])));
                    //Console.Error.WriteLine(string.Join(" ", array));
                    x = array[j - 1];
                    y = array[j];
                    z = array[j + 1];

                    median = AskForMedian(x, y, z);
                    if (median == -1){
                        return false;
                    }

                    // y and z are already in correct order thanks to the first step of insert above.
                    if (median == x)
                    {
                        array[j] = x;
                        array[j - 1] = y;
                    }
                    else
                    {
                        Debug.Assert(median == y);
                        break;
                    }
                }
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