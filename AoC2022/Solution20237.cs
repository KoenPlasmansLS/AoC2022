namespace AoC2022
{
    public class Solution20237 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution4.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                var point = 0;
                var split = lines[i].Split(":")[1].Split("|");
                var left = split[0].Trim();
                var right = split[1].Trim();
                var winning = left.Replace("  ", " ").Split(" ").Select(x => int.Parse(x));
                var have = right.Replace("  ", " ").Split(" ").Select(x => int.Parse(x));
                foreach(var ihave in have)
                {
                    if (winning.Contains(ihave))
                    {
                        if (point == 0)
                        {
                            point = 1;
                        }
                        else
                        {
                            point *= 2;
                        }
                    }
                }
                sum += point;
            }
            return sum.ToString();
        }
    }
}