namespace AoC2022
{
    public class Solution20234 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution2.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                sum+= ParseLine(lines, i);
            }
            return sum.ToString();
        }

        private static int ParseLine(string[] lines, int i)
        {
            var nrBlue = 0;
            var nrGreen = 0;
            var nrRed = 0;
            var splits = lines[i].Split(":")[1].Split(";");
            foreach (var split in splits)
            {
                var splitIns = split.Split(",");
                foreach (var splitIn in splitIns)
                {
                    var str = splitIn.Trim();
                    var nr = int.Parse(str.Split(" ")[0]);
                    var color = str.Split(" ")[1];
                    if (color == "red" && nrRed < nr)
                    {
                        nrRed = nr;
                    }
                    if (color == "green" && nrGreen < nr)
                    {
                        nrGreen = nr;
                    }
                    if (color == "blue" && nrBlue < nr)
                    {
                        nrBlue = nr;
                    }
                }
            }
            return nrBlue * nrGreen * nrRed;
        }
    }
}