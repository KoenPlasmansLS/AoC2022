namespace AoC2022
{
    public class Solution20238 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution4.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var nr = new int[lines.Length];
            for (var i = 0; i < lines.Count(); i++)
            {
                nr[i]+=1;
                var split = lines[i].Split(":")[1].Split("|");
                var left = split[0].Trim();
                var right = split[1].Trim();
                var winning = left.Replace("  ", " ").Split(" ").Select(x => int.Parse(x));
                var have = right.Replace("  ", " ").Split(" ").Select(x => int.Parse(x));
                var nrWinnings = 0;
                foreach(var ihave in have)
                {
                    if (winning.Contains(ihave))
                    {
                        nrWinnings++;
                    }
                }
                for(var k = 1; k <= nrWinnings; k++)
                {
                    nr[i + k] += nr[i];
                }
            }
            return nr.Sum().ToString();
        }
    }
}