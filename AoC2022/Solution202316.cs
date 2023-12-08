namespace AoC2022
{
    public class Solution202316 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution8.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var instructions = lines[0].Trim();
            var nodes = new Dictionary<string, (string, string)>();
            var i = 2;
            while(i < lines.Length)
            {
                var line = lines[i];
                var begin = line.Split("=")[0].Trim();
                var left = line.Split("=")[1].Split(",")[0].Replace("(", "").Trim();
                var right = line.Split("=")[1].Split(",")[1].Replace(")", "").Trim();
                nodes.Add(begin, (left, right));
                i++;
            }

            var steps = nodes.Select(x => x.Key).Where(x => x.EndsWith("A")).ToList();
            var cache = new Dictionary<int, List<(string, int, int)>>();
            for (var j = 0; j < steps.Count; j++)
            {
                cache.Add(j, new List<(string, int, int)>());
            }
            var sum = 0;
            var index = 0;
            var donesteps = new List<int>();
            while(donesteps.Count != steps.Count)
            {
                var dir = instructions[index];
                if (dir == 'L')
                {
                    var newSteps = new List<string>();
                    foreach (var step in steps)
                    {
                        newSteps.Add(nodes[step].Item1);
                        
                    }
                    steps = newSteps;
                }
                else
                {
                    var newSteps = new List<string>();
                    foreach (var step in steps)
                    {
                        newSteps.Add(nodes[step].Item2);
                    }
                    steps = newSteps;
                }
                for (var j = 0; j < steps.Count; j++)
                {
                    if (!donesteps.Contains(j) && cache[j].Any(x => x.Item1 == steps[j] && x.Item2 == index))
                    {
                        donesteps.Add(j);
                        if (steps[j].EndsWith("Z"))
                        {
                            cache[j].Add((steps[j], index, sum));
                        }
                    }
                    if (!donesteps.Contains(j) && steps[j].EndsWith("Z"))
                    {
                        cache[j].Add((steps[j], index, sum));
                    }
                   
                }
                sum++;
                index++;
                index %= instructions.Length;
            }

            var diff = new List<int>();
            var start = new List<int>();
            for (var j = 0; j < steps.Count; j++)
            {
                diff.Add(cache[j][1].Item3 - cache[j][0].Item3);
                start.Add(cache[j][0].Item3);
            }
            var found = false; 
            long nr = start[0];
            while(!found)
            {
                nr += diff[0];
                if (nr % diff[1] == start[1] && nr % diff[2] == start[2] && nr % diff[3] == start[3] && nr % diff[4] == start[4] && nr % diff[5] == start[5])
                {
                    found = true;
                }
            }
            return (nr+1).ToString();
        }
    }
}