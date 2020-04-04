using System;
using System.Linq;

namespace QualificationRound
{
    public class ProgramB
    {
        public static void MainB()
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var N = Convert.ToInt32(Console.ReadLine());
                var arrayString = Console.ReadLine();
                var array = Array.ConvertAll(arrayString.Split(' '), int.Parse);

                var evens = array.Where((number, index) => index % 2 == 0).ToArray();
                var odds = array.Where((number, index) => index % 2 != 0).ToArray();

                Array.Sort(evens);
                Array.Sort(odds);

                var unsortedIndex = -1;
                for (var i = 0; i < evens.Length; i++)
                {
                    if (i >= odds.Length) break;
                    if (evens[i] > odds[i])
                    {
                        unsortedIndex = i * 2;
                        break;
                    }

                    if (i < evens.Length - 1&& odds[i] > evens[i+1])
                    {
                        unsortedIndex = i * 2 + 1;
                        break;
                    }
                }

                var answer = unsortedIndex < 0 ? "OK" : unsortedIndex.ToString();
                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }
    }
}
