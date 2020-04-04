using System;
using System.Collections.Generic;
using System.Linq;

namespace Round1A
{
    public class AlienRhyme
    {
        static void _Main(string[] args)
        {
            var t = int.Parse(Console.ReadLine());

            var k = 1;
            while (k <= t)
            {
                var n = int.Parse(Console.ReadLine());

                var wordsTrie = new EndTrie();
                for (int i = 0; i < n; i++)
                {
                    wordsTrie.Add(Console.ReadLine());
                }

                var answer = wordsTrie.PairOffAndRemove() * 2;
                Console.WriteLine($"Case #{k}: {answer}");
                k++;
            }
        }

        private class EndTrie
        {
            public static readonly EndTrie NullTrie = new EndTrie();

            private int Count { get; set; }
            private readonly Dictionary<char, EndTrie> _section = new Dictionary<char, EndTrie>();

            public void Add(string word)
            {
                Count++;

                if (word == string.Empty)
                {
                    _section['0'] = NullTrie;
                    return;
                }

                var key = word.Length > 0 ? word.Last() : '0';
                if (!_section.ContainsKey(key))
                {
                    _section[key] = new EndTrie();
                }

                _section[key].Add(word.Substring(0, word.Length - 1));
            }

            public int PairOffAndRemove(bool countWithoutPrefixMatch = false)
            {
                var pairCount = 0;

                foreach (var pair in _section.Where(p => p.Value.Count > 1))
                {
                    var deeperCount = pair.Value.PairOffAndRemove(true);
                    Count -= deeperCount * 2;
                    pairCount += deeperCount;
                }

                var singlesCount = _section.Values.Count(t => t.Count == 1 || t == NullTrie);
                if (countWithoutPrefixMatch && singlesCount > 1)
                {
                    Count -= 2;
                    pairCount += 1;
                }


                return pairCount;
            }
        }
    }
}