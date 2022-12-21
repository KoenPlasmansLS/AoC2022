using System.Linq.Expressions;

namespace AoC2022
{
    public class Solution202242 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution21.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var dict = new Dictionary<string, (string, string, string)>();
            var lst = new Dictionary<string, long>();
            foreach (var line in lines)
            {
                var splt = line.Trim().Split(":");
                var name = splt[0];
                if (long.TryParse(splt[1].Trim(), out var value))
                {
                    lst.Add(name, value);
                }
                else
                {
                    var op = splt[1].Trim().Split(" ");
                    dict.Add(name, (op[0], op[2], op[1]));
                }
            }
            var r = (((((((((((((((((((((73621039497353 - (((49160133593649 * 2) - 702) / 3)) * 3) + 423) / 2) - 372) * 4) + 988) / 2) - 424) / 52) + 704) * 4) - 667) - 427) * 2) - 314) / 3) + 116) / 2) - 122) * 9);
            var r2 = ((((((((((r + 275) * 4) - 8) / 2) + 150) / 3) - 498) * 3) + 228) / 2);
            var r3 = (((((((((((r2 + 23) * 2) - 10) / 32) + 724) * 9) - 242) * 7) - 673) / 9) + 712);
            var r4 = ((((((((((((((((((((r3 / 2) + 828) * 10) - 467) * 2) - 67) + 625) / 2) - 364) / 12) + 699) * 4) - 533) * 8) + 376) / 2) - 477) / 47) - 65) * 2) + 273;
            var leftI = dict["root"].Item1;
            var inp = Expression.Parameter(typeof(long));
            var xp = GetExpression(dict, lst, inp, leftI);
            long t = 73621039497353;
            //if (xp is BinaryExpression x)
            //{
            //    if (x.Left is ConstantExpression c)
            //    {
            //        if (x.Method)
            //    }
            //}
            return r4.ToString();
        }

        private Expression GetExpression(Dictionary<string, (string, string, string)> dict, Dictionary<string, long> lst, Expression inp, string name)
        {
            Expression left = null;
            Expression right = null;
            
            if (lst.TryGetValue(name, out long value))
            {
                return ConstantExpression.Constant(value);
            }
            var item = dict[name];
            if (item.Item1 == "humn")
            {
                left = inp;
            }
            else
            {
                left = GetExpression(dict, lst, inp, item.Item1);
            }
            if (item.Item2 == "humn")
            {
                right = inp;
            }
            else
            {
                right = GetExpression(dict, lst, inp, item.Item2);
            }
            if (item.Item3 == "+")
            {
                if (left is ConstantExpression lc && right is ConstantExpression rc)
                {
                    return ConstantExpression.Constant((long)lc.Value + (long)rc.Value);
                }
                return BinaryExpression.Add(left, right);
            }
            if (item.Item3 == "-")
            {
                if (left is ConstantExpression lc && right is ConstantExpression rc)
                {
                    return ConstantExpression.Constant((long)lc.Value - (long)rc.Value);
                }
                return BinaryExpression.Subtract(left, right);
            }
            if (item.Item3 == "/")
            {
                if (left is ConstantExpression lc && right is ConstantExpression rc)
                {
                    return ConstantExpression.Constant((long)lc.Value / (long)rc.Value);
                }
                return BinaryExpression.Divide(left, right);
            }
            if (item.Item3 == "*")
            {
                if (left is ConstantExpression lc && right is ConstantExpression rc)
                {
                    return ConstantExpression.Constant((long)lc.Value * (long)rc.Value);
                }
                return BinaryExpression.Multiply(left, right);
            }
            return null;
        }

        private long Plus(long x, long y)
        {
            return x + y;
        }

        private long Minus(long x, long y)
        {
            return x - y;
        }

        private long Multi(long x, long y)
        {
            return x * y;
        }

        private long Div(long x, long y)
        {
            return x / y;
        }
    }
}