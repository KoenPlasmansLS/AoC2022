namespace AoC2022
{
    public class Solution202212 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution6.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var line = lines[0];
            var i = 14;
            while(true)
            {
                var sb = line[(i - 14)..i];
                var found = false;
                for(var j = 0; j < sb.Length; j++)
                {
                    for (var k = j+1; k < sb.Length; k++)
                    {
                        if (sb[j] == sb[k])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    break;
                }
                i++;
            }
            return i.ToString();
        }
    }
}