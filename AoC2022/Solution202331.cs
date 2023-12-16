namespace AoC2022
{
    public class Solution202331 : IProvideSolution
    {
        private bool[,] left;
        private bool[,] right;
        private bool[,] up;
        private bool[,] down;
        private char[,] arr;

        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution16.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            arr = new char[lines[0].Trim().Length, lines.Count()];
            for(var i = 0; i < lines.Length;i++)
            {
                for(var j = 0; j < lines[i].Trim().Length;j++)
                {
                    arr[j,i] = lines[i][j];
                }
            }
            left = new bool[arr.GetLength(0), arr.GetLength(1)];
            right = new bool[arr.GetLength(0), arr.GetLength(1)];
            down = new bool[arr.GetLength(0), arr.GetLength(1)];
            up = new bool[arr.GetLength(0), arr.GetLength(1)];
            DoBeam(0, 0, Dir.Right);
            var sum = 0;
            for (var i = 0; i < left.GetLength(0); i++)
            {
                for (var j = 0; j < left.GetLength(1); j++)
                {
                    if (left[i,j] || right[i,j] || up[i,j] || down[i,j])
                    {
                        sum++;
                    }
                }
            }
            return sum.ToString();
        }

        private void DoBeam(int x, int y , Dir dir)
        {
            while (true)
            {
                if (x < 0 || x >= left.GetLength(0) || y < 0 || y >= left.GetLength(1)) return;
                if (GetBool(dir, x, y)) return; //visited
                SetBool(dir, x, y); //visit
                if (arr[x, y] == '|' && (dir == Dir.Left || dir == Dir.Right))
                {
                    DoBeam(x, y + 1, Dir.Down);
                    DoBeam(x, y - 1, Dir.Up);
                    return;
                }
                else if (arr[x, y] == '-' && (dir == Dir.Up || dir == Dir.Down))
                {
                    DoBeam(x - 1, y, Dir.Left);
                    DoBeam(x + 1, y, Dir.Right);
                    return;
                }
                else if (arr[x, y] == '/')
                {
                    switch (dir)
                    {
                        case Dir.Left:
                            dir = Dir.Down;
                            break;
                        case Dir.Right:
                            dir = Dir.Up;
                            break;
                        case Dir.Up:
                            dir = Dir.Right;
                            break;
                        case Dir.Down:
                            dir = Dir.Left;
                            break;
                    }
                }
                else if (arr[x, y] == '\\')
                {
                    switch (dir)
                    {
                        case Dir.Left:
                            dir = Dir.Up;
                            break;
                        case Dir.Right:
                            dir = Dir.Down;
                            break;
                        case Dir.Up:
                            dir = Dir.Left;
                            break;
                        case Dir.Down:
                            dir = Dir.Right;
                            break;
                    }
                }
                switch (dir)
                {
                    case Dir.Left:
                        x--;
                        break;
                    case Dir.Right:
                        x++;
                        break;
                    case Dir.Up:
                        y--;
                        break;
                    case Dir.Down:
                        y++;
                        break;
                }
            }
        }

        private bool GetBool(Dir dir, int x, int y)
        {
            return dir switch
            {
                Dir.Right => right[x, y],
                Dir.Up => up[x, y],
                Dir.Down => down[x, y],
                _ => left[x, y],
            };
        }

        private void SetBool(Dir dir, int x, int y)
        {
            switch (dir)
            {
                case Dir.Right:
                    right[x, y] = true;
                    break;
                case Dir.Up:
                    up[x, y] = true;
                    break;
                case Dir.Down:
                    down[x, y] = true;
                    break;
                default:
                    left[x, y] = true;
                    break;
            }
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