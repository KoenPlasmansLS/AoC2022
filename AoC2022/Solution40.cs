namespace AoC2022
{
    public class Solution40 : Solution39
    {
        public override string GetSolution()
        {
            var lines = new FileInfo("Input/solution20.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines, 50);
        }
    }
}