using System.Linq.Expressions;

namespace AoC2022
{
    public class Solution202221 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution11.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            Monkey monkey = null;
            var monkeys = new List<Monkey>();
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("Monkey"))
                {
                    monkey = new Monkey { Id = int.Parse(line.Trim().Split(" ")[1].Replace(":","")) };
                    monkeys.Add(monkey);
                }
                if (line.Trim().StartsWith("Starting items:"))
                {
                    var splt = line.Trim().Split(" ");
                    for (var i = 2; i < splt.Length; i++)
                    {
                        monkey.Worries.Add(int.Parse(splt[i].Replace(",", "")));
                    }
                }
                if (line.Trim().StartsWith("Operation:"))
                {
                    var splt = line.Trim().Split(" ");
                    var inp = Expression.Parameter(typeof(int));
                    Expression right = inp;
                    if (int.TryParse(splt[5], out var val))
                    {
                        right = Expression.Constant(val);
                    }
                    Expression expr = BinaryExpression.Add(inp, right);
                    if (splt[4] == "*")
                    {
                        expr = BinaryExpression.Multiply(inp, right);
                    }
                    var p = Expression.Lambda<Func<int, int>>(expr, new ParameterExpression[] { inp }).Compile();
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
            for(var k = 1; k<= 20; k++)
            {
                foreach(var monk in monkeys)
                {
                    monk.DoRound(monkeys);
                }
                var t = "";
            }
            var insp = monkeys.Select(x => x.Inspected).ToList();
            insp.Sort();
            insp.Reverse();
            
            return (insp[0] * insp[1]).ToString();
        }
    }

    public class Monkey
    {
        public int Id { get; set; }
        public int Inspected { get; set; }
        public List<int> Worries { get; set; } = new List<int>();
        public Func<int, int> Modifier { get; set; }
        public int ModuloTest { get; set; }
        public int IdTrue { get; set; }
        public int IdFalse { get; set; }

        public void DoRound(List<Monkey> monkeys)
        {
            foreach(var item in Worries)
            {
                var newW = Modifier(item);
                newW = newW / 3;
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