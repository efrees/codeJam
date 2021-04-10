using System;
using System.Linq;

namespace Round1A
{
    class AppendSort
    {
        static void Main(string[] args)
        {
            var T = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= T)
            {
                var N = int.Parse(Console.ReadLine());
                var array = Console.ReadLine().Split().Select(long.Parse).ToArray();

                var answer = 0;
                var previous = 0L;

                foreach (var item in array)
                {
                    if (previous < item)
                    {
                        // already done
                        previous = item;
                        continue;
                    }

                    var previousAsString = previous.ToString();
                    var itemAsString = item.ToString();

                    var previousPrefix = previousAsString.Substring(0, itemAsString.Length);
                    var previousPostfix = previousAsString.Substring(itemAsString.Length); //can be empty
                    var prefixValue = long.Parse(previousPrefix);
                    if (prefixValue > item
                        || (prefixValue == item && previousPostfix.All(c => c == '9')))
                    {
                        //We have to make it longer, so just go with the smallest longer number, adding zeros
                        var digitsToAdd = previousAsString.Length + 1 - itemAsString.Length;
                        answer += digitsToAdd;

                        previous = item;
                        while (digitsToAdd > 0)
                        {
                            digitsToAdd--;
                            previous *= 10;
                        }
                    }
                    else
                    {
                        // We can make it larger at the same length, if remaining digits aren't 9's or if item is already greater than prefix.
                        var digitsToAdd = previousAsString.Length - itemAsString.Length;
                        answer += digitsToAdd;

                        if (prefixValue == item)
                        {
                            // we have to increment the postfix
                            var incrementedPostfix = previousPostfix.ToCharArray();
                            var incremented = false;
                            for (var i = 0; i < incrementedPostfix.Length; i++)
                            {
                                if (!incremented && incrementedPostfix[i] != '9')
                                {
                                    incrementedPostfix[i] = (char)(incrementedPostfix[i] + 1);
                                    incremented = true;
                                }
                                else if (incremented)
                                {
                                    incrementedPostfix[i] = '0';
                                }
                            }

                            var newValueAsString = previousPrefix + incrementedPostfix;
                            previous = long.Parse(newValueAsString);
                        }
                        else
                        {
                            // we can just fill with zeros
                            previous = item;
                            while (digitsToAdd > 0)
                            {
                                digitsToAdd--;
                                previous *= 10;
                            }
                        }
                    }
                }

                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }
    }
}