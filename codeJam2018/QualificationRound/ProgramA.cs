using System;

namespace QualificationRound
{
    public class ProgramA
    {
        public static void MainA()
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var caseDetails = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var D = int.Parse(caseDetails[0]);
                var P = caseDetails[1].ToCharArray();
                 
                var programDamage = DamageFromProgram(P);

                var hackCount = 0;
                var answer = "PENDING";
                while (programDamage > D)
                {
                    if (PerformBestHack(P))
                    {
                        hackCount++;
                        programDamage = DamageFromProgram(P);
                    }
                    else
                    {
                        answer = "IMPOSSIBLE";
                        break;
                    }
                }

                if (answer == "PENDING")
                {
                    answer = hackCount.ToString();
                }

                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        private static int DamageFromProgram(char[] program)
        {
            var power = 1;
            var damageSum = 0;
            foreach (var c in program)
            {
                if (c == 'S')
                {
                    damageSum += power;
                }

                if (c == 'C')
                {
                    power *= 2;
                }
            }

            return damageSum;
        }

        private static bool PerformBestHack(char[] c)
        {
            var hackIndex = c.Length - 2;
            while (hackIndex >= 0)
            {
                if (c[hackIndex] == 'C' && c[hackIndex + 1] == 'S')
                {
                    c[hackIndex] = 'S';
                    c[hackIndex + 1] = 'C';
                    return true;
                }

                hackIndex--;
            }

            return false;
        }
    }
}
