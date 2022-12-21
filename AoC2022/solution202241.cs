namespace AoC2022
{
    public class Solution202241 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution21.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var dict = new Dictionary<string, (string, string, Func<long, long, long>)>();
            var lst = new Dictionary<string, long>();
            foreach (var line in lines)
            {
                var splt = line.Trim().Split(":");
                var name = splt[0];
                if (long.TryParse(splt[1].Trim(), out var value))
                {
                    lst.Add(name, value);
                }
                else
                {
                    var op = splt[1].Trim().Split(" ");
                    if (op[1] == "+")
                    {
                        dict.Add(name, (op[0], op[2], Plus));
                    }
                    else if (op[1] == "-")
                    {
                        dict.Add(name, (op[0], op[2], Minus));
                    }
                    else if (op[1] == "*")
                    {
                        dict.Add(name, (op[0], op[2], Multi));
                    }
                    else if (op[1] == "/")
                    {
                        dict.Add(name, (op[0], op[2], Div));
                    }
                }
            }
            while(dict.Any() && !lst.ContainsKey("root"))
            {
                var m = new List<string>();
                foreach(var k in dict)
                {
                    if (lst.TryGetValue(k.Value.Item1, out long v1) && lst.TryGetValue(k.Value.Item2, out long v2))
                    {
                        lst.Add(k.Key, k.Value.Item3(v1, v2));
                        m.Add(k.Key);
                    }
                }
                foreach(var t in m)
                {
                    dict.Remove(t);
                }
            }
            return lst["root"].ToString();
        }

        private long Plus(long x, long y)
        {
            return x + y;
        }

        private long Minus(long x, long y)
        {
            return x - y;
        }

        private long Multi(long x, long y)
        {
            return x * y;
        }

        private long Div(long x, long y)
        {
            return x / y;
        }
    }
}