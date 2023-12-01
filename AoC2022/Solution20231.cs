namespace AoC2022
{
    public class Solution20231 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution1.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                for(var j = 0; j < lines[i].Length; j++)
                {
                    if (int.TryParse(lines[i][j].ToString(), out int d))
                    {
                        sum += d * 10;
                        break;
                    }
                }
                for (var j = lines[i].Length - 1; j >= 0; j--)
                {
                    if (int.TryParse(lines[i][j].ToString(), out int d))
                    {
                        sum += d;
                        break;
                    }
                }
            }
            return sum.ToString();
        }
    }
}