using System;
using System.Linq;

namespace PracticeRound
{
    public class ProgramA
    {
        public static void MainA()
        {
            var cases = Convert.ToInt32(Console.ReadLine());
            while (cases > 0)
            {
                var bounds = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var guessLimit = Console.ReadLine();

                InteractiveBinarySearch(int.Parse(bounds[0]), int.Parse(bounds[1]));

                cases--;
            }
        }

        private static bool InteractiveBinarySearch(int a, int b)
        {
            while (true)
            {
                var guess = a + (b - a + 1) / 2;
                Console.WriteLine(guess);
                Console.Out.Flush();
                var response = Console.ReadLine();

                switch (response)
                {
                    case "CORRECT":
                        return true;
                    case "TOO_BIG":
                        b = guess - 1;
                        continue;
                    case "TOO_SMALL":
                        a = guess;
                        continue;
                }

                return false;
            }
        }
    }
}