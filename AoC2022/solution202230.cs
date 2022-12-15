namespace AoC2022
{
    public class Solution202230 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution15.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var arr = new List<(int, int, int)>();
            foreach (var line in lines)
            {
                var splt = line.Trim().Split(' ');
                var x = int.Parse(splt[2].Split("=")[1].Replace(",", ""));
                var y = int.Parse(splt[3].Split("=")[1].Replace(":", ""));
                var bx = int.Parse(splt[8].Split("=")[1].Replace(",", ""));
                var by = int.Parse(splt[9].Split("=")[1]);
                var manhattan = Math.Abs(x - bx) + Math.Abs(y - by);
                arr.Add((x, y, manhattan));
            }
            long sum = 0;
            var foundNow = false;
            foreach (var (x, y, r) in arr)
            {
                foreach (var (px, py) in GetBoundary(x, y, r + 1))
                {
                    if (0 <= px && px <= 4000000 && 0 <= py && py <= 4000000)
                    {
                        var found = false;
                        foreach (var (dx, dy, dr) in arr)
                        {
                            if ((Math.Abs(px - dx) + Math.Abs(py - dy)) <= dr)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            foundNow = true;
                            sum = 4000000 * px + py;
                        }
                    }
                    if (foundNow)
                    {
                        break;
                    }
                }
                if (foundNow)
                {
                    break;
                }
            }
            return sum.ToString();
        }

        private static IEnumerable<(long, long)> GetBoundary(int x, int y, int r)
        {
            (long px, long py) = (x, y + r);
            while (px != x + r && py != y)
            {
                px += 1;
                py -= 1;
                yield return (px, py);
            }
            while (px != x && py != y - r)
            {
                px -= 1;
                py -= 1;
                yield return (px, py);
            }
            while (px != x - r && py != y)
            {
                px -= 1;
                py += 1;
                yield return (px, py);
            }
            while (px != x && py != y + r)
            {
                px += 1;
                py += 1;
                yield return (px, py);
            }
        }
    }
}