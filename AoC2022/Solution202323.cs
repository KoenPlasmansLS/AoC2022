namespace AoC2022
{
    public class Solution202323 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution12.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            foreach (var line in lines)
            {
                var split = line.Trim().Split(' ');
                var unknown = split[0];
                var lst = split[1].Split(",").Select(x => int.Parse(x)).ToList();
                if (unknown.Length == lst.Sum() + lst.Count - 1)
                {
                    Console.WriteLine(unknown);
                    sum++;
                }
                else
                {
                    sum += GetUnknown(lst, unknown, 0);
                }
                Console.WriteLine("---------------------------");
            }
            return sum.ToString();
        }
        
        private static int GetUnknown(List<int> size, string unknown, int index)
        {
            if (unknown[index] == '?')
            {
                var sum = 0;
                var newstr = unknown.ToCharArray();
                newstr[index] = '.';
                if (Check(size, newstr))
                {
                    if (newstr.Contains('?'))
                    {
                        sum += GetUnknown(size, new string(newstr), index + 1);
                    }
                    else
                    {
                        Console.WriteLine(new string(newstr));
                        sum++;
                    }
                }
                newstr[index] = '#';
                if (Check(size, newstr))
                {
                    if (newstr.Contains('?'))
                    {
                        sum += GetUnknown(size, new string(newstr), index + 1);
                    }
                    else
                    {
                        Console.WriteLine(new string(newstr));
                        sum++;
                    }
                }
                return sum;
            }
            else
            {
                return GetUnknown(size, unknown, index + 1);
            }
        }

        private static bool Check(List<int> size, char[] arr)
        {
            if (arr.Count(x => x == '#') > size.Sum()) return false;
            if (arr.Count(x => x == '#' ||x == '?') < size.Sum()) return false;

            int i = 0;
            int seq = 0;
            int nr = 0;
            while (i < arr.Length && arr[i]!='?')
            {
                if (arr[i] == '#')
                {
                    seq++;
                }
                else if (seq > 0)
                {
                    if (seq != size[nr]) return false;
                    nr++;
                    seq = 0;
                }
                i++;
            }
            if (seq > 0 && seq > size[nr]) return false;
            return true;
        }
    }
}