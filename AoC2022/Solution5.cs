namespace AoC2022
{
    public class Solution5 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution3.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var nr = lines.Count();
            var columns = lines[0].Count() - 1;
            var nrOnes = new int[columns];
            foreach (var line in lines)
            {
                for (var i = 0; i < columns; i++)
                {
                    if (line[i] == '1')
                    {
                        nrOnes[i]++;
                    }
                }
            }
            var gamma = 0;
            for (var i = 0; i < columns; i++)
            {
                gamma *= 2;
                if (nrOnes[i] > nr / 2)
                {
                    gamma += 1;
                }
            }
            var maxPow = Math.Pow(2, columns) - 1;
            return ((maxPow - gamma) * gamma).ToString();
        }
    }
}