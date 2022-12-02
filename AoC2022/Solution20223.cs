namespace AoC2022
{
    public class Solution20223 : IProvideSolution
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
                var ynr = splt[1] == "X" ? 1 : splt[1] == "Y" ? 2 : 3;
                sum += ynr;
                var onr = splt[0] == "A" ? 1 : splt[0] == "B" ? 2 : 3;
                sum += onr == ynr ? 3 : ((onr == 1 && ynr == 2) || (onr == 2 && ynr == 3) || (onr == 3 && ynr == 1)) ? 6 : 0;
            }
            return sum.ToString();
        }
    }
}