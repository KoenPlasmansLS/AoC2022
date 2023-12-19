using System.Collections.Generic;

namespace AoC2022
{
    public class Solution202333 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution17.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var arr = new int[lines[0].Trim().Length, lines.Count()];
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[i].Trim().Length; j++)
                {
                    arr[j, i] = int.Parse(lines[i][j].ToString());
                }
            }

            var q = new PriorityQueue<(int, int, int, Dir, int, List<(int, int)>),int>();
            q.Enqueue((0, 0, 0, Dir.Right, 0, new List<(int, int)>()), 0);
            var visited = new Dictionary<(int,int, Dir), int>();
            var min = 0;
            while(true)
            {
                (var length, var x, var y, Dir dir, int dirLength, List<(int,int)> lst) = q.Dequeue();
                if (x == arr.GetLength(0) - 1 && y == arr.GetLength(1) - 1)
                {
                    min = length;
                    for (var i = 0; i < arr.GetLength(1); i++)
                    {
                        for (var j = 0; j < arr.GetLength(0); j++)
                        {
                            Console.Write(lst.Contains((j, i)) ? '#' : " ");
                        }
                        Console.WriteLine();
                    }
                    break;
                }
                if (visited.TryGetValue((x, y, dir), out int value))
                {
                    if (dirLength >= value)
                    {
                        continue;
                    }
                    else
                    {
                        visited[(x, y, dir)] = dirLength;
                    }
                }
                else
                {
                    visited.Add((x,y,dir), dirLength);
                }
                if (y < arr.GetLength(1) - 1 && dir != Dir.Up && !(dir == Dir.Down && dirLength == 3))
                {
                    var c = lst.ToList();
                    c.Add((x, y + 1));
                    q.Enqueue((length + arr[x, y + 1], x, y + 1, Dir.Down, dir == Dir.Down ? dirLength+1 : 1, c), length + arr[x, y + 1]);
                }
                if (y > 0 && dir != Dir.Down && !(dir == Dir.Up && dirLength == 3))
                {
                    var c = lst.ToList();
                    c.Add((x, y -1));
                    q.Enqueue((length + arr[x, y - 1], x, y - 1, Dir.Up, dir == Dir.Up ? dirLength + 1 : 1, c), length + arr[x, y - 1]);
                }
                if (x < arr.GetLength(0) - 1 && dir != Dir.Left && !(dir == Dir.Right && dirLength == 3))
                {
                    var c = lst.ToList();
                    c.Add((x+1, y));
                    q.Enqueue((length + arr[x + 1, y], x + 1, y, Dir.Right, dir == Dir.Right ? dirLength + 1 : 1, c), length + arr[x + 1, y]);
                }
                if (x > 0 && dir != Dir.Right && !(dir == Dir.Left && dirLength == 3))
                {
                    var c = lst.ToList();
                    c.Add((x-1, y));
                    q.Enqueue((length + arr[x - 1, y], x - 1, y, Dir.Left, dir == Dir.Left ? dirLength + 1 : 1,c), length + arr[x - 1, y]);
                }
            }
            return min.ToString();
        }

        private enum Dir
        {
            Left,
            Right,
            Down,
            Up
        }
    }
}