using System.Collections.Immutable;
using Cache = System.Collections.Generic.Dictionary<string, long>;

namespace AoC2022
{
    public class Solution202324 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution12.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            foreach (var line in lines)
            {
                var split = line.Trim().Split(' ');
                var unknown = split[0];
                unknown = unknown + "?" + unknown + "?" + unknown + "?" + unknown + "?" + unknown;
                var lst = split[1].Split(",").Select(x => int.Parse(x)).ToList();
                var newlst = lst.ToList()!;
                newlst.AddRange(lst);
                newlst.AddRange(lst);
                newlst.AddRange(lst);
                newlst.AddRange(lst);
                newlst.Reverse();
                sum += GetUnknown(ImmutableStack.CreateRange(newlst), unknown, new Cache());
                //if (unknown.Length == newlst.Sum() + newlst.Count - 1)
                //{
                //    sum++;
                //}
                //else
                //{
                //    newlst.Reverse();
                //    sum += GetUnknown(ImmutableStack.CreateRange(newlst), unknown, new Cache());
                //}
            }
            return sum.ToString();
        }
        
        private static long GetUnknown(ImmutableStack<int> size, string unknown, Cache cache)
        {
            var key = unknown + "," + string.Join(",", size.Select(n => n.ToString()));
            if (!cache.ContainsKey(key))
            {
                cache[key] = Dispatch(unknown, size, cache);
            }
            return cache[key];
        }

        private static long Dispatch(string unknown, ImmutableStack<int> size, Cache cache)
        {
            var f = unknown.FirstOrDefault();
            switch(f)
            {
                case '.':
                    return GetUnknown(size, unknown[1..], cache);
                case '?':
                    return GetUnknown(size, "." + unknown[1..], cache) + GetUnknown(size, "#" + unknown[1..], cache);
                case '#':
                    if (!size.Any())
                    {
                        return 0;
                    }

                    var n = size.Peek();
                    size = size.Pop();

                    var potentiallyDead = unknown.TakeWhile(s => s == '#' || s == '?').Count();

                    if (potentiallyDead < n)
                    {
                        return 0;
                    }
                    else if (unknown.Length == n)
                    {
                        return GetUnknown(size, "", cache);
                    }
                    else if (unknown[n] == '#')
                    {
                        return 0; // dead spring follows the range -> not good
                    }
                    else
                    {
                        return GetUnknown(size, unknown[(n + 1)..], cache);
                    }
                default:
                    return size.Any() ? 0 : 1;
            }
        }
    }
}