using System.Drawing;

namespace AoC2022
{
    public class Solution202342 : IProvideSolution
    {
        private enum Dir
        {
            N, S, E, W
        }

        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution21.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] input)
        {
            var gridSize = input.Count() == input[0].Trim().Length ? input.Count() : throw new ArgumentOutOfRangeException();

            var start = Enumerable.Range(0, gridSize)
                .SelectMany(i => Enumerable.Range(0, gridSize)
                    .Where(j => input[i][j] == 'S')
                    .Select<int, (int, int)>(j => new (i, j)))
                .Single();

            var grids = 26501365 / gridSize;
            var rem = 26501365 % gridSize;

            // By inspection, the grid is square and there are no barriers on the direct horizontal / vertical path from S
            // So, we'd expect the result to be quadratic in (rem + n * gridSize) steps, i.e. (rem), (rem + gridSize), (rem + 2 * gridSize), ...
            // Use the code from Part 1 to calculate the first three values of this sequence, which is enough to solve for ax^2 + bx + c
            var sequence = new List<int>();
            var work = new HashSet<(int, int)> { start };
            var steps = 0;
            for (var n = 0; n < 3; n++)
            {
                for (; steps < n * gridSize + rem; steps++)
                {
                    // Funky modulo arithmetic bc modulo of a negative number is negative, which isn't what we want here
                    work = new HashSet<(int, int)>(work
                        .SelectMany(it => new[] { Dir.N, Dir.S, Dir.E, Dir.W }.Select(dir => Move(it, dir)))
                        .Where(dest => input[((dest.Item1 % 131) + 131) % 131][((dest.Item2 % 131) + 131) % 131] != '#'));
                }

                sequence.Add(work.Count);
            }

            // Solve for the quadratic coefficients
            var c = sequence[0];
            var aPlusB = sequence[1] - c;
            var fourAPlusTwoB = sequence[2] - c;
            var twoA = fourAPlusTwoB - (2 * aPlusB);
            var a = twoA / 2;
            var b = aPlusB - a;

            return (a * (grids * grids) + b * grids + c).ToString();
        }

        private (int, int) Move((int, int) coord, Dir dir, int dist = 1)
        {
            return dir switch
            {
                Dir.N => new (coord.Item1 - dist, coord.Item2),
                Dir.S => new (coord.Item1 + dist, coord.Item2),
                Dir.E => new (coord.Item1, coord.Item2 + dist),
                Dir.W => new (coord.Item1, coord.Item2 - dist),
            };
        }

    }
}