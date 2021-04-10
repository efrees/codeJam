using System;
using System.Linq;

namespace Round1A
{
    class HackedExam
    {
        public static void MainC()
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var parts = Console.ReadLine().Split().ToArray();
                var N = int.Parse(parts[0]);
                var Q = int.Parse(parts[1]);

                var knownScores = Enumerable.Range(0, N)
                    .Select(_ => Console.ReadLine().Split())
                    .Select(split => new TestResult{ Answers = split[0], Score = int.Parse(split[1])})
                    .ToList();

                // FTFT - 2
                // TFTF - 2

                // FTFT - 1
                // TFTF - 3

                // FFTF - 2
                // FFFT - 2

                // FFTF - 1
                // FFFT - 3

                // FFTF - 1
                // FFFT - 1

                // FFTF - 0 IMPOSSIBLE
                // FFFT - 0 IMPOSSIBLE

                // FFTFT - 1
                // FFFTF - 2

                // Odd number of differences (between two students) means their scores must be different

                var answer = "";
                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        class TestResult
        {
            public string Answers { get; set; }
            public int Score { get; set; }
        }
    }
}