using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeRound
{
    public class ProgramC
    {
        public static void Main()
        {
            var cases = Convert.ToInt32(Console.ReadLine());
            var k = 1;
            while (k <= cases)
            {
                var caseBoundaries = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var D = Convert.ToInt32(caseBoundaries[0]);
                var N = Convert.ToInt32(caseBoundaries[1]);
                var otherHorses = new List<Horse>();
                foreach (var i in Enumerable.Range(1, N))
                {
                    var horseDesriptor = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();
                    otherHorses.Add(new Horse
                    {
                        StartingPosition = horseDesriptor[0],
                        StartingSpeed = horseDesriptor[1]
                    });
                }

                var timeLastHorseFinishes = otherHorses
                    .Select(h => (D - h.StartingPosition) / (decimal) h.StartingSpeed)
                    .Max();

                var answer = D / timeLastHorseFinishes;
                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        public class Horse
        {
            public int StartingPosition { get; set; }
            public int StartingSpeed { get; set; }
        }
    }
}