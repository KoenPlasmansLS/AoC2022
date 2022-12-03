namespace AoC2022
{
    public class Solution20226 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution3.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                var lst = new List<int>();
                for (var k = 0; k < lines[i].Trim().Length; k++)
                {
                    lst.Add(Convert(lines[i].Trim()[k]));
                }
                i++;
                var lst2 = new List<int>();
                for (var k = 0; k < lines[i].Trim().Length; k++)
                {
                    var c = Convert(lines[i].Trim()[k]);
                    if (lst.Contains(c))
                    {
                        lst2.Add(c);
                    }
                }
                i++;
                var same = 0;
                for (var k = 0; k < lines[i].Trim().Length; k++)
                {
                    var c = Convert(lines[i].Trim()[k]);
                    if (lst2.Contains(c))
                    {
                        same = c;
                        break;
                    }
                }
                sum += same;
            }
            
            return sum.ToString();
        }

        private static int Convert(char c)
        {
            return c > 96 ? c - 96 : c - 38;
        }
    }
}