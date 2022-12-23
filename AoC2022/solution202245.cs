namespace AoC2022
{
    public class Solution202245 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution23.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var map = new bool[20 + lines[0].Length, 20 + lines.Length];
            var offset = 10;
            var j = 0;
            foreach(var line in lines)
            {
                for(var i = 0; i< lines[0].Trim().Length;i++)
                {
                    if (line[i] == '#')
                    {
                        map[offset + i, offset + j] = true;
                    }
                }
                j++;
            }

            var arr = new char[4] { 'N', 'S', 'W', 'E' };
            var arrI = 0;
            for (var step = 0; step < 10; step++)
            {
                var lstMoves = new List<(int, int, int, int)>();
                for (var k = 0; k < map.GetLength(0); k++)
                {
                    for (var l = 0; l < map.GetLength(1); l++)
                    {
                        if (map[k, l])
                        {
                            if (!map[k + 1, l] && !map[k - 1, l] && !map[k, l + 1] && !map[k, l - 1]
                                 && !map[k + 1, l + 1] && !map[k - 1, l + 1] && !map[k + 1, l - 1] && !map[k - 1, l - 1]) continue;


                            (int newk, int newl) = Try(map, arr, arrI, k, l);
                            lstMoves.Add((k, l, newk, newl));
                        }
                    }
                }
                foreach (var move in lstMoves)
                {
                    if (!lstMoves.Any(x => x != move && x.Item3 == move.Item3 && x.Item4 == move.Item4))
                    {
                        map[move.Item1, move.Item2] = false;
                        map[move.Item3, move.Item4] = true;
                    }
                }
                arrI = (arrI + 1) % 4;

                PrintMap(map);
            }
            (int minX, int maxX, int minY, int maxY) = PrintMap(map);
            var sum = 0;
            var sumpl = 0;
            for (var k = minX; k <= maxX; k++)
            {
                for (var l = minY; l <= maxY; l++)
                {
                    if (!map[k, l]) sum++;
                    else
                    {
                        sumpl++;
                    }
                }
            }

            return sum.ToString() + " " + ((maxX-minX + 1) * (maxY - minY + 1) - sumpl).ToString();
        }

        private static (int,int,int,int) PrintMap(bool[,] map)
        {
            var minX = int.MaxValue;
            var maxX = 0;
            var minY = int.MaxValue;
            var maxY = 0;
            for (var k = 0; k < map.GetLength(1); k++)
            {
                for (var l = 0; l < map.GetLength(0); l++)
                {
                    if (!map[l, k])
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        if (l < minX) minX = l;
                        if (l > maxX) maxX = l;
                        if (k < minY) minY =k;
                        if (k > maxY) maxY = k;
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
            return (minX, maxX, minY, maxY);
        }

        private static (int, int) Try(bool[,] map, char[] arr, int arrI, int k , int l)
        {
            
            for (var offset = 0; offset < 4; offset++)
            {
                var dir = arr[(arrI + offset) % 4];
                if (dir == 'N')
                {
                    if (!map[k - 1, l - 1] && !map[k, l - 1] && !map[k + 1, l - 1]) return (k, l - 1);
                }
                if (dir == 'S')
                {
                    if (!map[k - 1, l + 1] && !map[k, l + 1] && !map[k + 1, l + 1]) return (k, l + 1);
                }
                if (dir == 'E')
                {
                    if (!map[k + 1, l - 1] && !map[k + 1, l] && !map[k + 1, l + 1]) return (k + 1, l);
                }
                if (dir == 'W')
                {
                    if (!map[k - 1, l - 1] && !map[k - 1, l] && !map[k - 1, l + 1]) return (k - 1, l);
                }
            }
            return (k, l);
        }
    }
}