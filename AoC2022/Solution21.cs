namespace AoC2022
{
    public class Solution21 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/solution11.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected virtual string BaseAlgorithm(string[] lines)
        {
            var arr = GetData(lines);
            var sum = 0;
            for(var k = 0; k < 100;k++)
            {
                (arr, int flash) = ApplyStep(arr);
                sum += flash;
            }
            return sum.ToString();
        }

        protected int[,] GetData(string[] lines)
        {
            var arr = new int[10, 10];
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    arr[i, j] = int.Parse(lines[i][j].ToString());
                }
            }
            return arr;
        }

        protected (int[,], int) ApplyStep(int[,] arr)
        {
            var flashed = new bool[10, 10];
            var nrFlashed = 0;
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    arr[i, j] += 1;
                }
            }
            var newFlashed = 0;
            do
            {
                nrFlashed = newFlashed;
                for (var i = 0; i < 10; i++)
                {
                    for (var j = 0; j < 10; j++)
                    {
                        if(arr[i, j] > 9 && !flashed[i, j])
                        {
                            flashed[i, j] = true;
                            newFlashed += 1;
                            arr[i, j] = 0;
                            if (i != 9 && !flashed[i + 1, j]) arr[i + 1, j] += 1;
                            if (i != 9 && j != 9 && !flashed[i + 1, j + 1]) arr[i + 1, j + 1] += 1;
                            if (i != 9 && j > 0 && !flashed[i + 1, j - 1]) arr[i + 1, j - 1] += 1;
                            if (j != 9 && !flashed[i, j + 1]) arr[i, j + 1] += 1;
                            if (j > 0 && !flashed[i, j - 1]) arr[i, j - 1] += 1;
                            if (i > 0 && j > 0 && !flashed[i - 1, j - 1]) arr[i - 1, j - 1] += 1;
                            if (i > 0 && !flashed[i - 1, j]) arr[i - 1, j] += 1;
                            if (i > 0 && j != 9 && !flashed[i - 1, j + 1]) arr[i - 1, j + 1] += 1;
                        }
                    }
                }
            }
            while (newFlashed != nrFlashed);
            return (arr, nrFlashed);
        }
    }
}