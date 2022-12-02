namespace AoC2022
{
    public class Solution20224 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution2.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;

            for (var i = 0; i < lines.Count(); i++)
            {
                var splt = lines[i].Trim().Split(" ");
                var ynr = splt[1] == "X" ? 0 : splt[1] == "Y" ? 3 : 6;
                sum += ynr;
                var onr = splt[0] == "A" ? 1 : splt[0] == "B" ? 2 : 3;
                if(ynr == 0)
                {
                    sum += onr == 1 ? 3 : onr == 2 ? 1 : 2;
                }
                else if (ynr == 3)
                {
                    sum += onr;
                }
                else
                {
                    sum += onr == 1 ? 2 : onr == 2 ? 3 : 1;
                }
            }
            return sum.ToString();
        }
    }
}