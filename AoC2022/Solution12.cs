namespace AoC2022
{
    public class Solution12 : Solution11
    {
        public override string GetSolution()
        {
            var lines = new FileInfo("Input/solution6.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines, 256);
        }
    }
}