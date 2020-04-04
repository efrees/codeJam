using System;
using System.Linq;

namespace QualificationRound
{
    public class ProgramC
    {
        public static void MainC()
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var a = int.Parse(Console.ReadLine());

                var desiredWidth = a / 5;
                var gridTracker = new bool[5, Math.Max(desiredWidth, 3)];

                var preparedCells = 0;
                var exchanges = 0;
                while (exchanges < 1001)
                {
                    var bestCoordinates = GetNextAttempt(gridTracker, desiredWidth);
                    var direction = $"{bestCoordinates.r + 1} {bestCoordinates.c + 1}";
                    Console.WriteLine(direction);

                    var actualCoordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();

                    var responseSum = actualCoordinates.Sum();
                    if (responseSum == -2 || responseSum == 0)
                    {
                        break;
                    }

                    var actualRow = actualCoordinates[0];
                    var actualColumn = actualCoordinates[1];
                    if (!gridTracker[actualRow - 1, actualColumn - 1])
                    {
                        gridTracker[actualRow - 1, actualColumn - 1] = true;
                        preparedCells++;
                    }

                    if (actualColumn > desiredWidth && desiredWidth < gridTracker.GetLength(1))
                    {
                        desiredWidth = actualColumn;
                    }

                    exchanges++;
                }

                k++;
            }
        }

        private static Coord GetNextAttempt(bool[,] gridTracker, int desiredWidth)
        {
           Coord coordsWeNeed = null;
            var numRows = gridTracker.GetLength(0);
            var numCols = gridTracker.GetLength(1);
            for (var r = 0; r < numRows; r++)
            {
                for (var c = 0; c < desiredWidth; c++)
                {
                    if (!gridTracker[r, c])
                    {
                        coordsWeNeed = new Coord(r, c);
                        break;
                    }
                }

                if (coordsWeNeed != null) break;
            }

            return new Coord(Math.Min(coordsWeNeed.r + 1, numRows - 2), Math.Min(coordsWeNeed.c + 1, numCols - 2));
        }

        private class Coord
        {
            public int r { get; set; }
            public int c { get; set; }

            public Coord(int r, int c)
            {
                this.r = r;
                this.c = c;
            }
        }
    }
}