using System.Globalization;

namespace AoC2022
{
    public class Solution202215 : IProvideSolution
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
            var bln = new bool[maxj, maxi];

            var i = 0;
            var sum = 0;
            foreach (var line in lines)
            {
                var j = 0;
                foreach(var ch in line.Trim())
                {
                    mat[j, i] = int.Parse(ch.ToString());
                    bln[j, i] = i == 0 || j == 0 || i == maxi - 1 || j == maxj - 1;
                    if (bln[j, i])
                    {
                        sum++;
                    }
                    j++;
                }
                i++;
            }
            for (var l = 1; l < maxj - 1; l++)
            {
                for (var m = 1; m < maxi - 1; m++)
                {
                    var current = mat[l, m];
                    var maxLeft = FindMax(mat, 0, l - 1, m, m);
                    var maxRight = FindMax(mat, l + 1, maxj -1, m, m);
                    var maxTop = FindMax(mat, l, l, 0, m - 1);
                    var maxBottom = FindMax(mat, l, l, m + 1, maxi - 1);
                    if (current > maxLeft || current > maxRight ||current > maxTop || current > maxBottom)
                    {
                        bln[l, m] = true;
                        sum++;
                    }
                }
            }
            return sum.ToString();
        }

        public static int FindMax(int[,] mat, int xStart, int xEnd, int yStart, int yEnd)
        {
            var max = 0;
            for(var t = xStart; t <= xEnd; t++)
            {
                for (var u = yStart; u <= yEnd; u++)
                {
                    if(max < mat[t, u])
                    {
                        max = mat[t, u];
                    }
                }
            }
            return max;
        }
    }
}