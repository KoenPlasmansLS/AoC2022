namespace AoC2022
{
    public class Solution202321 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution11.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var arr = new bool[lines[0].Trim().Count(), lines.Count()];
            var rows = new int[lines.Length];
            var cols = new int[lines[0].Trim().Count()];
            var pos = new List<(int, int)>();
            var i = 0;
            foreach (var line in lines)
            {
                for (var j = 0; j < line.Trim().Length; j++)
                {
                    if (line[j] == '#')
                    {
                        arr[j, i] = true;
                        pos.Add((j, i));
                    }
                }
                i++;
            }
            //row
            for(var k = 0; k < arr.GetLength(0);k++)
            {
                var found = false;
                for (var l = 0; l < arr.GetLength(1); l++)
                {
                    if (arr[k, l])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    cols[k]++;
                }
            }
            //col
            for (var k = 0; k < arr.GetLength(1); k++)
            {
                var found = false;
                for (var l = 0; l < arr.GetLength(0); l++)
                {
                    if (arr[l, k])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    rows[k]++;
                }
            }
            long sum = 0;
            for(var a = 0; a < pos.Count() - 1; a++)
            {
                var p = pos[a];
                for(var b = a + 1; b < pos.Count(); b++)
                {
                    var otherp = pos[b];
                    long diff = Math.Abs(p.Item1 - otherp.Item1);
                    diff += Math.Abs(p.Item2 - otherp.Item2);
                    if (p.Item1 > otherp.Item1)
                    {
                        for (var t = p.Item1 - 1; t > otherp.Item1; t--)
                        {
                            diff += cols[t];
                        }
                    }
                    else
                    {
                        for (var t = p.Item1 + 1; t < otherp.Item1; t++)
                        {
                            diff += cols[t];
                        }
                    }


                    if (p.Item2 > otherp.Item2)
                    {
                        for (var t = p.Item2 - 1; t > otherp.Item2; t--)
                        {
                            diff += rows[t];
                        }
                    }
                    else
                    {
                        for (var t = p.Item2 + 1; t < otherp.Item2; t++)
                        {
                            diff += rows[t];
                        }
                    }
                    sum += diff;
                }
            }
            return sum.ToString();
        }
    }
}