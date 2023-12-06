namespace AoC2022
{
    public class Solution202311 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution6.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var times = lines[0].Split(":")[1].Trim().Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Split(" ");
            var records = lines[1].Split(":")[1].Trim().Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Split(" ");
            long sum = 1;
            for(var i = 0; i< times.Length;i++)
            {
                double time = int.Parse(times[i]);
                double record = int.Parse(records[i]);
                //(time -x) * x = record
                //time * x - x^2 - record = 0
                //x^2 - time*x + record = 0
                //+-Math.Sqrt(time*time - 4*record)
                var det = Math.Sqrt(time * time - 4 * record) / 2;
                var x1 = (int) Math.Floor(time / 2 + det);
                var x2 = (int) Math.Ceiling(time / 2 - det);
                if (x1 * (time - x1) == record)
                {
                    x1--;
                }
                if (x2 * (time - x2) == record)
                {
                    x2++;
                }
                var diff = x1 - x2 + 1;
                sum *= diff;
            }

            return sum.ToString();
        }
    }
}