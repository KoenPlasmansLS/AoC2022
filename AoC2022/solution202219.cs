namespace AoC2022
{
    public class Solution202219 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution10.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var cycle = 1;
            var value = 1;
            var sum = 0;
            foreach (var line in lines)
            {
                if (line.Trim() == "noop")
                {
                    sum += CheckSum(cycle, value);
                    cycle++;
                }
                else
                {
                    sum+= CheckSum(cycle, value);
                    cycle++;
                    sum+= CheckSum(cycle, value);
                    cycle++;
                    value += int.Parse(line.Trim().Split(" ")[1]);
                }
            }
            sum+= CheckSum(cycle, value);
            return sum.ToString();
        }

        private static int CheckSum(int cycle, int value)
        {
            if (cycle > 0 && (cycle+20)%40==0 && cycle <= 220)
            {
                return cycle * value;
            }
            return 0;
        }
    }
}