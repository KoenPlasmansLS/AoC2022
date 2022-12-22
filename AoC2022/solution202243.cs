namespace AoC2022
{
    public class Solution202243 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution22.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var arr = new bool?[lines[0].Length - 1, lines.Count() - 2];
            var j = 0;
            var path = new List<(int, char)>();
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    if (line == lines.Last())
                    {
                        var index = 0;
                        var splt = line.Trim().Split(new char[2] { 'R', 'L' });
                        foreach (var nr in splt)
                        {
                            index += nr.Length;
                            if (index >= line.Length) path.Add((int.Parse(nr), ' '));
                            else path.Add((int.Parse(nr), line[index]));
                            index++;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < lines[j].Length - 1; i++)
                        {
                            if (line[i] == '.') arr[i, j] = false;
                            if (line[i] == '#') arr[i, j] = true;
                        }
                        j++;
                    }
                }
            }


            for (var t = 0; t < arr.GetLength(1); t++)
            {
                for (var u = 0; u < arr.GetLength(0); u++)
                {
                    if (arr[u, t] == null) Console.Write(' ');
                    if (arr[u, t] == false) Console.Write('.');
                    if (arr[u, t] == true) Console.Write('#');
                }
                Console.WriteLine();
            }


            var y = 0;
            var x = 0;
            var dir = 0;
            while (arr[x, y] == null || arr[x, y] == true)
            {
                x++;
            }
            foreach ((int step, char d) in path)
            {
                if (dir == 0)
                {
                    for (var k = 0; k < step; k++)
                    {
                        var nextX = (x + 1) % arr.GetLength(0);
                        if (arr[nextX, y] == null)
                        {
                            nextX = 0;
                            while (arr[nextX, y] == null)
                            {
                                nextX++;
                            }
                            if (arr[nextX, y] == true)
                            {
                                break;
                            }
                            else
                            {
                                x = nextX;
                            }
                        }
                        else if (arr[nextX, y] == true)
                        {
                            break;
                        }
                        else
                        {
                            x = nextX;
                        }
                    }
                    if (d == 'R') dir = 1;
                    else if (d == 'L') dir = 3;
                }
                else if (dir == 1)
                {
                    for (var k = 0; k < step; k++)
                    {
                        var nextY = (y + 1) % arr.GetLength(1);
                        if (arr[x, nextY] == null)
                        {
                            nextY = 0;
                            while (arr[x, nextY] == null)
                            {
                                nextY++;
                            }
                            if (arr[x, nextY] == true)
                            {
                                break;
                            }
                            else
                            {
                                y = nextY;
                            }
                        }
                        else if (arr[x, nextY] == true)
                        {
                            break;
                        }
                        else
                        {
                            y = nextY;
                        }
                    }
                    if (d == 'R') dir = 2;
                    else if (d == 'L') dir = 0;
                }
                else if (dir == 2)
                {
                    for (var k = 0; k < step; k++)
                    {
                        var nextX = (x - 1 + arr.GetLength(0)) % arr.GetLength(0);
                        if (arr[nextX, y] == null)
                        {
                            nextX = arr.GetLength(0) - 1;
                            while (arr[nextX, y] == null)
                            {
                                nextX--;
                            }
                            if (arr[nextX, y] == true)
                            {
                                break;
                            }
                            else
                            {
                                x = nextX;
                            }
                        }
                        else if (arr[nextX, y] == true)
                        {
                            break;
                        }
                        else
                        {
                            x = nextX;
                        }
                    }
                    if (d == 'R') dir = 3;
                    else if (d == 'L') dir = 1;
                }
                else if (dir == 3)
                {
                    for (var k = 0; k < step; k++)
                    {
                        var nextY = (y - 1 + arr.GetLength(1)) % arr.GetLength(1);
                        if (arr[x, nextY] == null)
                        {
                            nextY = arr.GetLength(1) - 1;
                            while (arr[x, nextY] == null)
                            {
                                nextY--;
                            }
                            if (arr[x, nextY] == true)
                            {
                                break;
                            }
                            else
                            {
                                y = nextY;
                            }
                        }
                        else if (arr[x, nextY] == true)
                        {
                            break;
                        }
                        else
                        {
                            y = nextY;
                        }
                    }
                    if (d == 'R') dir = 0;
                    else if (d == 'L') dir = 2;
                }
            }
            return ((y + 1) * 1000 + (x + 1) * 4 + dir).ToString();
        }
    }
}