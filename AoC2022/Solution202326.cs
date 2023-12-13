namespace AoC2022
{
    public class Solution202326 : IProvideSolution
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
            var index = 1;
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
                //int? col = null;
                //int? row = null;
                //for(var d = 0; d < cols.Count - 1; d++) 
                //{
                //    var e = 0;
                //    var ok = true;
                //    while(d+e+1 < cols.Count && d-e >= 0)
                //    {
                //        if (cols[d + e + 1] != cols[d - e])
                //        {
                //            ok = false;
                //            break;
                //        }
                //        e++;
                //    }
                //    if (ok)
                //    {
                //        col = (d + 1);
                //    }
                //}
                for (var d = 0; d < rows.Count - 1; d++)
                {
                    var e = 0;
                    if (index == 46 && d==11)
                    {
                        var t = "";
                    }
                    var ok = true;
                    var smudge = false;
                    while (d + e + 1 < rows.Count && d - e >= 0)
                    {
                        if (rows[d + e + 1] != rows[d - e] && !smudge && DiffC(arr, d+e+1, d - e) == 1)
                        {
                            smudge = true;
                        }
                        else if (rows[d + e + 1] != rows[d - e])
                        {
                            ok = false;
                            break;
                        }
                        e++;
                    }
                    if (ok && smudge)
                    {
                        Console.WriteLine(index + ":" + (d + 1));
                        sum += (d + 1);
                    }
                }


                for (var d = 0; d < cols.Count - 1; d++)
                {
                    var e = 0;
                    var ok = true;
                    var smudge = false;
                    while (d + e + 1 < cols.Count && d - e >= 0)
                    {
                        if (cols[d + e + 1] != cols[d - e] && !smudge && Diff(arr, d+e+1, d-e)==1)
                        {
                            smudge = true;
                        }
                        else if (cols[d + e + 1] != cols[d - e])
                        {
                            ok = false;
                            break;
                        }
                        e++;
                    }
                    if (ok && smudge)
                    {
                        Console.WriteLine(index + ":" + (d+1)*100);
                        sum += 100 * (d + 1);
                    }
                }
                i++;
                index++;
            }
            return sum.ToString();
        }

        private static int Diff(char[,] arr, int row, int row2)
        {
            var diff = 0;
            for (var k = 0; k < arr.GetLength(0); k++)
            {
                if (arr[k, row] != arr[k, row2])
                {
                    diff++;
                }
            }
            return diff;
        }

        private static int DiffC(char[,] arr, int row, int row2)
        {
            var diff = 0;
            for (var k = 0; k < arr.GetLength(1); k++)
            {
                if (arr[row, k] != arr[row2, k])
                {
                    diff++;
                }
            }
            return diff;
        }

        private static bool IsPowerOf2(int x)
        {
            while (x > 1)
            {
                if (x % 2 != 0)
                {
                    return false;
                }
                x /= 2;
            }
            return true;
        }
    }
}