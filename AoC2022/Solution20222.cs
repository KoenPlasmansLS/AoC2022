namespace AoC2022
{
    public class Solution20222 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution1.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            var elves = new List<int>();

            for (var i = 1; i < lines.Count(); i++)
            {
                if (string.IsNullOrEmpty(lines[i].Trim()))
                {
                    elves.Add(sum);
                    sum = 0;
                } 
                else 
                {
                    sum += int.Parse(lines[i]);
                }
            }
            elves.Sort();
            elves.Reverse();
            return (elves[0] + elves[1] + elves[2]).ToString();
        }
    }
}