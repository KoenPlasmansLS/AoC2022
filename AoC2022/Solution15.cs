namespace AoC2022
{
    public class Solution15 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution8.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            int sum = 0;
            foreach(var line in lines)
            {
                var second = line.Trim().Split("|")[1].Trim();
                var numbers = second.Split(" ").ToList();
                sum += numbers.Count(x => x.Length < 5 || x.Length == 7);
            }
            return sum.ToString();
        }
    }
}