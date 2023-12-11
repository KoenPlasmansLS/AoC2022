namespace AoC2022
{
    public class Solution202320 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution10.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var arr = new char[lines[0].Count(), lines.Count()];
            var nr = new int[lines[0].Count(), lines.Count()];
            var i = 0;
            var beginX = 0;
            var beginY = 0;
            foreach (var line in lines)
            {
                for (var j = 0; j < line.Trim().Length; j++)
                {
                    arr[j, i] = line[j];
                    if (line[j] == 'S')
                    {
                        beginX = j;
                        beginY = i;
                    }
                }
                i++;
            }
            var lst = new List<(int, int)>();
            var previousX = beginX;
            var previousY = beginY;

            var nextX = beginX;
            var nextY = beginY;
            if (arr[nextX, nextY+1] == '|' || arr[nextX, nextY + 1] == 'L' || arr[nextX, nextY + 1] == 'J')
            {
                nextY++;
            }
            else if (arr[nextX, nextY - 1] == '|' || arr[nextX, nextY - 1] == 'F' || arr[nextX, nextY - 1] == '7')
            {
                nextY--;
            }
            else if (arr[nextX+1, nextY] == '-' || arr[nextX+1, nextY] == 'J' || arr[nextX+1, nextY] == '7')
            {
                nextX++;
            }
            lst.Add((nextX, nextY));
            while (nextX != beginX || nextY != beginY)
            {
                if (arr[nextX,nextY] == '|')
                {
                    var temp = nextY;
                    nextY += (nextY - previousY);
                    previousY = temp;
                }
                else if (arr[nextX, nextY] == '-')
                {
                    var temp = nextX;
                    nextX += (nextX - previousX);
                    previousX = temp;
                }
                else if (arr[nextX, nextY] == 'L')
                {
                    if (nextY == previousY)
                    {
                        nextY--;
                        previousX--;
                    }
                    else
                    {
                        nextX++;
                        previousY++;
                    }
                }
                else if (arr[nextX, nextY] == 'J')
                {
                    if (nextY == previousY)
                    {
                        nextY--;
                        previousX++;
                    }
                    else
                    {
                        nextX--;
                        previousY++;
                    }
                }
                else if (arr[nextX, nextY] == '7')
                {
                    if (nextY == previousY)
                    {
                        nextY++;
                        previousX++;
                    }
                    else
                    {
                        nextX--;
                        previousY--;
                    }
                }
                else if (arr[nextX, nextY] == 'F')
                {
                    if (nextY == previousY)
                    {
                        nextY++;
                        previousX--;
                    }
                    else
                    {
                        nextX++;
                        previousY--;
                    }
                }
                else
                {
                    throw new Exception("le");
                }
                lst.Add((nextX, nextY));
            }
            return CountPointsInsideLoop(lst).ToString();
        }

        static bool IsPointInsideLoop(int x, int y, List<(int, int)> loopPath)
        {
            // Use a point-in-polygon algorithm to check if (x, y) is inside the loop
            // Implement a suitable algorithm like Ray-Casting or other methods
            // This implementation assumes a simple check for points inside a convex polygon
            int crossings = 0;
            int n = loopPath.Count;

            for (int i = 0; i < n; i++)
            {
                int x1 = loopPath[i].Item1;
                int y1 = loopPath[i].Item2;
                int x2 = loopPath[(i + 1) % n].Item1;
                int y2 = loopPath[(i + 1) % n].Item2;

                if (((y1 <= y && y < y2) || (y2 <= y && y < y1)) &&
                    x < (x2 - x1) * (y - y1) / (y2 - y1) + x1)
                {
                    crossings++;
                }
            }

            return crossings % 2 == 1;
        }

        static int CountPointsInsideLoop(List<(int, int)> loopPath)
        {
            int minX = loopPath.Min(point => point.Item1);
            int maxX = loopPath.Max(point => point.Item1);
            int minY = loopPath.Min(point => point.Item2);
            int maxY = loopPath.Max(point => point.Item2);

            int count = 0;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (!loopPath.Contains((x, y)) && IsPointInsideLoop(x, y, loopPath))
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}