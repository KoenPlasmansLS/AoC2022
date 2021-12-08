namespace AoC2022
{
    public class Solution13 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution7.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var lst = lines[0].Split(",").Select(x => int.Parse(x)).ToList();
            lst.Sort();
            var index = lst[lst.Count / 2];
            var sum = 0;
            foreach(var i in lst)
            {
                sum += Math.Abs(i - index);
            }
            return sum.ToString();
        }
    }
}