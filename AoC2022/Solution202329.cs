namespace AoC2022
{
    public class Solution202329 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution15.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            foreach(var splt in lines[0].Trim().Split(","))
            {
                var hash = 0;
                foreach(var ch in splt)
                {
                    hash += ch;
                    hash *= 17;
                    hash %= 256;
                }
                sum += hash;
            }
            return sum.ToString();
        }
    }
}