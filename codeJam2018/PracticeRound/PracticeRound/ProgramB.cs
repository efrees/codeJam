using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeRound
{
    public class ProgramB
    {
        public static void MainB()
        {
            var cases = Convert.ToInt32(Console.ReadLine());
            var k = 1;
            while (k <= cases)
            {
                var numParties = Console.ReadLine();
                var partySizes = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var answer = PlanEvacuation(partySizes);
                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        internal static string PlanEvacuation(int[] partySizes)
        {
            var plan = new StringBuilder();
            var totalMembers = partySizes.Sum();
            var parties = partySizes.Select((size, index) => new Party(index, size))
                .OrderByDescending(p => p.Remaining)
                .ToArray();

            while (totalMembers > 0)
            {
                var largestParty = parties[0];
                var nextLargestParty = parties[1];

                if (totalMembers == 3 && largestParty.Remaining == 1)
                {
                    plan.Append(largestParty.Name);
                    largestParty.Remaining = 0;
                    totalMembers--;
                }
                else if (largestParty.Remaining == nextLargestParty.Remaining)
                {
                    //tie
                    plan.Append(largestParty.Name);
                    plan.Append(nextLargestParty.Name);
                    largestParty.Remaining--;
                    nextLargestParty.Remaining--;
                    totalMembers -= 2;
                }
                else
                {
                    plan.Append(largestParty.Name);
                    plan.Append(largestParty.Name);
                    largestParty.Remaining -= 2;
                    totalMembers -= 2;
                }

                plan.Append(' ');
                InsertIfUnsorted(parties, 1);
                InsertIfUnsorted(parties, 0);
            }

            return plan.ToString().TrimEnd();
        }

        private static void InsertIfUnsorted(IList<Party> parties, int index)
        {
            var elementToResort = parties[index];
            var compareIndex = index + 1;
            while (compareIndex < parties.Count
                   && elementToResort.Remaining < parties[compareIndex].Remaining)
            {
                parties[compareIndex - 1] = parties[compareIndex];
                compareIndex++;
            }

            parties[compareIndex - 1] = elementToResort;
        }

        private class Party
        {
            public char Name { get; }
            public int Remaining { get; set; }

            public Party(int index, int remaining)
            {
                Name = (char) ('A' + index);
                Remaining = remaining;
            }
        }
    }
}