namespace AoC2022
{
    public class Solution19 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution10.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            foreach(var line in lines)
            {
                var lineToInvestigate = line.Trim();
                var newLine = lineToInvestigate;
                do
                {
                    lineToInvestigate = newLine;
                    newLine = lineToInvestigate
                        .Replace("<>", string.Empty)
                        .Replace("[]", string.Empty)
                        .Replace("{}", string.Empty)
                        .Replace("()", string.Empty);

                }
                while (newLine != string.Empty && newLine.Length != lineToInvestigate.Length);
                if (newLine != string.Empty)
                {
                    var incompleteOrCorrupted = newLine.Replace("(", string.Empty).Replace("[", string.Empty).Replace("{", string.Empty).Replace("<", string.Empty);
                    if (incompleteOrCorrupted != string.Empty)
                    {
                        var corruptedChar = incompleteOrCorrupted[0];
                        if (corruptedChar == '>') sum += 25137;
                        if (corruptedChar == ')') sum += 3;
                        if (corruptedChar == ']') sum += 57;
                        if (corruptedChar == '}') sum += 1197;
                    }
                }
            }
            return sum.ToString();
        }
    }
}