using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace AoC2022
{
    public class Solution202338 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution19.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var rules = new Dictionary<string, List<(string, string)>>();
            foreach(var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                var ruleName = line.Split("{")[0];
                var rulesOfLine = line.Trim().Replace("}", string.Empty).Split("{")[1].Split(",");
                var lst = new List<(string, string)>();
                foreach (var rule in rulesOfLine)
                {
                    Func<xmas, bool> xmasFunc = null;
                    ParameterExpression parameter = Expression.Parameter(typeof(xmas), "xmas");
                    if (rule.Contains(":"))
                    {
                        var spl = rule.Split(":");
                        var end = spl[1];
                        var actrule = spl[0];
                        lst.Add((actrule, end));
                    }
                    else
                    {
                        lst.Add((string.Empty, rule));
                    }
                }
                rules.Add(ruleName, lst);
            }
            long sum = Start("in", rules, new List<string>());
            return sum.ToString();
        }

        private long Start(string ruleName, Dictionary<string, List<(string, string)>> rules, List<string> rulesApplied)
        {
            var rule = rules[ruleName];
            long sum = 0;
            foreach((var cond, var end) in rule)
            {
                if (end == "A")
                {
                    var all = rulesApplied.ToList();
                    all.Add(cond);
                    sum += Calc(all);
                }
                else if (end != "R")
                {
                    var all = rulesApplied.ToList();
                    all.Add(cond);
                    sum += Start(end, rules, all);
                }
                rulesApplied.Add(cond.Contains("<") ? cond.Replace("<", ">=") : cond.Replace(">", "<="));
            }
            return sum;
        }

        private long Calc(List<string> rulesApplied)
        {
            long xmin = 1;
            long xmax = 4000;
            long mmin = 1;
            long mmax = 4000;
            long amin = 1;
            long amax = 4000;
            long smin = 1;
            long smax = 4000;
            foreach(var rule in rulesApplied)
            {
                if (string.IsNullOrEmpty(rule)) continue;
                if (rule.StartsWith("x"))
                {
                    if (rule.Contains("<="))
                    {
                        xmax = Math.Min(xmax, int.Parse(rule.Split("<=")[1]));
                    }
                    else if (rule.Contains("<"))
                    {
                        xmax = Math.Min(xmax, int.Parse(rule.Split("<")[1]) - 1);
                    }
                    if (rule.Contains(">="))
                    {
                        xmin = Math.Max(xmin, int.Parse(rule.Split(">=")[1]));
                    }
                    else if (rule.Contains(">"))
                    {
                        xmin = Math.Max(xmin, int.Parse(rule.Split(">")[1]) + 1);
                    }
                }
                if (rule.StartsWith("m"))
                {
                    if (rule.Contains("<="))
                    {
                        mmax = Math.Min(mmax, int.Parse(rule.Split("<=")[1]));
                    }
                    else if (rule.Contains("<"))
                    {
                        mmax = Math.Min(mmax, int.Parse(rule.Split("<")[1]) - 1);
                    }
                    if (rule.Contains(">="))
                    {
                        mmin = Math.Max(mmin, int.Parse(rule.Split(">=")[1]));
                    }
                    else if (rule.Contains(">"))
                    {
                        mmin = Math.Max(mmin, int.Parse(rule.Split(">")[1]) + 1);
                    }
                }
                if (rule.StartsWith("a"))
                {
                    if (rule.Contains("<="))
                    {
                        amax = Math.Min(amax, int.Parse(rule.Split("<=")[1]));
                    }
                    else if (rule.Contains("<"))
                    {
                        amax = Math.Min(amax, int.Parse(rule.Split("<")[1]) - 1);
                    }
                    if (rule.Contains(">="))
                    {
                        amin = Math.Max(amin, int.Parse(rule.Split(">=")[1]));
                    }
                    else if (rule.Contains(">"))
                    {
                        amin = Math.Max(amin, int.Parse(rule.Split(">")[1]) + 1);
                    }
                }
                if (rule.StartsWith("s"))
                {
                    if (rule.Contains("<="))
                    {
                        smax = Math.Min(smax, int.Parse(rule.Split("<=")[1]));
                    }
                    else if (rule.Contains("<"))
                    {
                        smax = Math.Min(smax, int.Parse(rule.Split("<")[1]) - 1);
                    }
                    if (rule.Contains(">="))
                    {
                        smin = Math.Max(smin, int.Parse(rule.Split(">=")[1]));
                    }
                    else if (rule.Contains(">"))
                    {
                        smin = Math.Max(smin, int.Parse(rule.Split(">")[1]) + 1);
                    }
                }
            }
            return (xmax - xmin + 1) * (mmax - mmin + 1) * (amax - amin + 1) * (smax - smin + 1);
        }

        private class xmas
        {
            public int x { get; set; }
            public int m { get; set; }
            public int a { get; set; }
            public int s { get; set; }
        }
    }
}