namespace AoC2022
{
    public class Solution20239 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution5.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var seeds = lines[0].Trim().Split(":")[1].Trim().Split(" ").Select(x => long.Parse(x)).ToList();
            var i = 3;
            while(i < lines.Length)
            {
                var map = new Dictionary<(long, long), long>();
                while (i < lines.Length && !string.IsNullOrEmpty(lines[i].Trim()))
                {
                    var split = lines[i].Split(" ");
                    map.Add((long.Parse(split[1]), long.Parse(split[2])), long.Parse(split[0]));
                    i++;
                }
                var newSeeds = new List<long>();
                foreach(var seed in seeds)
                {
                    var newSeed = seed;
                    foreach(var kvp in map)
                    {
                        if (seed >= kvp.Key.Item1 && seed < kvp.Key.Item1 + kvp.Key.Item2)
                        {
                            newSeed = kvp.Value + (seed - kvp.Key.Item1);
                            break;
                        }
                    }
                    newSeeds.Add(newSeed);
                }
                seeds = newSeeds;
                i += 2;
            }

            return seeds.Min().ToString();
        }
    }
}