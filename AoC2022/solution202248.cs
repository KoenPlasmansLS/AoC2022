namespace AoC2022
{
    public class Solution202248 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution24.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var map = new bool[lines[0].Trim().Length, lines.Count()];
            var blizz = new List<(int, int, int)>();
            var j = 0;
            var step = 1;
            foreach(var line in lines)
            {
                for(var i = 0; i< lines[0].Trim().Length;i++)
                {
                    if (line[i] == '#')
                    {
                        map[i, j] = true;
                    }
                    if (line[i] == '>')
                    {
                        blizz.Add((i, j, 0));
                    }
                    if (line[i] == '<')
                    {
                        blizz.Add((i, j, 2));
                    }
                    if (line[i] == 'v')
                    {
                        blizz.Add((i, j, 1));
                    }
                    if (line[i] == '^')
                    {
                        blizz.Add((i, j, 3));
                    }
                }
                j++;
            }
            var x = 0;
            var y = 0;
            while (map[x,y])
            {
                x++;
            }
            var possible = new HashSet<(int, int)>();
            possible.Add((x, y));
            while(true)
            {
                var newblizz = MoveBlizz(blizz, map);
                var newPos = new HashSet<(int, int)>();
                foreach(var pos in possible)
                {
                    if (!newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1, pos.Item2));
                    }
                    if (!map[pos.Item1 + 1, pos.Item2] && !newblizz.Any(x => x.Item1 == pos.Item1 + 1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1 + 1, pos.Item2));
                    }
                    if (!map[pos.Item1 - 1, pos.Item2] && !newblizz.Any(x => x.Item1 == pos.Item1 - 1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1 - 1, pos.Item2));
                    }
                    if (!map[pos.Item1, pos.Item2 + 1] && !newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2 + 1))
                    {
                        newPos.Add((pos.Item1, pos.Item2 + 1));
                    }
                    if (!(pos.Item1 == 1 && pos.Item2 == 0) &&!map[pos.Item1, pos.Item2 - 1] && !newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2 - 1))
                    {
                        newPos.Add((pos.Item1, pos.Item2 - 1));
                    }
                }
                possible = newPos;
                blizz = newblizz;
                if (possible.Any(x => x.Item1 == map.GetLength(0) - 2 && x.Item2 == map.GetLength(1) - 1))
                {
                    break;
                }
                step++;
            }

            possible = new HashSet<(int, int)>();
            step++;
            possible.Add((map.GetLength(0) - 2, map.GetLength(1) - 1));
            while (true)
            {
                var newblizz = MoveBlizz(blizz, map);
                var newPos = new HashSet<(int, int)>();
                foreach (var pos in possible)
                {
                    if (!newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1, pos.Item2));
                    }
                    if (!map[pos.Item1 + 1, pos.Item2] && !newblizz.Any(x => x.Item1 == pos.Item1 + 1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1 + 1, pos.Item2));
                    }
                    if (!map[pos.Item1 - 1, pos.Item2] && !newblizz.Any(x => x.Item1 == pos.Item1 - 1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1 - 1, pos.Item2));
                    }
                    if (!(pos.Item1 == map.GetLength(0) - 2 && pos.Item2 == map.GetLength(1) - 1) && !map[pos.Item1, pos.Item2 + 1] && !newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2 + 1))
                    {
                        newPos.Add((pos.Item1, pos.Item2 + 1));
                    }
                    if (!(pos.Item1 == 1 && pos.Item2 == 0) && !map[pos.Item1, pos.Item2 - 1] && !newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2 - 1))
                    {
                        newPos.Add((pos.Item1, pos.Item2 - 1));
                    }
                }
                possible = newPos;
                blizz = newblizz;
                if (possible.Any(x => x.Item1 == 1 && x.Item2 == 0))
                {
                    break;
                }
                step++;
            }

            possible = new HashSet<(int, int)>();
            step++;
            possible.Add((x, y));
            while (true)
            {
                var newblizz = MoveBlizz(blizz, map);
                var newPos = new HashSet<(int, int)>();
                foreach (var pos in possible)
                {
                    if (!newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1, pos.Item2));
                    }
                    if (!map[pos.Item1 + 1, pos.Item2] && !newblizz.Any(x => x.Item1 == pos.Item1 + 1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1 + 1, pos.Item2));
                    }
                    if (!map[pos.Item1 - 1, pos.Item2] && !newblizz.Any(x => x.Item1 == pos.Item1 - 1 && x.Item2 == pos.Item2))
                    {
                        newPos.Add((pos.Item1 - 1, pos.Item2));
                    }
                    if (!map[pos.Item1, pos.Item2 + 1] && !newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2 + 1))
                    {
                        newPos.Add((pos.Item1, pos.Item2 + 1));
                    }
                    if (!(pos.Item1 == 1 && pos.Item2 == 0) && !map[pos.Item1, pos.Item2 - 1] && !newblizz.Any(x => x.Item1 == pos.Item1 && x.Item2 == pos.Item2 - 1))
                    {
                        newPos.Add((pos.Item1, pos.Item2 - 1));
                    }
                }
                possible = newPos;
                blizz = newblizz;
                if (possible.Any(x => x.Item1 == map.GetLength(0) - 2 && x.Item2 == map.GetLength(1) - 1))
                {
                    break;
                }
                step++;
            }

            return step.ToString();
        }
        
        private static List<(int, int, int)> MoveBlizz(List<(int, int, int)> blizz, bool[,] map)
        {
            var newList = new List<(int, int, int)>();
            foreach(var bl in blizz)
            {
                if (bl.Item3 == 0)
                {
                    if (map[bl.Item1 + 1, bl.Item2])
                    {
                        newList.Add((1, bl.Item2, bl.Item3));
                    }
                    else
                    {
                        newList.Add((bl.Item1 + 1, bl.Item2, bl.Item3));
                    }
                }
                else if (bl.Item3 == 2)
                {
                    if (map[bl.Item1 - 1, bl.Item2])
                    {
                        newList.Add((map.GetLength(0)-2, bl.Item2, bl.Item3));
                    }
                    else
                    {
                        newList.Add((bl.Item1 - 1, bl.Item2, bl.Item3));
                    }
                }
                else if (bl.Item3 == 1)
                {
                    if (map[bl.Item1, bl.Item2 + 1])
                    {
                        if (map[0, bl.Item2])
                        {
                            newList.Add((bl.Item1, 1, bl.Item3));
                        }
                        else
                        {
                            newList.Add((bl.Item1, 0, bl.Item3));
                        }
                    }
                    else
                    {
                        newList.Add((bl.Item1, bl.Item2 + 1, bl.Item3));
                    }
                }
                else if (bl.Item3 == 3)
                {
                    if (bl.Item2 == 0 || map[bl.Item1, bl.Item2 - 1])
                    {
                        newList.Add((bl.Item1, map.GetLength(1) - 2, bl.Item3));
                    }
                    else
                    {
                        newList.Add((bl.Item1, bl.Item2 - 1, bl.Item3));
                    }
                }
            }
            return newList;
        }
    }
}