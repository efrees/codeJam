using System;
using System.Collections.Generic;
using System.Linq;

namespace Round1A
{
    public class GolfGophers
    {
        static void Main(string[] args)
        {
            var tokens = Console.ReadLine().Split(' ');
            var t = int.Parse(tokens[0]);
            var n = int.Parse(tokens[1]);
            var maxGophers = int.Parse(tokens[2]);

            var k = 1;
            while (k <= t)
            {
                var windmills = new int[18];

                var possibilities = new HashSet<int>();
                var possibilitiesOrig = Enumerable.Range(1, maxGophers);
                foreach (var p in possibilitiesOrig)
                {
                    possibilities.Add(p);
                }

                foreach (var blades in new int[] { 2, 3, 5, 7, 11, 13, 17 })
                {
                    for (int i = 0; i < windmills.Length; i++)
                    {
                        windmills[i] = blades;
                    }

                    Console.WriteLine(string.Join(" ", windmills));

                    //Get answer
                    var windmillPositions = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                    //Update possibilities
                    var totalOffset = windmillPositions.Sum();
                    possibilities.RemoveWhere(x => x % blades != totalOffset % blades);

                    if (possibilities.Count == 1)
                    {
                        break;
                    }
                }

                // Guess
                Console.WriteLine(possibilities.First());

                var correctness = int.Parse(Console.ReadLine());
                if (correctness < 0)
                {
                    //Wrong answer. Get out.
                    return;
                }

                k++;
            }
        }
    }
}