namespace AoC2022
{
    public class Solution20221 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution1.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var max = 0;
            var sum = 0;
            for (var i = 1; i < lines.Count(); i++)
            {
                if (string.IsNullOrEmpty(lines[i].Trim()))
                {
                    var current = sum;
                    if (current > max)
                    {
                        max = current;
                    }
                    sum = 0;
                } 
                else 
                {
                    sum += int.Parse(lines[i]);
                }
            }
            return max.ToString();
        }
    }
}