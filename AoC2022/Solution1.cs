namespace AoC2022
{
    public class Solution1 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/solution1.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines, int windowSize = 1)
        { 
            var count = 0;
            var first = true;
            var max = 0;
            for (var i = windowSize-1; i < lines.Count(); i++)
            {
                if (first)
                {
                    first = false;
                    max = int.Parse(lines[0]);
                }
                else
                {
                    var current = int.Parse(lines[i]);
                    if (current > max)
                    {
                        count++;
                    }
                    max = int.Parse(lines[i - (windowSize - 1)]);
                }
            }
            return count.ToString();
        }
    }
}