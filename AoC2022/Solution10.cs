namespace AoC2022
{
    public class Solution10 : Solution9
    {
        public override string GetSolution()
        {
            var lines = new FileInfo("Input/solution5.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines, Extra);
        }

        private void Extra(int x1, int y1, int x2, int y2, int[,] matrix)
        {
            for (var l = 0; l <= Math.Abs(x1 - x2); l++)
            {
                if (x1 < x2 && y1 < y2)
                {
                    matrix[x1 + l, y1 + l]++;
                }
                else if (x1 < x2 && y1 > y2)
                {
                    matrix[x1 + l, y1 - l]++;
                }
                else if (x1 > x2 && y1 > y2)
                {
                    matrix[x1 - l, y1 - l]++;
                }
                else if (x1 > x2 && y1 < y2)
                {
                    matrix[x1 - l, y1 + l]++;
                }
            }
        }
    }
}