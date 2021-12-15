namespace AoC2022
{
    public class Solution30 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution15.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var x = lines[0].Trim().Length;
            var y = lines.Count();
            var map = new int[5*x, 5*y];
            for (var i = 0; i < y; i++)
            {
                for (var j = 0; j < x; j++)
                {
                    map[j, i] = int.Parse(lines[i][j].ToString());
                }
            }
            for (var a = 0; a < 5; a++)
            {
                for (var b = 0; b < 5; b++)
                {
                    for (var i = 0; i < y; i++)
                    {
                        for (var j = 0; j < x; j++)
                        {
                            map[j + a*x, i + b*y] = ((map[j, i]+a+b-1) % 9) + 1;
                        }
                    }
                }
            }
            x *= 5;
            y *= 5;
            var visited = new bool[x, y];
            var p = new PriorityQueue<(int, int, int), int>();

            p.Enqueue((0, 0, 0), 0);
            while (true)
            {
                var (i, j, score) = p.Dequeue();
                if (i == x - 1 && j == y - 1) return score.ToString();
                if (i > 0 && !visited[i - 1, j])
                {
                    visited[i - 1, j] = true;
                    p.Enqueue((i - 1, j, score + map[i - 1, j]), score + map[i - 1, j]);
                }
                if (j > 0 && !visited[i, j - 1])
                {
                    visited[i, j - 1] = true;
                    p.Enqueue((i, j - 1, score + map[i, j - 1]), score + map[i, j - 1]);
                }
                if (i < x - 1 && !visited[i + 1, j])
                {
                    visited[i + 1, j] = true;
                    p.Enqueue((i + 1, j, score + map[i + 1, j]), score + map[i + 1, j]);
                }
                if (j < y - 1 && !visited[i, j + 1])
                {
                    visited[i, j + 1] = true;
                    p.Enqueue((i, j + 1, score + map[i, j + 1]), score + map[i, j + 1]);
                }
            }
        }
    }
}