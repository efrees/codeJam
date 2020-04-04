using System;
using System.Linq;
using System.Collections.Generic;

namespace QualifyingRound
{
    public class Vestigium
    {
        public static void VestigiumMain()
        {
            var t = int.Parse(Console.ReadLine());
            var c = 1;
            while (c <= t)
            {
                var n = int.Parse(Console.ReadLine());
                var trace = 0;
                var seenInColumn = Enumerable.Range(0, n)
                    .Select(_ => new HashSet<int>())
                    .ToArray();
                var columnsWithDuplicate = new HashSet<int>();
                var rowsWithDuplicate = new HashSet<int>();

                for (var i = 0; i < n; i++)
                {
                    var seenInRow = new HashSet<int>();
                    var rowTokens = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (var j = 0; j < n; j++)
                    {
                        var value = int.Parse(rowTokens[j]);

                        if (i == j)
                        {
                            trace += value;
                        }

                        if (seenInRow.Contains(value))
                        {
                            rowsWithDuplicate.Add(i);
                        }

                        if (seenInColumn[j].Contains(value))
                        {
                            columnsWithDuplicate.Add(j);
                        }

                        seenInRow.Add(value);
                        seenInColumn[j].Add(value);
                    }
                }

                var rows = rowsWithDuplicate.Count();
                var cols = columnsWithDuplicate.Count();
                Console.WriteLine($"Case #{c}: {trace} {rows} {cols}");
                c++;
            }
        }
    }
}