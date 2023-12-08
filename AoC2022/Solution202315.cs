namespace AoC2022
{
    public class Solution202315 : IProvideSolution
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

            var step = "AAA";
            var sum = 0;
            var index = 0;
            while(step != "ZZZ")
            {
                var dir = instructions[index];
                if (dir == 'L')
                {
                    step = nodes[step].Item1;
                }
                else
                {
                    step = nodes[step].Item2;
                }
                sum++;
                index++;
                index %= instructions.Length;
            }
            return sum.ToString();
        }
    }
}