namespace AoC2022
{
    public class Solution8 : Solution7
    {
        public override string GetSolution()
        {
            var lines = new FileInfo("Input/solution4.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected override bool IsBingo(int i, int maxLength, List<int> ignore)
        {
            ignore.Add(i);
            if (ignore.Count == maxLength)
            {
                return true;
            }
            return false;
        }
    }
}