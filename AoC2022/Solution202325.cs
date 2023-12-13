namespace AoC2022
{
    public class Solution202325 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution13.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var i = 0;
            long sum = 0;
            while (i < lines.Length)
            {
                var begin = i;
                while (i < lines.Length && !string.IsNullOrWhiteSpace(lines[i]))
                {
                    i++;
                }
                var end = i;

                var arr = new char[lines[begin].Trim().Length, end - begin];
                var cols = new List<long>();
                for (var t = begin; t < end; t++)
                {
                    var line = lines[t];
                    long col = 0;
                    for (var c = 0; c < line.Trim().Length; c++)
                    {
                        arr[c, t - begin] = line[c];
                        col *= 2;
                        if (arr[c, t - begin] == '#')
                        {
                            col++;
                        }
                    }
                    cols.Add(col);
                }
                var rows = new List<long>();
                for (var c = 0; c < lines[begin].Trim().Length; c++)
                {
                    long row = 0;
                    for (var t = begin; t < end; t++)
                    {
                        row *= 2;
                        if (arr[c, t - begin] == '#')
                        {
                            row++;
                        }
                    }
                    rows.Add(row);
                }
                for(var d = 0; d < cols.Count - 1; d++) 
                {
                    var e = 0;
                    var ok = true;
                    while(d+e+1 < cols.Count && d-e >= 0)
                    {
                        if (cols[d + e + 1] != cols[d - e])
                        {
                            ok = false;
                            break;
                        }
                        e++;
                    }
                    if (ok)
                    {
                        sum += 100 * (d + 1);
                    }
                }
                for (var d = 0; d < rows.Count - 1; d++)
                {
                    var e = 0;
                    var ok = true;
                    while (d + e + 1 < rows.Count && d - e >= 0)
                    {
                        if (rows[d + e + 1] != rows[d - e])
                        {
                            ok = false;
                            break;
                        }
                        e++;
                    }
                    if (ok)
                    {
                        sum += (d + 1);
                    }
                }
                i++;
            }
            return sum.ToString();
        }
    }
}