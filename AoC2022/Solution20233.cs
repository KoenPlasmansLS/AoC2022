namespace AoC2022
{
    public class Solution20233 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution2.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                sum+= ParseLine(lines, i);
            }
            return sum.ToString();
        }

        private static int ParseLine(string[] lines, int i)
        {
            var id = int.Parse(lines[i].Split(":")[0].Split(" ")[1]);
            var splits = lines[i].Split(":")[1].Split(";");
            foreach (var split in splits)
            {
                var splitIns = split.Split(",");
                foreach (var splitIn in splitIns)
                {
                    var str = splitIn.Trim();
                    var nr = int.Parse(str.Split(" ")[0]);
                    var color = str.Split(" ")[1];
                    if (color == "red" && nr > 12)
                    {
                        return 0;
                    }
                    if (color == "green" && nr > 13)
                    {
                        return 0;
                    }
                    if (color == "blue" && nr > 14)
                    {
                        return 0;
                    }
                }
            }
            return id;
        }
    }
}