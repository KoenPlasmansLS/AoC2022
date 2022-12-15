namespace AoC2022
{
    public class Solution202229 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution15.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var row = 2000000;
            var arr = new bool[7000000];
            var b = new bool[7000000];
            var offset = 2000000;
            foreach (var line in lines)
            {
                var splt = line.Trim().Split(' ');
                var x = int.Parse(splt[2].Split("=")[1].Replace(",", ""));
                var y = int.Parse(splt[3].Split("=")[1].Replace(":", ""));
                var bx = int.Parse(splt[8].Split("=")[1].Replace(",", ""));
                var by = int.Parse(splt[9].Split("=")[1]);
                if (by == row)
                {
                    b[bx + offset] = true;
                }
                var manhattan = Math.Abs(x - bx) + Math.Abs(y - by);
                var distY = Math.Abs(y - row);
                var overlap = manhattan - distY;
                if (overlap >= 0)
                {
                    for(var t = x - overlap; t <= x + overlap; t++)
                    {
                        arr[t + offset] = true;
                    }
                }
            }
            var sum = 0;
            for(var k = 0; k< arr.Length; k++)
            {
                if (arr[k] && !b[k]) sum++;
            }
            return sum.ToString();
        }
    }
}