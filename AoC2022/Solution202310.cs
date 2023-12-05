namespace AoC2022
{
    public class Solution202310 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution5.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var seeds = lines[0].Trim().Split(":")[1].Trim().Split(" ").Select(x => long.Parse(x)).ToList();
            var ranges = new List<(long, long)>();
            for(var j = 0; j < seeds.Count; j+=2)
            {
                ranges.Add((seeds[j], seeds[j + 1]));
            }
            var i = 3;
            var queue = new Queue<(long, long)>(ranges);
            while (i < lines.Length)
            {
                var map = new Dictionary<(long, long), long>();
                while (i < lines.Length && !string.IsNullOrEmpty(lines[i].Trim()))
                {
                    var split = lines[i].Split(" ");
                    map.Add((long.Parse(split[1]), long.Parse(split[2])), long.Parse(split[0]));
                    i++;
                }
                Console.WriteLine("Status");
                foreach (var range in queue)
                {
                    Console.WriteLine($"{string.Format("{0:#,0}", range.Item1)}-{string.Format("{0:#,0}", range.Item1 + range.Item2 - 1)}");
                }
                Console.WriteLine("Mapping");
                var newRanges = new List<(long, long)>();
                while (queue.Count > 0)
                {
                    var range = queue.Dequeue();
                    var minRange = range.Item1;
                    var maxRange = range.Item1 + range.Item2 - 1;
                    foreach (var kvp in map)
                    {
                        var minMap = kvp.Key.Item1;
                        var maxMap = kvp.Key.Item1 + kvp.Key.Item2 - 1;
                        if (minRange < minMap && maxRange >= minMap && maxRange <= maxMap)
                        {
                            newRanges.Add((kvp.Value, maxRange - minMap + 1));
                            //Console.WriteLine($"Mapping1 - {string.Format("{0:#,0}", minMap)}-{string.Format("{0:#,0}", maxMap)}({kvp.Key.Item2})");
                            //Console.WriteLine($"Old range - {string.Format("{0:#,0}", minRange)}-{string.Format("{0:#,0}", maxRange)}({range.Item2})");
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", kvp.Value)}-{string.Format("{0:#,0}", kvp.Value + maxRange - minMap)}({maxRange - minMap})");
                            maxRange = minMap - 1;
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", minRange)}-{string.Format("{0:#,0}", maxRange)}({})");
                        }

                        if (minMap <= minRange && minRange <= maxMap && minMap <= maxRange && maxRange <= maxMap)
                        {

                            //Console.WriteLine($"Mapping2 - {string.Format("{0:#,0}", minMap)}-{string.Format("{0:#,0}", maxMap)}");
                            //Console.WriteLine($"Old range - {string.Format("{0:#,0}", minRange)}-{string.Format("{0:#,0}", maxRange)}");
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", kvp.Value + minRange - minMap)}-{string.Format("{0:#,0}", kvp.Value + minRange - minMap + range.Item2 - 1)}");
                            newRanges.Add((kvp.Value + minRange - minMap, maxRange - minRange + 1));
                            minRange = 0;
                            maxRange = 0;
                            break;
                        }

                        if (maxRange > maxMap && minRange >= minMap && minRange <= maxMap)
                        {
                            newRanges.Add((kvp.Value + minRange - minMap, maxMap - minRange + 1));
                            minRange = maxMap + 1;

                            //Console.WriteLine($"Mapping3 - {string.Format("{0:#,0}", minMap)}-{string.Format("{0:#,0}", maxMap)}");
                            //Console.WriteLine($"Old range - {string.Format("{0:#,0}", minRange)}-{string.Format("{0:#,0}", maxRange)}");
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", kvp.Value + minRange - minMap)}-{string.Format("{0:#,0}", kvp.Value + maxMap - minMap)}");
                            //minRange = minMap - 1;
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", minRange)}-{string.Format("{0:#,0}", maxRange)}");
                        }

                        if (minRange < minMap && minMap < maxRange && minRange < maxMap && maxMap < maxRange)
                        {
                            newRanges.Add((kvp.Value, maxMap - minMap + 1));
                            queue.Enqueue((maxMap +1, maxRange - maxMap));
                            //Console.WriteLine($"Mapping4 - {string.Format("{0:#,0}", minMap)}-{string.Format("{0:#,0}", maxMap)}");
                            //Console.WriteLine($"Old range - {string.Format("{0:#,0}", minRange)}-{string.Format("{0:#,0}", maxRange)}");
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", kvp.Value)}-{string.Format("{0:#,0}", kvp.Value + maxMap - minMap)}");
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", maxMap + 1)}-{string.Format("{0:#,0}", maxRange)}");
                            maxRange = minMap - 1;
                            //Console.WriteLine($"New range - {string.Format("{0:#,0}", minRange)}-{string.Format("{0:#,0}", maxRange)}");
                        }
                    }
                    if (minRange != 0 && maxRange != 0)
                    {
                        newRanges.Add((minRange, maxRange - minRange + 1));
                    }
                }
                foreach (var newRange in newRanges)
                {
                    queue.Enqueue(newRange);
                }
                i += 2;
            }

            return queue.Select(x => x.Item1).Min().ToString();
        }
    }
}