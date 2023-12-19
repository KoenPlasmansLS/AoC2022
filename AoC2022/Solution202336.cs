using System.Collections.Generic;
using System.ComponentModel;

namespace AoC2022
{
    public class Solution202336 : IProvideSolution
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

            long sum = 1;
            var lst = new List<(int, int)>();
            foreach(var line in lines)
            {
                var dir = line.Trim().Split(' ')[2].Substring(7,1);
                if (dir == "0")
                {
                    dir = "R";
                }
                if (dir == "1")
                {
                    dir = "D";
                }
                if (dir == "2")
                {
                    dir = "L";
                }
                if (dir == "3")
                {
                    dir = "U";
                }
                var length = Convert.ToInt32(line.Split(' ')[2].Substring(2, 5), 16);
                if (dir == "R")
                {
                    x += length;
                    sum += length;
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
                    sum += length;
                }
                lst.Add((x, y));
            }
            var areas = new List<Area>();
            var xs = lst.Select(x => x.Item1).OrderBy(x => x).Distinct().ToList();
            var ys = lst.Select(x => x.Item2).OrderBy(x => x).Distinct().ToList();
            for(var l = 0; l < xs.Count() - 1; l++)
            {
                for (var m = 0; m < ys.Count() - 1; m++)
                {
                    areas.Add(new Area { X0 = xs[l]+1, Y0 = ys[m]+1, X1 = xs[l + 1], Y1 = ys[m + 1] });
                }
            }

            foreach(var area in areas) 
            { 
                if(IsPointInsideLoop(area.X0 + 1, area.Y0 + 1, lst))
                {
                    sum+=area.Surface;
                }
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
                long x1 = loopPath[i].Item1;
                long y1 = loopPath[i].Item2;
                long x2 = loopPath[(i + 1) % n].Item1;
                long y2 = loopPath[(i + 1) % n].Item2;

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

        private class Area
        {
            public int X0 { get; set; }
            public int Y0 { get; set; }
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public long Surface => ((long) (X1-X0 + 1)) * (long) ((Y1-Y0+1));
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