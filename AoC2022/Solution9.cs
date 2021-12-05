namespace AoC2022
{
    public class Solution9 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/solution5.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines, Action<int, int ,int, int, int[,]>? extra = null)
        {
            var matrix = new int[1000,1000];
            foreach(var line in lines)
            {
                var split = line.Split("->");
                var x1 = int.Parse(split[0].Split(",")[0]);
                var y1 = int.Parse(split[0].Split(",")[1]);
                var x2 = int.Parse(split[1].Split(",")[0]);
                var y2 = int.Parse(split[1].Split(",")[1]);
                if (x1 == x2)
                {
                    for(var l = Math.Min(y1, y2); l<=Math.Max(y1, y2); l++)
                    {
                        matrix[x1, l]++;
                    }
                }
                else if (y1 == y2)
                {
                    for (var l = Math.Min(x1, x2); l <= Math.Max(x1, x2); l++)
                    {
                        matrix[l, y1]++;
                    }
                }
                else
                {
                    extra?.Invoke(x1, y1, x2, y2, matrix);
                }
            }
            var sum = 0;
            for(var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (matrix[i, j] >= 2)
                    {
                        sum++;
                    }
                }
            }
            return sum.ToString();
        }
    }
}