namespace AoC2022
{
    public class Solution202318 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution9.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            foreach (var line in lines)
            {
                var seq = line.Trim().Split(' ').Select(x => int.Parse(x)).ToList();
                var lastValues = new List<int>
                {
                    seq.First()
                };
                while(!seq.All(x=> x == 0))
                {
                    var newSeq = new List<int>();
                    for(var i = 1; i < seq.Count(); i++)
                    {
                        newSeq.Add(seq[i] - seq[i - 1]);
                    }
                    seq = newSeq;
                    lastValues.Add(seq.First());
                }
                var temp = 0;
                for(var i = lastValues.Count()-2;i >= 0;i--)
                {
                    temp = lastValues[i] - temp;
                }
                sum += temp;
            }
            return sum.ToString();
        }
    }
}