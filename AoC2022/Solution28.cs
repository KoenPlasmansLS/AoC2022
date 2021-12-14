namespace AoC2022
{
    public class Solution28 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution14.txt").OpenText().ReadToEnd().Split("\n");
            return Solution27.BaseAlgorithm(lines, 40);
        }
    }
}