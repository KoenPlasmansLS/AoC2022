namespace AoC2022
{
    public class Solution20225 : IProvideSolution
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
                var lngth = lines[i].Trim().Length;
                var lst = new List<int>();
                for(var k = 0; k < lngth/2;k++)
                {
                    lst.Add(Convert(lines[i].Trim()[k]));
                }
                var isFound = false;
                var j = 0;
                while(!isFound)
                {
                    var o = Convert(lines[i].Trim()[j + lngth/2]);
                    if (lst.Contains(o))
                    {
                        isFound = true;
                        sum += o;
                    }
                    j++;
                }
            }
            return sum.ToString();
        }

        private static int Convert(char c)
        {
            return c > 96 ? c - 96 : c - 38;
        }
    }
}