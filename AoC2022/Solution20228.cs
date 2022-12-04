namespace AoC2022
{
    public class Solution20228 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution4.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                var splt = lines[i].Split(",");
                var rng = new Range(int.Parse(splt[0].Split("-")[0]), int.Parse(splt[0].Split("-")[1]));
                var rng2 = new Range(int.Parse(splt[1].Split("-")[0]), int.Parse(splt[1].Split("-")[1]));
                if ((rng.Start.Value >= rng2.Start.Value && rng.End.Value <= rng2.End.Value) || (rng2.Start.Value >= rng.Start.Value && rng2.End.Value <= rng.End.Value)) 
                { 
                    sum += 1; 
                }
                else if ((rng.Start.Value >= rng2.Start.Value && rng2.End.Value >= rng.Start.Value) || (rng2.Start.Value >= rng.Start.Value && rng.End.Value >= rng2.Start.Value))
                {
                    sum += 1;
                }
            }
            
            return sum.ToString();
        }
    }
}