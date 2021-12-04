namespace AoC2022
{
    public class Solution7 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/solution4.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            List<int> nrsCalled = lines[0].Split(',').Select(int.Parse).ToList();
            (List<int[,]> boards, List<bool[,]> filled) = FetchData(lines);

            var indexOfBingo = 0;
            var next = 0;
            var ignore = new List<int>();
            while (true)
            {
                next = nrsCalled[indexOfBingo];
                for(var i = 0; i < boards.Count; i++)
                {
                    if (ignore.Contains(i)) continue;
                    var board = boards[i];
                    var filledBoard = filled[i];
                    for(var k = 0; k < 5; k++)
                    {
                        for(var j = 0; j < 5; j++)
                        {
                            if (board[k,j] == next)
                            {
                                filledBoard[k, j] = true; 
                            }
                        }
                    }
                    for (var k = 0; k < 5; k++)
                    {
                        var bingoRow = true;
                        var bingoColumn = true;
                        for (var j = 0; j < 5; j++)
                        {
                            if(!filledBoard[k,j])
                            {
                                bingoRow = false;
                            }
                            if (!filledBoard[j, k])
                            {
                                bingoColumn = false;
                            }
                        }
                        if (bingoRow || bingoColumn)
                        {
                            if (IsBingo(i, boards.Count, ignore))
                            {
                                return (next * CalculSumNotBingo(board, filledBoard)).ToString();
                            }
                            break;
                        }
                    }
                }
                indexOfBingo++;
            }
        }

        protected virtual bool IsBingo(int i, int maxLength, List<int> ignore)
        {
            return true;
        }
        
        private static (List<int[,]>, List<bool[,]>) FetchData(string[] lines)
        {
            List<int> nrsCalled = lines[0].Split(',').Select(int.Parse).ToList();

            var boards = new List<int[,]> { };
            var filled = new List<bool[,]> { };
            for (var i = 2; i <= lines.Length; i += 6)
            {
                filled.Add(new bool[5, 5]);
                var nrsBoard = new int[5, 5];
                for (var j = 0; j < 5; j++)
                {
                    var valuesPerLine = lines[i + j].Trim().Replace("  ", " ").Split(' ');
                    for (var k = 0; k < 5; k++)
                    {
                        nrsBoard[j, k] = int.Parse(valuesPerLine[k]);
                    }
                }
                boards.Add(nrsBoard);
            }
            return (boards, filled);
        }

        private static int CalculSumNotBingo(int[,] board, bool[,] filledBoard)
        {
            var sum = 0;
            for (var k = 0; k < 5; k++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (!filledBoard[k, j])
                    {
                        sum += board[k, j];
                    }
                }
            }
            return sum;
        }
    }
}