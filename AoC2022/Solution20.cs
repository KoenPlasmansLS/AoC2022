namespace AoC2022
{
    public class Solution20 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution10.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var scores = new List<long>();
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
                    if (incompleteOrCorrupted == string.Empty)
                    {
                        var reverse = newLine.Reverse();
                        long score = 0;
                        foreach(var chr in reverse)
                        {
                            score *= 5;
                            if (chr == '(') score += 1;
                            if (chr == '[') score += 2;
                            if (chr == '{') score += 3;
                            if (chr == '<') score += 4;
                        }
                        scores.Add(score);
                    }
                }
            }
            scores.Sort();
            return scores[scores.Count / 2].ToString();
        }
    }
}