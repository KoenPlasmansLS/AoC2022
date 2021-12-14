namespace AoC2022
{
    public class Solution27 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution14.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines, 10);
        }

        public static string BaseAlgorithm(string[] lines, int nrSteps)
        {
            var polymer = lines[0].Trim();
            var rules = new Dictionary<string, string>();
            for(var i = 2; i < lines.Length; i++)
            {
                rules.Add(lines[i].Substring(0, 2), lines[i].Substring(6, 1));
            }
            var data = new Dictionary<string, long>();
            for(var j = 0; j < polymer.Length - 1; j++)
            {
                var str = new string(new[] { polymer[j], polymer[j + 1] });
                if (!data.ContainsKey(str))
                {
                    data.Add(str, 0);
                }
                data[str]++;
            }
            for (var j = 1; j <= nrSteps; j++)
            {
                data = ApplyPolymer(data, rules);
            }
            var sums = new Dictionary<char, long>();
            foreach(var entry in data)
            {
                if (!sums.ContainsKey(entry.Key[0])) sums.Add(entry.Key[0], 0);
                if (!sums.ContainsKey(entry.Key[1])) sums.Add(entry.Key[1], 0);

                sums[entry.Key[0]] += entry.Value;
                sums[entry.Key[1]] += entry.Value;
            }
            var most = sums.Max(x => x.Value);
            var least = sums.Min(x => x.Value);
            return ((most - least) / 2).ToString();
        }

        private static Dictionary<string, long> ApplyPolymer(Dictionary<string, long> nr, Dictionary<string, string> rules)
        {
            var data = new Dictionary<string, long>();
            foreach (var rule in rules)
            {
                if (nr.ContainsKey(rule.Key))
                {
                    var firstStr = new string(rule.Key[0].ToString().Concat(rule.Value).ToArray());
                    var secondStr = new string(rule.Value.Concat(rule.Key[1].ToString()).ToArray());
                    if (!data.ContainsKey(firstStr)) data.Add(firstStr, 0);
                    if (!data.ContainsKey(secondStr)) data.Add(secondStr, 0);

                    data[firstStr] += nr[rule.Key];
                    data[secondStr] += nr[rule.Key];
                }
            }
            return data;
        }
    }
}