using System;
using System.Collections.Generic;
using System.Linq;

namespace QualifyingRound
{
    public class ParentingPartnering
    {
        public static void Main()
        {
            var t = int.Parse(Console.ReadLine());
            var k = 1;
            while (k <= t)
            {
                var n = int.Parse(Console.ReadLine());
                var scheduleMinutes = new int[24 * 60];
                var impossible = false;
                var edges = new HashSet<Edge>();

                for (var i = 0; i < n; i++)
                {
                    var activityTokens = Console.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    var start = Convert.ToInt32(activityTokens[0]);
                    var end = Convert.ToInt32(activityTokens[1]);

                    for (var j = start; j < end && !impossible; j++)
                    {
                        switch (scheduleMinutes[j])
                        {
                            case -1: // third activity overlapping
                                impossible = true;
                                break;
                            case 0: //No overlap yet
                                scheduleMinutes[j] = i + 1;
                                break;
                            default: //First overlap
                                var other = scheduleMinutes[j] - 1;
                                scheduleMinutes[j] = -1; // "locked"
                                edges.Add(new Edge { V1 = i, V2 = other });
                                edges.Add(new Edge { V1 = other, V2 = i });
                                // undirected graph ensures we will visit an entire connected sub-graph in one go
                                // so we can color our starting node for each group arbitrarily
                                break;
                        }
                    }
                }

                var graph = edges.ToLookup(e => e.V1, e => e.V2);
                var assignedParents = new char[n];
                var startActivity = 0;

                while (!impossible && startActivity < assignedParents.Length)
                {
                    if (assignedParents[startActivity] != default(char))
                    {
                        startActivity++;
                        continue;
                    }

                    impossible = !ColorGraph(graph, startActivity, 'J', assignedParents);
                }

                var result = impossible ? "IMPOSSIBLE" : new string(assignedParents);
                Console.WriteLine($"Case #{k}: {result}");
                k++;
            }
        }

        // Returns true if successful
        private static bool ColorGraph(ILookup<int, int> graph, int activity, char parent, char[] assignedParents)
        {
            var oppositeParent = parent == 'J' ? 'C' : 'J';

            if (assignedParents[activity] == oppositeParent)
            {
                return false; // conflict
            }

            if (assignedParents[activity] == parent)
            {
                return true; // this node is already done
            }

            assignedParents[activity] = parent;

            foreach (var connected in graph[activity])
            {
                var possible = ColorGraph(graph, connected, oppositeParent, assignedParents);

                if (!possible)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public struct Edge
    {
        public int V1 { get; set; }
        public int V2 { get; set; }
    }
}