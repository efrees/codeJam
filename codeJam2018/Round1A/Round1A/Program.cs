using System;
using System.Linq;

namespace Round1A
{
    public class Program
    {
        static void Main(string[] args)
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

        private static double SolveCase()
        {
            var np = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            var N = np[0];
            var targetP = np[1];

            var cookies = new Cookie[N];
            for (var i = 0; i < N; i++)
            {
                cookies[i] = Cookie.Parse(Console.ReadLine());
            }

            Array.Sort(cookies, CompareByMaxPerimeter);
            var basePerimeter = cookies.Sum(c => c.MinPerimeter);
            var maxPerimeter = cookies.Sum(c => c.MaxPerimeter);

            var bestSoFar = basePerimeter;
            var currentPerimeter = basePerimeter;
            var lastCutCookie = -1;
            for (var i = 0; i < cookies.Length; i++)
            {
                if (currentPerimeter + cookies[i].MinIncrease > targetP)
                {
                    break;
                }

                currentPerimeter += cookies[i].MinIncrease;
                lastCutCookie = i;
            }

            var firstCookieFromEnd = cookies.Length;
            for (var j = cookies.Length - 1; j > lastCutCookie; j--)
            {
                if (currentPerimeter + cookies[j].MinIncrease > targetP)
                {
                    break;
                }

                currentPerimeter += cookies[j].MinIncrease;
                firstCookieFromEnd = j;
            }

            //all these cookies in play can be increased in perimeter

            for (var i = 0; i <= lastCutCookie && currentPerimeter < targetP; i++)
            {
                var difference = cookies[i].MaxIncrease - cookies[i].MinIncrease;
                if (currentPerimeter + difference < targetP)
                {
                    currentPerimeter += difference;
                }
                else
                {
                    currentPerimeter = targetP;
                }
            }

            for (var i = firstCookieFromEnd; i < cookies.Length && currentPerimeter < targetP; i++)
            {
                var difference = cookies[i].MaxIncrease - cookies[i].MinIncrease;
                if (currentPerimeter + difference < targetP)
                {
                    currentPerimeter += difference;
                }
                else
                {
                    currentPerimeter = targetP;
                }
            }

            return currentPerimeter;
        }

        private static int CompareByMaxPerimeter(Cookie dimension1, Cookie dimension2)
        {
            return dimension1.MaxPerimeter.CompareTo(dimension2.MaxPerimeter);
        }

        internal class Cookie
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public double MinPerimeter => (Width + Height) * 2;

            public double MinCutPerimeter => MinPerimeter + 2 * Math.Min(Width, Height);
            public double MinIncrease => MinCutPerimeter - MinPerimeter;

            public double MaxPerimeter { get; set; }
            public double MaxIncrease => MaxPerimeter - MinPerimeter;

            public static Cookie Parse(string line)
            {
                var data = Array.ConvertAll(line.Split(' '), int.Parse);
                var dimension = new Cookie
                {
                    Width = data[0],
                    Height = data[1]
                };
                dimension.MaxPerimeter = (dimension.Width + dimension.Height) * 2
                                         + 2* Math.Sqrt(dimension.Width * dimension.Width +
                                                     dimension.Height * dimension.Height);
                return dimension;
            }
        }
    }
}
