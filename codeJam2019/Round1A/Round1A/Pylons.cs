using System;
using System.Collections.Generic;
using System.Linq;

namespace Round1A
{
    public class Pylons
    {
        // Mono compile error as of the end of the round
        // Runtime error once C# 7 syntax was swapped with equivalent
        static void _Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= t)
            {
                var dimensions = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                var r = dimensions[0];
                var c = dimensions[1];
                var swapCoords = r < c;

                if (swapCoords)
                {
                    var tmp = c;
                    c = r;
                    r = tmp;
                }

                IEnumerable<Coord> visitedPoints = null;

                var answer = "POSSIBLE";
                if (r <= 3)
                {
                    answer = "IMPOSSIBLE";
                }
                else if (r == 4 && c == 4)
                {
                    visitedPoints = Visit4x4();
                }
                else
                {
                    var nextC = 1;

                    if (c % 3 == 1)
                    {
                        //Two twos, then threes
                        visitedPoints = VisitNx2(r, nextC)
                            .Concat(VisitNx2(r, nextC + 2));
                        nextC += 4;
                    }
                    else if (c % 3 == 2)
                    {
                        //One two, then threes
                        visitedPoints = VisitNx2(r, nextC);
                        nextC += 2;
                    }

                    //Do threes
                    visitedPoints.Concat(VisitNx3(r, nextC));
                }

                Console.WriteLine($"Case #{k}: {answer}");

                if (visitedPoints != null)
                {
                    foreach (var point in visitedPoints)
                    {
                        point.Print(swapCoords);
                    }
                }

                k++;
            }
        }

        private static IEnumerable<Coord> Visit4x4()
        {
            return new Coord[]
            {
                new Coord { R = 1, C = 1 },
                new Coord { R = 3, C = 2 },
                new Coord { R = 1, C = 3 },
                new Coord { R = 3, C = 4 },
                new Coord { R = 2, C = 1 },
                new Coord { R = 4, C = 2 },
                new Coord { R = 2, C = 3 },
                new Coord { R = 4, C = 4 },
                new Coord { R = 3, C = 1 },
                new Coord { R = 1, C = 2 },
                new Coord { R = 3, C = 3 },
                new Coord { R = 1, C = 4 },
                new Coord { R = 2, C = 2 },
                new Coord { R = 4, C = 1 },
                new Coord { R = 2, C = 4 },
                new Coord { R = 4, C = 3 },
            };
        }

        private static IEnumerable<Coord> VisitNx2(int n, int cStart = 1)
        {
            var rStart = 0;
            for (var i = 0; i < n; i++)
            {
                var r = (rStart + i) % n;
                yield return new Coord { R = r + 1, C = cStart };
                yield return new Coord { R = (r + n - 2) % n + 1, C = cStart + 1 };
            }
        }

        private static IEnumerable<Coord> VisitNx3(int n, int cStart = 0)
        {
            var rStart = 0;
            for (var r = rStart; r < n; r++)
            {
                yield return new Coord { R = r + 1, C = cStart };
                yield return new Coord { R = (r + 2) % n + 1, C = cStart + 1 };
                yield return new Coord { R = r + 1, C = cStart + 2 };
            }
        }

        private class Coord
        {
            internal int R { get; set; }
            internal int C { get; set; }

            internal void Print(bool swap)
            {
                if (swap)
                    Console.WriteLine($"{C} {R}");
                else
                    Console.WriteLine($"{R} {C}");
            }
        }
    }
}