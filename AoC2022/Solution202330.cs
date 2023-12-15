namespace AoC2022
{
    public class Solution202330 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution15.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            var dict = new Dictionary<int, List<(string, int)>>();
            for(var i  = 0; i < 256; i++)
            {
                dict.Add(i, new List<(string, int)>());
            }
            foreach(var splt in lines[0].Trim().Split(","))
            {
                if (splt.Contains('='))
                {
                    var left = splt.Split("=")[0];
                    var right = int.Parse(splt.Split("=")[1]);
                    var hash = Hash(left);
                    var lst = dict[hash];
                    var lens = lst.FirstOrDefault(x => x.Item1 == left);
                    if (lens.Item1 != null)
                    {
                        var i = lst.IndexOf(lens);
                        lst.RemoveAt(i);
                        lens.Item2 = right;
                        lst.Insert(i, lens);
                    }
                    else
                    {
                        lst.Add((left, right));
                    }
                } 
                else
                {
                    var left = splt.Split("-")[0];
                    var hash = Hash(left);
                    var lst = dict[hash];
                    var lens = lst.FirstOrDefault(x => x.Item1 == left);
                    if (lens.Item1 != null)
                    {
                        lst.Remove(lens);
                    }
                }
            }
            var k = 1;
            foreach(var box in dict.Values)
            {
                var j = 1;
                foreach(var lens in box)
                {
                    sum += k * j *lens.Item2;
                    j++;
                }
                k++;
            }
            return sum.ToString();
        }

        private static int Hash(string s)
        {
            var hash = 0;
            foreach (var ch in s)
            {
                hash += ch;
                hash *= 17;
                hash %= 256;
            }
            return hash;
        }
    }
}