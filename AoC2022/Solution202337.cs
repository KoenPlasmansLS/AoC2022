using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace AoC2022
{
    public class Solution202337 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution19.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var parts = new List<xmas>();
            var rules = new Dictionary<string, List<(Func<xmas, bool>, string)>>();
            var nowtheparts = false;
            foreach(var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    nowtheparts |= true;
                    continue;
                }

                if (nowtheparts)
                {
                    var kline = line.Replace("{", string.Empty).Replace("}", string.Empty).Trim();
                    var split = kline.Split(',');
                    var xmas = new xmas { x = int.Parse(split[0].Substring(2)), m = int.Parse(split[1].Substring(2)), a = int.Parse(split[2].Substring(2)), s = int.Parse(split[3].Substring(2)) };
                    parts.Add(xmas);
                } 
                else
                {
                    var ruleName = line.Split("{")[0];
                    var rulesOfLine = line.Trim().Replace("}", string.Empty).Split("{")[1].Split(",");
                    var lst = new List<(Func<xmas, bool>, string)>();
                    foreach(var rule in rulesOfLine)
                    {
                        Func<xmas, bool> xmasFunc = null;
                        ParameterExpression parameter = Expression.Parameter(typeof(xmas), "xmas");
                        if (rule.Contains(":"))
                        {
                            var spl = rule.Split(":");
                            var end = spl[1];
                            var actrule = spl[0];  
                            if (actrule.Contains("<"))
                            {
                                var val = actrule.Split("<")[0];
                                var value = int.Parse(actrule.Split("<")[1]);
                                var member = Expression.PropertyOrField(parameter, val);
                                var body = Expression.LessThan(member, Expression.Constant(value, typeof(int)));
                                xmasFunc = Expression.Lambda<Func<xmas, bool>>(body, parameter).Compile();
                            }
                            else if (actrule.Contains(">"))
                            {
                                var val = actrule.Split(">")[0];
                                var value = int.Parse(actrule.Split(">")[1]);
                                var member = Expression.PropertyOrField(parameter, val);
                                var body = Expression.GreaterThan(member, Expression.Constant(value, typeof(int)));
                                xmasFunc = Expression.Lambda<Func<xmas, bool>>(body, parameter).Compile();
                            }
                            lst.Add((xmasFunc, end));
                        }
                        else
                        {
                            var body = Expression.Constant(true, typeof(bool));
                            xmasFunc = Expression.Lambda<Func<xmas, bool>>(body, parameter).Compile();
                            lst.Add((xmasFunc, rule));
                        }
                    }
                    rules.Add(ruleName, lst);
                }
            }
            long sum = 0;
            foreach(var part in parts)
            {
                sum += DoOne(rules, part, "in");
            }
            return sum.ToString();
        }

        private static long DoOne(Dictionary<string, List<(Func<xmas, bool>, string)>> rules, xmas part, string ruleName)
        {
            var rule = rules[ruleName];
            foreach ((Func<xmas, bool> exp, string end) in rule)
            {
                if (exp(part))
                {
                    if (end == "A")
                    {
                        return part.x + part.m + part.a + part.s;
                    }
                    else if (end == "R")
                    {
                        return 0;
                    }
                    else
                    {
                        return DoOne(rules, part, end);
                    }
                }
            }
            return 0;
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