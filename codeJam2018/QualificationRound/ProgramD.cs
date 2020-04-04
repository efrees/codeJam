using System;
using System.Dynamic;

namespace QualificationRound
{
    public class ProgramD
    {
        public static void Main()
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var targetArea = Convert.ToDouble(Console.ReadLine());

                //Only handles small case, which can always be answered with a single rotation.
                // sin(x) + cos(x) = targetArea

                var minRotation = 0.0;
                var maxRotation = Math.PI / 4;

                const double epsilon = 0.0000001;
                var iterationLimit = 100;
                var iterationCount = 0;
                double xAxisRotationAngle = 0.0;
                double zAxisRotationAngle;
                double resultingArea;

                do
                {
                    zAxisRotationAngle = (maxRotation - minRotation) / 2 + minRotation;
                    resultingArea = Math.Sin(zAxisRotationAngle) + Math.Cos(zAxisRotationAngle);

                    if (resultingArea > targetArea)
                    {
                        maxRotation = zAxisRotationAngle;
                    }
                    else if (resultingArea < targetArea)
                    {
                        minRotation = zAxisRotationAngle;
                    }

                    iterationCount++;
                } while (iterationCount < iterationLimit && Math.Abs(resultingArea - targetArea) > epsilon);

                if (resultingArea < targetArea && Math.Abs(resultingArea - targetArea) > epsilon)
                {
                    //try rotating around the x-axis to get the large case working
                    minRotation = 0.0;
                    maxRotation = Math.PI / 4;

                    iterationCount = 0;

                    var originalResultingArea = resultingArea;
                    do
                    {
                        xAxisRotationAngle = (maxRotation - minRotation) / 2 + minRotation;
                        resultingArea = originalResultingArea * Math.Cos(xAxisRotationAngle)
                                        + Math.Sin(xAxisRotationAngle);

                        if (resultingArea > targetArea)
                        {
                            maxRotation = xAxisRotationAngle;
                        }
                        else if (resultingArea < targetArea)
                        {
                            minRotation = xAxisRotationAngle;
                        }

                        iterationCount++;
                    } while (iterationCount < iterationLimit && Math.Abs(resultingArea - targetArea) > epsilon);
                }

                var sinOfAngle = Math.Sin(zAxisRotationAngle);
                var cosOfAngle = Math.Cos(zAxisRotationAngle);

                var rightFace = new Coord
                {
                    X = 0.5 * sinOfAngle,
                    Y = 0.5 * cosOfAngle * Math.Cos(xAxisRotationAngle),
                    Z = 0.5 * cosOfAngle * Math.Sin(xAxisRotationAngle)
                };

                var topFace = new Coord
                {
                    X = -0.5 * cosOfAngle,
                    Y = 0.5 * sinOfAngle * Math.Cos(xAxisRotationAngle),
                    Z = 0.5 * sinOfAngle * Math.Sin(xAxisRotationAngle)
                };

                var frontFace = new Coord
                {
                    X = 0,
                    Y = 0.5 * Math.Sin(xAxisRotationAngle),
                    Z = 0.5 * Math.Cos(xAxisRotationAngle)
                };

                Console.WriteLine($"Case #{k}:");
                Console.WriteLine(rightFace.ToString());
                Console.WriteLine(topFace.ToString());
                Console.WriteLine(frontFace.ToString());
                k++;
            }
        }

        private class Coord
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

            public override string ToString()
            {
                return $"{X} {Y} {Z}";
            }
        }
    }
}