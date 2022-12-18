namespace AoC2022
{
    public class Solution202235 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution18.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sides = 0;
            var mat = new bool[80, 80, 80];
            foreach (var line in lines)
            {
                var splt = line.Trim().Split(",");
                var x = int.Parse(splt[0]);
                var y = int.Parse(splt[1]);
                var z = int.Parse(splt[2]);
                mat[x, y, z] = true;
                sides += 6;
                if (mat[x + 1, y, z])
                {
                    sides-=2;
                }
                if (x > 0 && mat[x - 1, y, z])
                {
                    sides-=2;
                }
                if (mat[x, y + 1, z])
                {
                    sides-=2;
                }
                if (y > 0 && mat[x, y - 1, z])
                {
                    sides-=2;
                }
                if (mat[x, y, z + 1])
                {
                    sides-=2;
                }
                if (z > 0 && mat[x, y, z - 1])
                {
                    sides-=2;
                }
            }
            return sides.ToString();
        }
    }
}