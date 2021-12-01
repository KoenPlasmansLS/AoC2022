namespace AoC2022
{
    public class Solution2 : Solution1
    {
        public override string GetSolution()
        {
            var lines = new FileInfo("Input/solution1.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines, 3);
        }
    }
}