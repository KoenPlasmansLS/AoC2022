namespace AoC2022
{
    public class Solution22 : Solution21
    {
        public override string GetSolution()
        {
            var lines = new FileInfo("Input/solution11.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected override string BaseAlgorithm(string[] lines)
        {
            var arr = GetData(lines);
            var step = 0;
            var flash = 0;
            while(flash != 100)
            {
                (arr, flash) = ApplyStep(arr);
                step += 1;
            }
            return step.ToString();
        }
    }
}