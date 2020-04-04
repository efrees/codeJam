using System;
using System.Text;

// You Can Go Your Own Way
public class SolutionB
{
	public static void Main()
	{
        var t = int.Parse(Console.ReadLine());

        var k = 1;
        while (k <= t)
        {
            var n = int.Parse(Console.ReadLine());
            var lydiaPath = Console.ReadLine();

            var pathBuilder = new StringBuilder(2*n);
            var lydiaOffset = 0;

            for(var i = 0; i < n - 1; i++)
            {
                if (lydiaOffset > 0)
                {
                    pathBuilder.Append("SE");
                }
                else if (lydiaOffset < 0)
                {
                    pathBuilder.Append("ES");
                }
                else if (lydiaPath[i*2] == 'E')
                {
                    pathBuilder.Append("SE");
                }
                else if (lydiaPath[i*2] == 'S')
                {
                    pathBuilder.Append("ES");
                }
                
                lydiaOffset += lydiaPath[i*2] == 'E' ? 1 : -1;
                lydiaOffset += lydiaPath[i*2+1] == 'E' ? 1 : -1;
            }


            Console.WriteLine($"Case #{k}: {pathBuilder}");
            k++;
        }
	}
}
