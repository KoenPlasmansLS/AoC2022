using System.Linq.Expressions;

namespace AoC2022
{
    public class Solution202222 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution11.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            Monkey2 monkey = null;
            var monkeys = new List<Monkey2>();
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("Monkey"))
                {
                    monkey = new Monkey2 { Id = int.Parse(line.Trim().Split(" ")[1].Replace(":","")) };
                    monkeys.Add(monkey);
                }
                if (line.Trim().StartsWith("Starting items:"))
                {
                    var splt = line.Trim().Split(" ");
                    for (var i = 2; i < splt.Length; i++)
                    {
                        monkey.Worries.Add(long.Parse(splt[i].Replace(",", "")));
                    }
                }
                if (line.Trim().StartsWith("Operation:"))
                {
                    var splt = line.Trim().Split(" ");
                    var inp = Expression.Parameter(typeof(long));
                    Expression right = inp;
                    if (long.TryParse(splt[5], out var val))
                    {
                        right = Expression.Constant(val, typeof(long));
                    }
                    Expression expr = BinaryExpression.Add(inp, right);
                    if (splt[4] == "*")
                    {
                        expr = BinaryExpression.Multiply(inp, right);
                    }
                    var p = Expression.Lambda<Func<long, long>>(expr, new ParameterExpression[] { inp }).Compile();
                    monkey.Modifier = p;
                }
                if (line.Trim().StartsWith("Test:"))
                {
                    var splt = line.Trim().Split(" ");
                    monkey.ModuloTest = int.Parse(splt[3]);
                }
                if (line.Trim().StartsWith("If true"))
                {
                    var splt = line.Trim().Split(" ");
                    monkey.IdTrue = int.Parse(splt[5]);
                }
                if (line.Trim().StartsWith("If false"))
                {
                    var splt = line.Trim().Split(" ");
                    monkey.IdFalse = int.Parse(splt[5]);
                }
            }
            long supermod = 1;
            foreach (var monk in monkeys)
            {
                supermod *= monk.ModuloTest;
            }
            for (var k = 1; k<= 10000; k++)
            {
                foreach(var monk in monkeys)
                {
                    monk.DoRound(monkeys, supermod);
                }
            }
            var insp = monkeys.Select(x => x.Inspected).ToList();
            insp.Sort();
            insp.Reverse();
            
            return (insp[0] * insp[1]).ToString();
        }
    }

    public class Monkey2
    {
        public int Id { get; set; }
        public long Inspected { get; set; }
        public List<long> Worries { get; set; } = new List<long>();
        public Func<long, long> Modifier { get; set; }
        public int ModuloTest { get; set; }
        public int IdTrue { get; set; }
        public int IdFalse { get; set; }

        public void DoRound(List<Monkey2> monkeys, long supermod)
        {
            foreach(var item in Worries)
            {
                var newW = Modifier(item) % supermod;
                Inspected++;
                if(newW % ModuloTest == 0)
                {
                    monkeys.Single(x => x.Id == IdTrue).Worries.Add(newW);
                }
                else
                {
                    monkeys.Single(x => x.Id == IdFalse).Worries.Add(newW);
                }
            }
            Worries.Clear();
        }
    }
}