namespace AoC2022
{
    public class Solution6 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution3.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        public string BaseAlgorithm(string[] lines)
        {
            List<bool[]> boolLines = ConvertToBoolArrays(lines);
            int oxygen = CalcLevel(boolLines, x => x);
            int CO = CalcLevel(boolLines, x => !x);
            return (oxygen * CO).ToString();
        }

        private List<bool[]> ConvertToBoolArrays(string[] lines)
        {
            var columns = lines[0].Count() - 1;
            return lines.Select((x) =>
            {
                var boolLine = new bool[columns];
                for (var i = 0; i < columns; i++)
                {
                    boolLine[i] = x[i] == '1';
                }
                return boolLine;
            }).ToList();
        }

        private int CalcLevel(List<bool[]> lines, Func<bool, bool> largerOne, int i = 0)
        {
            if (lines.Count == 1)
            {
                return CalculateNumber(lines[0]);
            } 
            else
            {
                List<bool[]> newLines;
                var count = lines.Count(x => x[i]);
                if (count >= ((double) lines.Count() / 2))
                {
                    newLines = lines.Where(x => largerOne(x[i])).ToList();
                } 
                else
                {
                    newLines = lines.Where(x => !largerOne(x[i])).ToList();
                }
                return CalcLevel(newLines, largerOne, i + 1);
            }
        }

        private int CalculateNumber(bool[] number)
        {
            var actualNumber = 0;
            for (var i = 0; i < number.Count(); i++)
            {
                actualNumber *= 2;
                if (number[i])
                {
                    actualNumber += 1;
                }
            }
            return actualNumber;
        }
    }
}