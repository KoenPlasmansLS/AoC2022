namespace AoC2022
{
    public class Solution26 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution13.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private static string BaseAlgorithm(string[] lines)
        {
            var lst = new List<(int, int)>();
            foreach (var line in lines)
            {
                if (line.Trim() == string.Empty) break;

                lst.Add((int.Parse(line.Split(",")[0]), int.Parse(line.Split(",")[1].Trim())));
            }
            var arr = new bool[lst.Max(x => x.Item1) + 1, lst.Max(x => x.Item2) + 1];
            foreach (var item in lst)
            {
                arr[item.Item1, item.Item2] = true;
            }
            foreach (var firstFold in lines.Where(x => x.Contains("fold along")))
            {
                var yIndex = firstFold.IndexOf("y=");
                if (yIndex >= 0)
                {
                    var rest = int.Parse(firstFold.Substring(yIndex + 2));
                    arr = FoldY(rest, arr);
                }
                var xIndex = firstFold.IndexOf("x=");
                if (xIndex >= 0)
                {
                    var rest = int.Parse(firstFold.Substring(xIndex + 2));
                    arr = FoldX(rest, arr);
                }
            }
            for (var k = 0; k < 10; k++)
            {
                for (var l = 0; l < 100; l++)
                {
                    Console.Write(arr[l, k] ? "#" : " ");
                }
                Console.WriteLine();
            }
            return "HECRZKPR";
        }

        private static bool[,] FoldY(int fold, bool[,] arr)
        {
            for (var k = fold; k < arr.GetLength(1); k++)
            {
                for (var l = 0; l < arr.GetLength(0); l++)
                {
                    if (arr[l, k])
                    {
                        arr[l, k] = false;
                        var newLine = 2 * fold - k;
                        if (newLine >= 0)
                        {
                            arr[l, newLine] = true;
                        }
                    }
                }
            }
            return arr;
        }

        private static bool[,] FoldX(int fold, bool[,] arr)
        {
            for (var k = fold; k < arr.GetLength(0); k++)
            {
                for (var l = 0; l < arr.GetLength(1); l++)
                {
                    if (arr[k,l])
                    {
                        arr[k, l] = false;
                        var newLine = 2 * fold - k;
                        if (newLine >= 0)
                        {
                            arr[newLine, l] = true;
                        }
                    }
                }
            }
            return arr;
        }
    }
}