namespace AoC2022
{
    public class Solution202249 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution25.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            foreach(var line in lines)
            {
                sum += Translate(line.Trim());
            }
            return Translate(sum);
        }

        public static long Translate(string str)
        {
            long j = 0;
            for(var i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] == '1') j += (long) Math.Pow(5, str.Length - i - 1);
                if (str[i] == '2') j += 2* (long) Math.Pow(5, str.Length - i - 1);
                if (str[i] == '-') j -= (long)Math.Pow(5, str.Length - i - 1);
                if (str[i] == '=') j -= 2 * (long)Math.Pow(5, str.Length - i - 1);
            }
            return j;
        }

        public static string Translate(long i)
        {
            var str = "";
            while(i != 0)
            {
                var mod = i % 5;
                if (mod == 0)
                {
                    str += "0";
                }
                if (mod == 1)
                {
                    str += "1";
                    i -= 1;
                }
                if (mod == 2)
                {
                    str += "2";
                    i -= 2;
                }
                if (mod == 3)
                {
                    str += "=";
                    i += 2;
                }
                if (mod == 4)
                {
                    str += "-";
                    i += 1;
                }
                i /= 5;
            }
            return new string(str.Reverse().ToArray());
        }
    }
}