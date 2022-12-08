namespace AoC2022
{
    public class Solution202211 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution6.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var line = lines[0];
            var i =4;
            while(true)
            {
                var sb = line[(i - 4)..i];
                if (sb[0] != sb[1] && sb[0] != sb[2] && sb[1] != sb[2] && sb[0] != sb[3] && sb[1] != sb[3] && sb[2] != sb[3])
                {
                    break;
                }
                i++;
            }
            return i.ToString();
        }
    }
}