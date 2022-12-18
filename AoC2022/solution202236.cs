namespace AoC2022
{
    public class Solution202236 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution18.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var min = 100;
            var max = 0;
            var cubes = new List<int[]>();
            foreach (var line in lines)
            {
                var tmp = new int[3];
                var sn = line.Split(',');
                for (int i = 0; i < 3; i++)
                {
                    tmp[i] = int.Parse(sn[i]);
                    max = Math.Max(max, tmp[i]);
                    min = Math.Min(min, tmp[i]);
                }
                cubes.Add(tmp);
            }
            min--; max++;

            var blocks = new HashSet<int>();
            foreach (var cube in cubes) blocks.Add(cube[0] * 10000 + cube[1] * 100 + cube[2]);
            var steam = new List<(int, int, int)> { (min, min, min), (min, min, max), (min, max, min), (min, max, max),
                                                    (max, min, min), (max, min, max), (max, max, min), (max, max, max) };
            var dirs = new List<(int, int, int)> { (-1, 0, 0), (1, 0, 0), (0, -1, 0), (0, 1, 0), (0, 0, -1), (0, 0, 1) };
            var visited = new HashSet<int> { 10000 * min + 100 * min + min };
            var sides = new HashSet<int>();
            while (steam.Count > 0)
            {
                var more = new List<(int, int, int)>(steam.Count);
                foreach (var (x, y, z) in steam)
                {
                    foreach (var (dx, dy, dz) in dirs)
                    {
                        if (x + dx < min || y + dy < min || z + dz < min) continue;
                        if (x + dx > max || y + dy > max || z + dz > max) continue;
                        int code = (x + dx) * 10000 + (y + dy) * 100 + (z + dz);
                        if (visited.Contains(code)) continue;
                        if (blocks.Contains(code))
                        {
                            sides.Add((x * 2 + dx) * 10000 + (y * 2 + dy) * 100 + (z * 2 + dz));
                            continue;
                        }
                        visited.Add(code);
                        more.Add((x + dx, y + dy, z + dz));
                    }
                }
                steam = more;
            }
            return sides.Count.ToString();
        }
    }
}