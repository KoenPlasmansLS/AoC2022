namespace AoC2022
{
    public class Solution202240 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution20.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var lst = new List<(int, long)>();
            var i = 0;
            foreach (var line in lines)
            {
                lst.Add((i, long.Parse(line.Trim()) * 811589153));
                i++;
            }
            var copy = lst.ToList();
            var length = copy.Count - 1;
            for (var j = 0; j < 10; j++)
            {
                foreach ((var index, var nr) in lst)
                {
                    var oldindex = copy.IndexOf((index, nr));
                    copy.RemoveAt(oldindex);
                    var newindex = (int)((nr + oldindex) % length);
                    while (newindex < 0)
                    {
                        newindex += length;
                    }
                    copy.Insert(newindex, (index, nr));
                }
            }
            var zero = lst.First(i => i.Item2 == 0);
            var indexZero = copy.IndexOf(zero);
            var x = copy[(indexZero + 1000) % lst.Count];
            var y = copy[(indexZero + 2000) % lst.Count];
            var Z = copy[(indexZero + 3000) % lst.Count];
            return (x.Item2 + y.Item2 + Z.Item2).ToString();
        }
    }
}