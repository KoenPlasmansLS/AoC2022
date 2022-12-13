using System.Numerics;

namespace AoC2022
{
    public class MyComparison : IComparer<CompList>
    {
        public int Compare(CompList x, CompList y)
        {
            var l = Solution202226.Compare(x, y);
            if (l == true) return -1;
            if (l == false) return 1;
            return 0;
        }
    }

    public class Solution202226 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution13.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            var lst = new List<CompList>();
            var p1 = Parse("[[2]]");
            var p2 = Parse("[[6]]");
            lst.Add(p1);
            lst.Add(p2);
            for (var i = 0; i < lines.Length; i += 3)
            {
                var ll = Parse(lines[i].Trim());
                var rl = Parse(lines[i + 1].Trim());
                lst.Add(ll);
                lst.Add(rl);
            }
            var m = new MyComparison();
            lst.Sort(m);
            var x1 = lst.IndexOf(p1) + 1;
            var x2 = lst.IndexOf(p2) + 1;
            return (x1 * x2).ToString();
        }

        public static bool? Compare(CompList ll, CompList rl)
        {
            var i = 0;
            while(true)
            {
                if (i < ll.List.Count && i >= rl.List.Count())
                {
                    return false;
                }
                else if (i >= ll.List.Count && i < rl.List.Count())
                {
                    return true;
                }
                else if (i >= ll.List.Count && i >= rl.List.Count())
                {
                    return null;
                }
                if (ll.List[i] is CompValue && rl.List[i] is CompValue)
                {
                    var res = Compare(ll.List[i] as CompValue, rl.List[i] as CompValue);
                    if (res == true) return true;
                    if (res == false) return false;
                }
                if (ll.List[i] is CompList && rl.List[i] is CompValue)
                {
                    var ri = new CompList();
                    ri.List.Add(rl.List[i]);
                    var res = Compare(ll.List[i] as CompList, ri);
                    if (res == true) return true;
                    if (res == false) return false;
                }
                if (ll.List[i] is CompValue && rl.List[i] is CompList)
                {
                    var li = new CompList();
                    li.List.Add(ll.List[i]);
                    var res = Compare(li, rl.List[i] as CompList);
                    if (res == true) return true;
                    if (res == false) return false;
                }
                if (ll.List[i] is CompList && rl.List[i] is CompList)
                {
                    var res = Compare(ll.List[i] as CompList, rl.List[i] as CompList);
                    if (res == true) return true;
                    if (res == false) return false;
                }
                i++;
            }
        }

        public static bool? Compare(CompValue ll, CompValue rl)
        {
            if (ll.Value < rl.Value) return true;
            if (ll.Value > rl.Value) return false;
            return null;
        }

        public static CompList Parse(string line)
        {
            CompList current = null;
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == '[')
                {
                    if (current == null)
                    {
                        current = new CompList();
                    }
                    else
                    {
                        var lst = new CompList();
                        lst.Parent = current;
                        current.List.Add(lst);
                        current = lst;
                    }
                }
                if (line[j] == ']')
                {
                    var par = current.Parent;
                    if (par != null) current = par;
                }
                if (int.TryParse(line[j].ToString(), out var val))
                {
                    if (int.TryParse(line[j..(j+2)], out var val2))
                    {
                        current.List.Add(new CompValue { Value = val2 });
                        j++;
                    }
                    else
                    {
                        current.List.Add(new CompValue { Value = val });
                    }
                }
            }
            return current;
        }
    }
}