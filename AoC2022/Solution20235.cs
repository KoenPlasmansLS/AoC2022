namespace AoC2022
{
    public class Solution20235 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution3.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var matrix = new char[lines.Count(), lines[0].Trim().Length];
            for (var i = 0; i < lines.Count(); i++)
            {
                for(var j =0; j < lines[i].Trim().Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }
            var sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                var nr = 0;
                var symbolFound = false;
                for (var j = 0; j < lines[i].Trim().Length; j++)
                {
                    if (matrix[i, j] < 48 || matrix[i,j] >= 58)
                    {
                        if (nr > 0 && symbolFound)
                        {
                            sum += nr;
                            Console.WriteLine(nr);
                        }
                        nr = 0;
                        symbolFound = false;
                    }
                    else
                    {
                        if (matrix[i, j] >= 48 && matrix[i, j] < 58)
                        {
                            nr = nr * 10 + (matrix[i, j] - 48);
                        }
                        //D
                        if (j < lines[i].Trim().Length - 1 && matrix[i, j + 1] != 46 && (matrix[i, j + 1] < 48 || matrix[i, j + 1] >= 58))
                        {
                            symbolFound = true;
                        }
                        //U
                        if (j > 0 && matrix[i, j - 1] != 46 && (matrix[i, j - 1] < 48 || matrix[i, j - 1] >= 58))
                        {
                            symbolFound = true;
                        }
                        //R
                        if (i < lines.Length - 1 && matrix[i + 1, j] != 46 && (matrix[i + 1, j] < 48 || matrix[i + 1, j] >= 58))
                        {
                            symbolFound = true;
                        }
                        //L
                        if (i > 0 && matrix[i - 1, j] != 46 && (matrix[i - 1, j] < 48 || matrix[i - 1, j] >= 58))
                        {
                            symbolFound = true;
                        }
                        //UR
                        if (i < lines.Length - 1 && j > 0 && matrix[i + 1, j - 1] != 46 && (matrix[i + 1, j - 1] < 48 || matrix[i + 1, j - 1] >= 58))
                        {
                            symbolFound = true;
                        }
                        //UL
                        if (i > 0 && j > 0 && matrix[i - 1, j - 1] != 46 && (matrix[i - 1, j - 1] < 48 || matrix[i - 1, j - 1] >= 58))
                        {
                            symbolFound = true;
                        }
                        //DR
                        if (i < lines.Length - 1 && j < lines[i].Trim().Length - 1 && matrix[i + 1, j + 1] != 46 && (matrix[i + 1, j + 1] < 48 || matrix[i + 1, j + 1] >= 58))
                        {
                            symbolFound = true;
                        }
                        //DL
                        if (i > 0 && j < lines[i].Trim().Length - 1 && matrix[i - 1, j + 1] != 46 && (matrix[i - 1, j + 1] < 48 || matrix[i - 1, j + 1] >= 58))
                        {
                            symbolFound = true;
                        }
                    }
                }
                if (nr > 0 && symbolFound)
                {
                    sum += nr;
                    Console.WriteLine(nr);
                }
            }
            return sum.ToString();
        }
    }
}