using System;
using System.Linq;

namespace QualificationRound
{
    class MedianSort
    {
        static void MainC(string[] args)
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
                var valToInsert = array[i];

                var insertPosition = FindInsertPositionTernary(array, 0, i - 1, valToInsert);
                if (insertPosition == -1)
                {
                    return false;
                }

                for (var j = i; j > insertPosition; j--)
                {
                    array[j] = array[j - 1];
                }

                array[insertPosition] = valToInsert;
            }

            return true;
        }

        private static int FindInsertPositionTernary(int[] array, int low, int high, int valToInsert)
        {
            if (low > high)
            {
                return low;
            }
            
            var lowMiddle = low + (high - low) / 3;
            var highMiddle = low + (high - low) * 2 / 3;

            var isRangeOfTwo = high - low == 1;
            if (isRangeOfTwo)
            {
                highMiddle = high;
            }

            var p1 = array[lowMiddle];
            var p2 = array[highMiddle];

            var median = AskForMedian(p1, p2, valToInsert);
            if (median == -1)
            {
                return -1;
            }

            // We know p1 < p2
            if (median == p1)
            {
                // insert in low section
                // If low == lowMiddle, top case will catch it after they cross.
                if (isRangeOfTwo)
                {
                    return lowMiddle;
                }

                var newHigh1 = Math.Max(lowMiddle - 1, low + 1);

                return FindInsertPositionTernary(array, low, newHigh1, valToInsert);
            }

            if (median == p2)
            {
                // insert in high section
                return isRangeOfTwo
                    ? high + 1
                    : FindInsertPositionTernary(array, highMiddle, high, valToInsert);
            }

            // insert in middle
            if (isRangeOfTwo)
            {
                return highMiddle;
            }

            // if lowMiddle is adjacent to highMiddle (range of three) they'll cross and we'll use highMiddle (the new "low")
            var newLow = lowMiddle + 1;
            var newHigh = highMiddle - 1;

            // if they were separated by one, we'll have to do one more comparison with the current highMiddle
            if (newHigh == newLow)
            {
                newHigh++;
            }
            
            return isRangeOfTwo
                ? highMiddle
                : FindInsertPositionTernary(array, newLow, newHigh, valToInsert);
        }

        //private static int ActualMedian(int p1, int p2, int valToInsert)
        //{
        //    var vals = new[] { p1, p2, valToInsert };
        //    var max = vals.Max();
        //    var min = vals.Min();
        //    return vals.Except(new[] { min, max }).First();
        //}

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