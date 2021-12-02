namespace AoC2022
{
    public class Solution4 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution2.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        public string BaseAlgorithm(string[] lines)
        {
            var aim = 0;
            var depth = 0;
            var hor = 0;
            foreach(var line in lines)
            {
                if (line.StartsWith("forward"))
                {
                    var val = int.Parse(line.Split(" ")[1]);
                    hor += int.Parse(line.Split(" ")[1]);
                    depth += val * aim;
                }
                else if (line.StartsWith("up"))
                {
                    aim -= int.Parse(line.Split(" ")[1]);
                }
                else if (line.StartsWith("down"))
                {
                    aim += int.Parse(line.Split(" ")[1]);
                }
            }
            return (depth * hor).ToString();
        }
    }
}