namespace AoC2022
{
    public class Solution202224 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution12.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            int[,] map = new int[lines[0].Trim().Length, lines.Length];
            var i = 0;
            var endX = 0;
            var endY = 0;
            foreach (var line in lines)
            {
                var j = 0;
                foreach (var cr in line.Trim())
                {
                    if (cr == 'S')
                    {
                        map[j, i] = 1;
                    }
                    else if (cr == 'E')
                    {
                        map[j, i] = 26;
                        endX = j;
                        endY = i;
                    }
                    else
                    {
                        map[j, i] = cr - 96;
                    }
                    j++;
                }
                i++;
            }
            var min = int.MaxValue;
            for (var i1 = 0; i1 < lines[0].Trim().Length; i1++)
            {
                for (var j1 = 0; j1 < lines.Length; j1++)
                {
                    if (map[i1, j1] == 1)
                    {
                        var currentMin = FindMin(map, i1, j1, endX, endY);
                        if (currentMin < min)
                        {
                            min = currentMin;
                        }
                    }
                }
            }

            return (min).ToString();
        }

        private static int FindMin(int[,] map, int beginX, int beginY, int endX, int endY)
        {
            var pq = new PriorityQueue<(int, int, int), int>();
            var visited = new List<(int, int)>();
            pq.Enqueue((beginX, beginY, 0), 0);
            while (pq.Count > 0)
            {
                var t = pq.Dequeue();
                if (t.Item1 == endX && t.Item2 == endY)
                {
                    return t.Item3;
                    break;
                }
                var current = map[t.Item1, t.Item2];
                if (t.Item2 + 1 < map.GetLength(1) && map[t.Item1, t.Item2 + 1] <= current + 1)
                {
                    if (!visited.Contains((t.Item1, t.Item2 + 1)))
                    {
                        pq.Enqueue((t.Item1, t.Item2 + 1, t.Item3 + 1), t.Item3 + 1);
                        visited.Add((t.Item1, t.Item2 + 1));
                    }
                }
                if (t.Item2 - 1 >= 0 && map[t.Item1, t.Item2 - 1] <= current + 1)
                {
                    if (!visited.Contains((t.Item1, t.Item2 - 1)))
                    {
                        pq.Enqueue((t.Item1, t.Item2 - 1, t.Item3 + 1), t.Item3 + 1);
                        visited.Add((t.Item1, t.Item2 - 1));
                    }
                }
                if (t.Item1 + 1 < map.GetLength(0) && map[t.Item1 + 1, t.Item2] <= current + 1)
                {
                    if (!visited.Contains((t.Item1 + 1, t.Item2)))
                    {
                        pq.Enqueue((t.Item1 + 1, t.Item2, t.Item3 + 1), t.Item3 + 1);
                        visited.Add((t.Item1 + 1, t.Item2));
                    }
                }
                if (t.Item1 - 1 >= 0 && map[t.Item1 - 1, t.Item2] <= current + 1)
                {
                    if (!visited.Contains((t.Item1 - 1, t.Item2)))
                    {
                        pq.Enqueue((t.Item1 - 1, t.Item2, t.Item3 + 1), t.Item3 + 1);
                        visited.Add((t.Item1 - 1, t.Item2));
                    }
                }
            }
            return int.MaxValue;
        }
    }
}