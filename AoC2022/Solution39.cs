namespace AoC2022
{
    public class Solution39 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/solution20.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines, 2);
        }

        protected string BaseAlgorithm(string[] lines, int nr)
        {
            var ime = new bool[512];
            for(var i = 0; i < 512; i++)
            {
                if (lines[0][i] == '#') ime[i] = true;
            }
            var yMax = lines.Length - 2 + 4 * nr;
            var xMax = lines[2].Trim().Length + 4 * nr;
            var input = new bool[xMax, yMax];
            for(var j = 2; j < lines.Length; j++)
            {
                for (var i = 0; i < xMax - 4 * nr; i++)
                {
                    if (lines[j][i] == '#') input[i+2*nr, j -2 + 2*nr] = true;
                }
            }
            var output = input;
            for (var t = 0; t < nr; t++)
            {
                xMax -= 2;
                yMax -= 2;
                output = GetOutput(output, xMax, yMax, ime);
            }
            var sum = 0;
            for (var i = 0; i < output.GetLength(1); i++)
            {
                for (var j = 0; j < output.GetLength(0); j++)
                {
                    if (output[j, i])
                        sum++;
                }
            }
            return sum.ToString();
        }

        private bool[,] GetOutput(bool[,] input, int xMax, int yMax, bool[] ime)
        {
            var output = new bool[xMax, yMax];
            for(var i = 0; i < xMax; i++)
            {
                for(var j = 0; j < yMax; j++)
                {
                    var nr = 0;
                    for(var k = -1; k < 2; k++)
                    {
                        for (var l = -1; l < 2; l++)
                        {
                            var x = i + l;
                            var y = j + k;
                            nr *= 2;
                            if (input[x+1, y+1]) nr += 1;
                        }
                    }
                    output[i, j] = ime[nr];
                }
            }
            return output;
        }
    }
}