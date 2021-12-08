namespace AoC2022
{
    public class Solution16 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution8.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            foreach (var line in lines)
            {
                var splitted = line.Trim().Split("|");
                var first = splitted[0].Trim().Split(" ").ToList();
                //a bit brute force but ok, a more elegant solution is probably possible
                var one = first.First(x => x.Length == 2);
                var seven = first.First(x => x.Length == 3);
                var above = seven.First(x => !one.Contains(x));
                var four = first.First(x => x.Length == 4);
                var nine = first.First(x => x.Length == 6 && seven.All(y => x.Contains(y)) && four.All(y => x.Contains(y)));
                var eight = first.First(x => x.Length == 7);
                var below = nine.First(x => !seven.Contains(x) && !four.Contains(x));
                var three = first.First(x => x.Length == 5 && one.All(y => x.Contains(y)));
                var middle = three.First(x => x != above && x != below && !one.Contains(x));
                var leftAbove = nine.First(x => !one.Contains(x) && x != middle && x != above && x != below);
                var five = first.First(x => x.Length == 5 && x.Contains(leftAbove));
                var rightBelow = five.First(x => x != middle && x != above && x != below && x != leftAbove);
                var rightAbove = one.First(x => x != rightBelow);
                var leftBelow = eight.First(x => x != middle && x != below && x != above && x != leftAbove && x != rightBelow && x != rightAbove);
                var second = splitted[1].Trim().Split(" ");
                var allData = new AllData { Above = above, Below = below, Middle = middle, RightAbove = rightAbove, RightBelow = rightBelow, LeftBelow = leftBelow, LeftAbove = leftAbove };
                var number = allData.Transform(second[0]) * 1000 + allData.Transform(second[1]) * 100 + allData.Transform(second[2]) * 10 + allData.Transform(second[3]);
                sum += number;
            }
            return sum.ToString();
        }

        private class AllData
        {
            public char Above { get; set; }
            public char Below { get; set; }
            public char Middle { get; set; }
            public char RightAbove { get; set; }
            public char LeftAbove { get; set; }
            public char RightBelow { get; set; }
            public char LeftBelow { get; set; }

            public int Transform(string str)
            {
                switch(str.Length)
                {
                    case 2: return 1;
                    case 3: return 7;
                    case 4: return 4;
                    case 7: return 8;
                    case 5:
                        if (str.Contains(LeftBelow)) return 2;
                        if (str.Contains(RightAbove)) return 3;
                        return 5;
                    case 6:
                        if (!str.Contains(Middle)) return 0;
                        if (!str.Contains(LeftBelow)) return 9;
                        return 6;
                }
                return 0;
            }
        }
    }
}