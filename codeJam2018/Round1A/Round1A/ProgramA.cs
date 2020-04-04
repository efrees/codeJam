using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Round1A
{
    public class ProgramA
    {
        static void MainA(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var answer = SolveCase();
                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        private static string SolveCase()
        {
            var rchv = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var r = rchv[0];
            var c = rchv[1];
            var h = rchv[2];
            var v = rchv[3];

            var grid = new string[r];
            foreach (var i in Enumerable.Range(0, r))
            {
                grid[i] = Console.ReadLine();
            }

            var hasSolution = FindSolution(grid, r, c, h, v);

            return hasSolution ? "POSSIBLE" : "IMPOSSIBLE";
        }

        internal static bool FindSolution(string[] grid, int r, int c, int h, int v)
        {
            var totalPieces = (v + 1) * (h + 1);
            var totalChipCount = grid.Sum(row => row.Count(col => col == '@'));

            if (totalChipCount % totalPieces != 0)
            {
                return false;
            }

            if (totalChipCount == 0) return true;

            var chipsPerPerson = totalChipCount / totalPieces;
            var targetColumnTotal = chipsPerPerson * (h + 1);
            var targetRowTotal = chipsPerPerson * (v + 1);

            var columnTotals = new int[c];
            var rowTotals = new int[r];
            for (var j = 0; j < r; j++)
            {
                for (var i = 0; i < c; i++)
                {
                    if (grid[j][i] == '@')
                    {
                        columnTotals[i]++;
                        rowTotals[j]++;
                    }
                }
            }

            var verticalCutsBeforeColumns = TryCutting(columnTotals, v, targetColumnTotal);
            var horizontalCutsBeforeRows = TryCutting(rowTotals, h, targetRowTotal);

            return verticalCutsBeforeColumns != null
                   && horizontalCutsBeforeRows != null
                   && CutsProduceEquivalentCounts(grid, r, c, verticalCutsBeforeColumns, horizontalCutsBeforeRows);
        }

        private static int[] TryCutting(int[] transverseTotals, int cuts, int targetSectionTotal)
        {
            var cutsBefore = new int[cuts];
            var nextCutIndex = 0;
            var workingSum = 0;
            for (var i = 0; i < transverseTotals.Length; i++)
            {
                if (nextCutIndex < cuts && workingSum == targetSectionTotal)
                {
                    cutsBefore[nextCutIndex] = i;
                    nextCutIndex++;
                    workingSum = 0;
                }
                else if (workingSum > targetSectionTotal)
                {
                    return null;
                }

                workingSum += transverseTotals[i];
            }

            if (nextCutIndex != cuts || workingSum != targetSectionTotal)
            {
                // we didn't make the expected number of cuts, or we didn't end with the correct number.
                if (Debugger.IsAttached) Debugger.Break();
                return null;
            }

            return cutsBefore;
        }

        private static bool CutsProduceEquivalentCounts(string[] grid, int r, int c, int[] verticalCutsBeforeColumns,
            int[] horizontalCutsBeforeRows)
        {
            var allCounts = new List<int>();
            verticalCutsBeforeColumns = verticalCutsBeforeColumns.Concat(new[] { c }).ToArray();
            horizontalCutsBeforeRows = horizontalCutsBeforeRows.Concat(new[] { r }).ToArray();
            for (var i = 0; i < verticalCutsBeforeColumns.Length; i++)
            {
                var startColumn = i > 0 ? verticalCutsBeforeColumns[i - 1] : 0;

                for (var j = 0; j < horizontalCutsBeforeRows.Length; j++)
                {
                    var startRow = j > 0 ? horizontalCutsBeforeRows[j - 1] : 0;
                    var lastColumn = verticalCutsBeforeColumns[i] - 1;
                    var lastRow = horizontalCutsBeforeRows[j] - 1;

                    var count = SumRange(grid, startRow, startColumn, lastRow, lastColumn);

                    allCounts.Add(count);
                }
            }

            return allCounts.Distinct().Count() == 1;
        }

        private static int SumRange(string[] grid, int startRow, int startColumn, int lastRow, int lastColumn)
        {
            var count = 0;
            for (var row = startRow; row <= lastRow; row++)
            for (var col = startColumn; col <= lastColumn; col++)
            {
                if (grid[row][col] == '@') count++;
            }

            return count;
        }
    }
}