namespace AoC2022
{
    public class Solution202216 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution8.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var maxi = lines.Length;
            var maxj = lines[0].Trim().Length;
            var mat = new int[maxj, maxi];

            var i = 0;
            foreach (var line in lines)
            {
                var j = 0;
                foreach(var ch in line.Trim())
                {
                    mat[j, i] = int.Parse(ch.ToString());
                    j++;
                }
                i++;
            }
            var maxScn = 0;
            for (var l = 1; l < maxj - 1; l++)
            {
                for (var m = 1; m < maxi - 1; m++)
                {
                    var current = mat[l, m];
                    var maxLeft = FindMaxNeg(mat, current, l - 1, 0, m, m);
                    var maxRight = FindMaxPos(mat, current, l + 1, maxj -1, m, m);
                    var maxTop = FindMaxNeg(mat, current, l, l, m - 1, 0);
                    var maxBottom = FindMaxPos(mat, current, l, l, m + 1, maxi - 1);
                    var scnCurrent = maxLeft * maxRight * maxTop * maxBottom;
                    if (scnCurrent > maxScn)
                    {
                        maxScn = scnCurrent;
                    }
                }
            }
            return maxScn.ToString();
        }

        public static int FindMaxPos(int[,] mat, int current, int xStart, int xEnd, int yStart, int yEnd)
        {
            var sum = 0;
            for(var t = xStart; t <= xEnd; t++)
            {
                for (var u = yStart; u <= yEnd; u++)
                {
                    if(mat[t, u] < current)
                    {
                        sum++;
                    }
                    else
                    {
                        sum++;
                        return sum;
                    }
                }
            }
            return sum;
        }

        public static int FindMaxNeg(int[,] mat, int current, int xStart, int xEnd, int yStart, int yEnd)
        {
            var sum = 0;
            for (var t = xStart; t >= xEnd; t--)
            {
                for (var u = yStart; u >= yEnd; u--)
                {
                    if (mat[t, u] < current)
                    {
                        sum++;
                    }
                    else
                    {
                        sum++;
                        return sum;
                    }
                }
            }
            return sum;
        }
    }
}