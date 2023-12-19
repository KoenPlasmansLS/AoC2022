using System.Collections.Generic;
using System.ComponentModel;

namespace AoC2022
{
    public class Solution202335 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution18.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var x = 0;
            var y = 0;
            var lst = new List<(int, int)>();
            foreach(var line in lines)
            {
                var dir = line.Split(' ')[0];
                var length = int.Parse(line.Split(' ')[1]);
                if (dir == "R")
                {
                    x += length;
                }
                if (dir == "L")
                {
                    x -= length;
                }
                if (dir == "U")
                {
                    y -= length;
                }
                if (dir == "D")
                {
                    y += length;
                }
                lst.Add((x, y));
            }

            var sum = 0;
            for (var j = lst.Min(x => x.Item2); j <= lst.Max(x => x.Item2); j++)
            {
                for (var i = lst.Min(x => x.Item1); i <= lst.Max(x => x.Item1);i++)
                {
                    if(IsPointInsideLoop(i,j, lst))
                    {
                        Console.Write('#');
                        sum++;
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            return sum.ToString();
        }

        static bool IsPointInsideLoop(int x, int y, List<(int, int)> loopPath)
        {
            // Use a point-in-polygon algorithm to check if (x, y) is inside the loop
            // Implement a suitable algorithm like Ray-Casting or other methods
            // This implementation assumes a simple check for points inside a convex polygon
            int n = loopPath.Count;
            bool inside = false;
            for (int i = 0; i < n; i++)
            {
                int x1 = loopPath[i].Item1;
                int y1 = loopPath[i].Item2;
                int x2 = loopPath[(i + 1) % n].Item1;
                int y2 = loopPath[(i + 1) % n].Item2;

                bool intersect = ((y1 > y) != (y2 > y)) && (x < (x2 - x1) * (y - y1) / (y2 - y1) + x1);

                if (intersect)
                    inside = !inside;

                // Include points on the path itself
                if ((y1 == y && y2 == y) && ((x1 <= x && x <= x2) || (x2 <= x && x <= x1)))
                    return true;

                if ((x1 == x && x2 == x) && ((y1 <= y && y <= y2) || (y2 <= y && y <= y1)))
                    return true;
            }

            return inside;
        }

        private enum Dir
        {
            Left,
            Right,
            Down,
            Up
        }
    }
}