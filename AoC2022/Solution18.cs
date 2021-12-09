namespace AoC2022
{
    public class Solution18 : IProvideSolution
    {
        private bool[,] _visited;
        private int[,] _arr;
        private int _x;
        private int _y;

        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution9.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            _y = lines.Count();
            _x = lines[0].Length - 1;
            _arr = new int[_x, _y];
            for (var i = 0; i < _x; i++)
            {
                for (var j = 0; j < _y; j++)
                {
                    _arr[i, j] = int.Parse(lines[j][i].ToString());
                }
            }
            _visited = new bool[_x, _y];
            var pools = new List<int>();
            for (var i = 0; i < _x; i++)
            {
                for (var j = 0; j < _y; j++)
                {
                    if (!_visited[i,j])
                    {
                        pools.Add(Find(i, j,  0));
                    }
                }
            }
            pools.Sort();
            pools.Reverse();
            long product = 1;
            foreach(var pool in pools.Take(3))
            {
                product *= pool;
            }
            return product.ToString();
        }

        private int Find(int i, int j, int pool)
        {         
            if (_arr[i, j] != 9 && !_visited[i, j])
            {
                _visited[i, j] = true;
                pool++;
                if (i > 0)
                {
                    pool = Find(i - 1, j, pool);
                }
                if (j > 0)
                {
                    pool = Find(i, j - 1, pool);
                }
                if (j < _y - 1)
                {
                    pool = Find(i, j + 1, pool);
                }
                if (i < _x - 1)
                {
                    pool = Find(i + 1, j, pool);
                }
            }
            return pool;
        }
    }
}