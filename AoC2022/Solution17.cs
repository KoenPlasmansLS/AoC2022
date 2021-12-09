namespace AoC2022
{
    public class Solution17 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution9.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            int y = lines.Count();
            int x = lines[0].Length - 1;
            var arr = new int[x, y];
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    arr[i, j] = int.Parse(lines[i][j].ToString());
                }
            }
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var current = arr[i, j];
                    if ((i == 0 || arr[i - 1, j] > current) &&
                        (j == 0 || arr[i, j - 1] > current) &&
                        (i == x - 1 || arr[i + 1, j] > current) &&
                        (j == y - 1 || arr[i, j + 1] > current))
                    {
                        sum += current + 1;
                    }
                }
            }
            return sum.ToString();
        }
    }
}