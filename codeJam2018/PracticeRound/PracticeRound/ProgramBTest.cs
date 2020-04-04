using System.Linq;
using NUnit.Framework;

namespace PracticeRound
{
    public class ProgramBTest
    {
        [Test]
        public void evacuate_all_members()
        {
            var initialParties = new[] { 3, 7, 4 };

            var actualPlan = ProgramB.PlanEvacuation(initialParties);

            var evacuatedCounts = actualPlan.Where(char.IsLetter)
                .ToLookup(x => x, x => 1);
            Assert.AreEqual(3, evacuatedCounts['A'].Count(), "A");
            Assert.AreEqual(7, evacuatedCounts['B'].Count(), "B");
            Assert.AreEqual(4, evacuatedCounts['C'].Count(), "C");
        }

        [Test]
        public void maintain_tie_with_two_parties()
        {
            var initialParties = new[] { 5, 5 };
            var expectedPlan = "AB AB AB AB AB";

            var actualPlan = ProgramB.PlanEvacuation(initialParties);

            Assert.AreEqual(expectedPlan, actualPlan);
        }

        [Test]
        public void do_not_evacuate_more_than_remaining()
        {
            var initialParties = new[] { 3, 3, 3 };

            var actualPlan = ProgramB.PlanEvacuation(initialParties);

            var evacuatedCounts = actualPlan.Where(char.IsLetter).GroupBy(x => x);
            Assert.AreEqual(3, evacuatedCounts.Max(g => g.Count()));
        }

        [Test]
        public void prevent_majority_with_three_parties()
        {
            var initialParties = new[] { 4, 4, 3 };
            var expectedPlan = "AB AB CC AB A BC";

            var actualPlan = ProgramB.PlanEvacuation(initialParties);

            Assert.AreEqual(expectedPlan, actualPlan);
        }
    }
}
