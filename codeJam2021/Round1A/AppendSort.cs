using System;
using System.Diagnostics;
using System.Linq;

namespace Round1A
{
    class AppendSort
    {
        static void MainA(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var N = int.Parse(Console.ReadLine());
                var array = Console.ReadLine().Split().ToArray();

                var answer = 0;
                var previous = "0";

                foreach (var item in array)
                {
                    var firstDifferingDigits = GetFirstDifferingDigits(previous, item);

                    var previousIsLessThanCurrent = previous.Length < item.Length
                                                    || (previous.Length == item.Length && firstDifferingDigits != null &&
                                                        firstDifferingDigits.PreviousIsLess());
                    if (previousIsLessThanCurrent)
                    {
                        // already done
                        previous = item;
                    Debug.WriteLine(previous);
                        continue;
                    }

                    // Now, previous is known to be longer or equal length to item

                    var previousPrefix = previous.Substring(0, item.Length);
                    var previousPostfix = previous.Substring(item.Length); //can be empty

                    var firstDifferingInPrefix = GetFirstDifferingDigits(previousPrefix, item);
                    var previousPrefixIsLarger =  firstDifferingInPrefix != null && firstDifferingInPrefix.PreviousIsGreater();

                    if (previousPrefixIsLarger
                        || (previousPrefix == item && previousPostfix.All(c => c == '9')))
                    {
                        //We have to make it longer, so just go with the smallest longer number, adding zeros
                        var digitsToAdd = previous.Length + 1 - item.Length;
                        answer += digitsToAdd;

                        previous = item + new string('0', digitsToAdd);
                    }
                    else
                    {
                        // We can make it larger at the same length, if remaining digits aren't 9's or if item is already greater than prefix.
                        var digitsToAdd = previous.Length - item.Length;
                        answer += digitsToAdd;

                        if (previousPrefix == item)
                        {
                            // we have to increment the postfix
                            // 10 | 00 => 10 | 01
                            // 10 | 009 => 10 | 010
                            // 10 | 209 => 10 | 210
                            var incrementedPostfix = previousPostfix.ToCharArray();
                            var incremented = false;
                            for (var i =  incrementedPostfix.Length -1; i >= 0; i--)
                            {
                                if (incremented)
                                {
                                    continue;
                                }

                                if (incrementedPostfix[i] == '9')
                                {
                                    incrementedPostfix[i] = '0';
                                }
                                else
                                {
                                    incrementedPostfix[i] = (char)(incrementedPostfix[i] + 1);
                                    incremented = true;
                                }
                            }

                            var newValueAsString = previousPrefix + new string(incrementedPostfix);
                            previous = newValueAsString;
                        }
                        else
                        {
                            // we can just fill with zeros
                            previous = item + new string('0', digitsToAdd);
                        }
                    }
                    Debug.WriteLine(previous);
                }

                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        private static CharPair GetFirstDifferingDigits(string previous, string item)
        {
            return previous
                .Zip(item, (cp, ci) => new CharPair { prev = cp, cur = ci })
                .FirstOrDefault(x => x.prev != x.cur);
        }

        class CharPair
        {
            public char prev;
            public char cur;

            public bool PreviousIsLess()
            {
                return prev < cur;
            }

            public bool PreviousIsGreater()
            {
                return prev > cur;
            }
        }
    }
}