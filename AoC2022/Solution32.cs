namespace AoC2022
{
    public class Solution32 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution16.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            foreach (var line in lines)
            {
                var trimLine = line.Trim();
                var bits = new bool[4 * trimLine.Length];
                for (var i = 0; i < trimLine.Length; i++)
                {
                    var nr = Solution31.GetHexVal(trimLine[i]);
                    if (nr >> 3 == 1) bits[4 * i] = true;
                    if ((nr % 8) >> 2 == 1) bits[4 * i + 1] = true;
                    if ((nr % 4) >> 1 == 1) bits[4 * i + 2] = true;
                    if ((nr % 2) == 1) bits[4 * i + 3] = true;
                }
                var pack = Solution31.Parse(bits);
                sum += pack.GetValue();
            }
            return sum.ToString();
        }
    }
}