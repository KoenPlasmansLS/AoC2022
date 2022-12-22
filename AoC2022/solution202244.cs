namespace AoC2022
{
    public class Solution202244 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution22.txt").OpenText().ReadToEnd().Split("\n");
            var linesS = lines.Select(x => x.Replace("\r", "")).ToList();
            return Part2(linesS);
        }

        private static string Part2(List<string> input)
        {
            var (instructions, map, x, y, direction) = FormatInput(input);

            foreach (var instruction in instructions)
            {
                for (var i = 0; i < instruction.Distance; ++i)
                {
                    var row = map[y];
                    switch (direction)
                    {
                        case Direction.Right:
                            if (x + 1 >= row.Count)
                            {
                                //Face 2
                                var tile = map[149 - y][99];
                                if (tile == Tile.Path)
                                {
                                    x = 99;
                                    y = 149 - y;
                                    direction = Direction.Left;
                                }
                                else
                                {
                                    i = instruction.Distance;
                                }
                            }
                            else if (row[x + 1] == Tile.Path)
                            {
                                ++x;
                            }
                            else if (row[x + 1] == Tile.Wall)
                            {
                                i = instruction.Distance;
                            }
                            else
                            {
                                //Face 3
                                if (y < 100)
                                {
                                    var tile = map[49][y + 50];
                                    if (tile == Tile.Path)
                                    {
                                        x = y + 50;
                                        y = 49;
                                        direction = Direction.Up;
                                    }
                                    else if (tile == Tile.None)
                                    {
                                        throw new Exception("");
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                                //Face 4
                                else if (y < 150)
                                {
                                    var tile = map[149 - y][149];
                                    if (tile == Tile.Path)
                                    {
                                        x = 149;
                                        y = 149 - y;
                                        direction = Direction.Left;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                                //Face 6
                                else
                                {
                                    var tile = map[149][y - 100];
                                    if (tile == Tile.Path)
                                    {
                                        x = y - 100;
                                        y = 149;
                                        direction = Direction.Up;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                            }
                            break;

                        case Direction.Left:
                            if (x - 1 < 0)
                            {
                                //Face 5
                                if (y < 150)
                                {
                                    var tile = map[149 - y][50];
                                    if (tile == Tile.Path)
                                    {
                                        x = 50;
                                        y = 149 - y;
                                        direction = Direction.Right;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                                //Face 6
                                else
                                {
                                    var tile = map[0][y - 100];
                                    if (tile == Tile.Path)
                                    {
                                        x = y - 100;
                                        y = 0;
                                        direction = Direction.Down;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                            }
                            else if (row[x - 1] == Tile.Path)
                            {
                                --x;
                            }
                            else if (row[x - 1] == Tile.Wall)
                            {
                                i = instruction.Distance;
                            }
                            else
                            {
                                //Face 1
                                if (y < 50)
                                {
                                    var tile = map[149 - y][0];
                                    if (tile == Tile.Path)
                                    {
                                        x = 0;
                                        y = 149 - y;
                                        direction = Direction.Right;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                                //Face 3
                                else
                                {
                                    var tile = map[100][y - 50];
                                    if (tile == Tile.Path)
                                    {
                                        x = y - 50;
                                        y = 100;
                                        direction = Direction.Down;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                            }
                            break;

                        case Direction.Down:
                            if (y + 1 >= map.Count)
                            {
                                //Face 6
                                var tile = map[0][x + 100];
                                if (tile == Tile.Path)
                                {
                                    x = x + 100;
                                    y = 0;
                                }
                                else
                                {
                                    i = instruction.Distance;
                                }
                            }
                            else if (map[y + 1][x] == Tile.Path)
                            {
                                ++y;
                            }
                            else if (map[y + 1][x] == Tile.Wall)
                            {
                                i = instruction.Distance;
                            }
                            else
                            {
                                //Face 4
                                if (x < 100)
                                {
                                    var tile = map[x + 100][49];
                                    if (tile == Tile.Path)
                                    {
                                        y = x + 100;
                                        x = 49;
                                        direction = Direction.Left;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                                //Face 2
                                else
                                {
                                    var tile = map[x - 50][99];
                                    if (tile == Tile.Path)
                                    {
                                        y = x - 50;
                                        x = 99;
                                        direction = Direction.Left;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                            }
                            break;

                        case Direction.Up:
                            if (y - 1 < 0)
                            {
                                //Face 1
                                if (x < 100)
                                {
                                    var tile = map[x + 100][0];
                                    if (tile == Tile.Path)
                                    {
                                        y = x + 100;
                                        x = 0;
                                        direction = Direction.Right;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                                //Face 2
                                else
                                {
                                    var tile = map[199][x - 100];
                                    if (tile == Tile.Path)
                                    {
                                        x = x - 100;
                                        y = 199;
                                    }
                                    else
                                    {
                                        i = instruction.Distance;
                                    }
                                }
                            }
                            else if (map[y - 1][x] == Tile.Path)
                            {
                                --y;
                            }
                            else if (map[y - 1][x] == Tile.Wall)
                            {
                                i = instruction.Distance;
                            }
                            else
                            {
                                //Face 5
                                var tile = map[x + 50][50];
                                if (tile == Tile.Path)
                                {
                                    y = x + 50;
                                    x = 50;
                                    direction = Direction.Right;
                                }
                                else
                                {
                                    i = instruction.Distance;
                                }
                            }
                            break;
                    }
                }

                direction = GetNewDirection(direction, instruction.DirectionToTurn);
            }

            return CalculateScore(x, y, direction);
        }

        private static (List<(char DirectionToTurn, int Distance)>, List<List<Tile>>, int, int, Direction) FormatInput(List<string> input)
        {
            var rowLength = input.TakeWhile(i => !string.IsNullOrEmpty(i)).Max(i => i.Length);
            var map = input.TakeWhile(i => !string.IsNullOrEmpty(i))
                .Select(i => i.PadRight(rowLength).Select(j => j == ' ' ? Tile.None : j == '.' ? Tile.Path : Tile.Wall).ToList())
                .ToList();

            var instructions = input[^1].Split('R').SelectMany(i =>
            {
                var split = i.Split('L').Select(s => int.Parse(s)).ToList();
                var line = split.Take(split.Count - 1).Select(s => ('L', s)).ToList();
                line.Add(('R', split.Last()));
                return line;
            }).ToList();
            var last = instructions.Last().s;
            instructions.RemoveAt(instructions.Count - 1);
            instructions.Add(('S', last));

            var direction = Direction.Right;
            var x = map[0].FindIndex(t => t == Tile.Path);
            var y = 0;

            return (instructions, map, x, y, direction);
        }

        private static Direction GetNewDirection(Direction direction, char directionToTurn)
        {
            switch (directionToTurn)
            {
                case 'R':
                    switch (direction)
                    {
                        case Direction.Right:
                            return Direction.Down;

                        case Direction.Down:
                            return Direction.Left;

                        case Direction.Left:
                            return Direction.Up;

                        case Direction.Up:
                            return Direction.Right;
                    }
                    break;

                case 'L':
                    switch (direction)
                    {
                        case Direction.Right:
                            return Direction.Up;

                        case Direction.Up:
                            return Direction.Left;

                        case Direction.Left:
                            return Direction.Down;

                        case Direction.Down:
                            return Direction.Right;
                    }
                    break;
            }

            return direction;
        }

        private static string CalculateScore(int x, int y, Direction direction)
        {
            var directionScore = 0;
            switch (direction)
            {
                case Direction.Down:
                    directionScore = 1;
                    break;

                case Direction.Left:
                    directionScore = 2;
                    break;

                case Direction.Up:
                    directionScore = 3;
                    break;
            }

            return (directionScore + 4 * (x + 1) + 1000 * (y + 1)).ToString();
        }

        private enum Tile
        {
            None,
            Path,
            Wall
        }

        private enum Direction
        {
            Right,
            Left,
            Up,
            Down
        }
        //    protected static string BaseAlgorithm(string[] lines)
        //    {
        //        var (instructions, map, x, y, direction) = FormatInput(lines.ToList());

        //        foreach (var instruction in instructions)
        //        {
        //            for (var i = 0; i < instruction.Distance; ++i)
        //            {
        //                var row = map[y];
        //                switch (direction)
        //                {
        //                    case 0:
        //                        if (x + 1 >= row.Count)
        //                        {
        //                            //Face 2
        //                            var tile = map[149 - y][99];
        //                            if (tile == false)
        //                            {
        //                                x = 99;
        //                                y = 149 - y;
        //                                direction = 2;
        //                            }
        //                            else
        //                            {
        //                                i = instruction.Distance;
        //                            }
        //                        }
        //                        else if (row[x + 1] == false)
        //                        {
        //                            ++x;
        //                        }
        //                        else if (row[x + 1] == true)
        //                        {
        //                            i = instruction.Distance;
        //                        }
        //                        else
        //                        {
        //                            //Face 3
        //                            if (y < 100)
        //                            {
        //                                var tile = map[49][y + 50];
        //                                if (tile == false)
        //                                {
        //                                    x = y + 50;
        //                                    y = 49;
        //                                    direction = 3;
        //                                }
        //                                else if (tile == null)
        //                                {
        //                                    throw new Exception("");
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                            //Face 4
        //                            else if (y < 150)
        //                            {
        //                                var tile = map[149 - y][149];
        //                                if (tile == false)
        //                                {
        //                                    x = 149;
        //                                    y = 149 - y;
        //                                    direction = 2;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                            //Face 6
        //                            else
        //                            {
        //                                var tile = map[149][y - 100];
        //                                if (tile == false)
        //                                {
        //                                    x = y - 100;
        //                                    y = 149;
        //                                    direction = 3;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                        }
        //                        break;

        //                    case 2:
        //                        if (x - 1 < 0)
        //                        {
        //                            //Face 5
        //                            if (y < 150)
        //                            {
        //                                var tile = map[149 - y][50];
        //                                if (tile == false)
        //                                {
        //                                    x = 50;
        //                                    y = 149 - y;
        //                                    direction = 0;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                            //Face 6
        //                            else
        //                            {
        //                                var tile = map[0][y - 100];
        //                                if (tile == false)
        //                                {
        //                                    x = y - 100;
        //                                    y = 0;
        //                                    direction = 1;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                        }
        //                        else if (row[x - 1] == false)
        //                        {
        //                            --x;
        //                        }
        //                        else if (row[x - 1] == true)
        //                        {
        //                            i = instruction.Distance;
        //                        }
        //                        else
        //                        {
        //                            //Face 1
        //                            if (y < 50)
        //                            {
        //                                var tile = map[149 - y][0];
        //                                if (tile == false)
        //                                {
        //                                    x = 0;
        //                                    y = 149 - y;
        //                                    direction = 0;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                            //Face 3
        //                            else
        //                            {
        //                                var tile = map[100][y - 50];
        //                                if (tile == false)
        //                                {
        //                                    x = y - 50;
        //                                    y = 100;
        //                                    direction = 1;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                        }
        //                        break;

        //                    case 1:
        //                        if (y + 1 >= map.Count)
        //                        {
        //                            //Face 6
        //                            var tile = map[0][x + 100];
        //                            if (tile == false)
        //                            {
        //                                x += 100;
        //                                y = 0;
        //                            }
        //                            else
        //                            {
        //                                i = instruction.Distance;
        //                            }
        //                        }
        //                        else if (map[y + 1][x] == false)
        //                        {
        //                            ++y;
        //                        }
        //                        else if (map[y + 1][x] == true)
        //                        {
        //                            i = instruction.Distance;
        //                        }
        //                        else
        //                        {
        //                            //Face 4
        //                            if (x < 100)
        //                            {
        //                                var tile = map[x + 100][49];
        //                                if (tile == false)
        //                                {
        //                                    y = x + 100;
        //                                    x = 49;
        //                                    direction = 2;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                            //Face 2
        //                            else
        //                            {
        //                                var tile = map[x - 50][99];
        //                                if (tile == false)
        //                                {
        //                                    y = x - 50;
        //                                    x = 99;
        //                                    direction = 2;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                        }
        //                        break;

        //                    case 3:
        //                        if (y - 1 < 0)
        //                        {
        //                            //Face 1
        //                            if (x < 100)
        //                            {
        //                                var tile = map[x + 100][0];
        //                                if (tile == false)
        //                                {
        //                                    y = x + 100;
        //                                    x = 0;
        //                                    direction = 0;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                            //Face 2
        //                            else
        //                            {
        //                                var tile = map[199][x - 100];
        //                                if (tile == false)
        //                                {
        //                                    x -= 100;
        //                                    y = 199;
        //                                }
        //                                else
        //                                {
        //                                    i = instruction.Distance;
        //                                }
        //                            }
        //                        }
        //                        else if (map[y - 1][x] == false)
        //                        {
        //                            --y;
        //                        }
        //                        else if (map[y - 1][x] == true)
        //                        {
        //                            i = instruction.Distance;
        //                        }
        //                        else
        //                        {
        //                            //Face 5
        //                            var tile = map[x + 50][50];
        //                            if (tile == false)
        //                            {
        //                                y = x + 50;
        //                                x = 50;
        //                                direction = 0;
        //                            }
        //                            else
        //                            {
        //                                i = instruction.Distance;
        //                            }
        //                        }
        //                        break;
        //                }
        //            }
        //            if (instruction.DirectionToTurn == 'R') direction = (direction + 1) % 4;
        //            else if (instruction.DirectionToTurn == 'L') direction = (direction - 1 + 4) % 4;
        //        }
        //        return ((y + 1) * 1000 + (x + 1) * 4 + direction).ToString();
        //    }

        //    private static (List<(char DirectionToTurn, int Distance)>, List<List<bool?>>, int, int, int) FormatInput(List<string> input)
        //    {
        //        var rowLength = input.TakeWhile(i => !string.IsNullOrEmpty(i)).Max(i => i.Length);
        //        var map = input.TakeWhile(i => !string.IsNullOrEmpty(i))
        //            .Select(i => i.PadRight(rowLength).Select<char, bool?>(j => j == ' ' ? null : j != '.').ToList())
        //            .ToList();

        //        var instructions = input[^1].Split('R').SelectMany(i =>
        //        {
        //            var split = i.Split('L').Select(s => int.Parse(s)).ToList();
        //            var line = split.Take(split.Count - 1).Select(s => ('L', s)).ToList();
        //            line.Add(('R', split.Last()));
        //            return line;
        //        }).ToList();
        //        var last = instructions.Last().s;
        //        instructions.RemoveAt(instructions.Count - 1);
        //        instructions.Add(('S', last));

        //        var direction = 0;
        //        var x = map[0].FindIndex(t => t == false);
        //        var y = 0;

        //        return (instructions, map, x, y, direction);
        //    }
    }
}