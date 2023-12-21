using System.Data;

namespace AoC2022
{
    public class Solution202341 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution21.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var arr = new bool[lines[0].Trim().Length, lines.Count()];
            var startX = 0;
            var startY = 0;
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[i].Trim().Length; j++)
                {
                    if (lines[i][j]=='S')
                    {
                        startX = j;
                        startY = i;
                    }
                    if (lines[i][j] == '#')
                    {
                        arr[j, i] = true;
                    }
                }
            }
            var boundary = new List<(int, int)>();
            var total = 1;
            var visited = new bool[lines[0].Trim().Length, lines.Count()];
            boundary.Add((startX, startY));
            visited[startX, startY] = true;
            for(var i = 0; i< 64; i++)
            {
                var lstNewBoundary = new List<(int, int)>();
                foreach((int x, int y) in boundary)
                {
                    if (x > 0 && !arr[x-1, y] && !visited[x - 1,y])
                    {
                        lstNewBoundary.Add((x - 1, y));
                        visited[x - 1, y] = true;
                        if (i % 2 == 1)
                        {
                            total++;
                        }
                    }
                    if (x < arr.GetLength(0)-1 && !arr[x + 1, y] && !visited[x + 1, y])
                    {
                        lstNewBoundary.Add((x + 1, y));
                        visited[x + 1, y] = true;
                        if (i % 2 == 1)
                        {
                            total++;
                        }
                    }
                    if (y > 0 && !arr[x, y - 1] && !visited[x, y - 1])
                    {
                        lstNewBoundary.Add((x, y - 1));
                        visited[x, y - 1] = true;
                        if (i % 2 == 1)
                        {
                            total++;
                        }
                    }
                    if (y < arr.GetLength(1) - 1 && !arr[x, y + 1] && !visited[x, y + 1])
                    {
                        lstNewBoundary.Add((x, y + 1));
                        visited[x, y + 1] = true;
                        if (i % 2 == 1)
                        {
                            total++;
                        }
                    }
                }
                boundary = lstNewBoundary;
            }
            return total.ToString();
        }
    }
}