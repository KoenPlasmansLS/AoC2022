namespace AoC2022
{
    public class Solution3 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution2.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        public string BaseAlgorithm(string[] lines)
        {
            var depth = 0;
            var hor = 0;
            foreach(var line in lines)
            {
                if (line.StartsWith("forward"))
                {
                    hor += int.Parse(line.Split(" ")[1]);
                }
                else if (line.StartsWith("up"))
                {
                    depth -= int.Parse(line.Split(" ")[1]);
                }
                else if (line.StartsWith("down"))
                {
                    depth += int.Parse(line.Split(" ")[1]);
                }
            }
            return (depth * hor).ToString();
        }
    }
}