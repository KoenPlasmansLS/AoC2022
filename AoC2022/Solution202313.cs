namespace AoC2022
{
    public class Solution202313 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution7.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            long sum = 0;
            var hands = new List<(string, int)>();
            foreach ( var line in lines)
            {
                var split = line.Trim().Split(' ');
                hands.Add((split[0], int.Parse(split[1])));
            }
            hands.Sort(new PokerComparer());
            for(var i = 0; i < hands.Count; i++)
            {
                sum += (i +1) * hands[i].Item2;
            }
            return sum.ToString();
        }
    }

    public class PokerComparer : IComparer<(string, int)>
    {
        public int Compare((string, int) x, (string, int) y)
        {
            var leftRank = GetRank(x.Item1);
            var rightRank = GetRank(y.Item1);
            if (leftRank > rightRank)
            {
                return 1;
            }
            if (leftRank < rightRank)
            {
                return -1;
            }
            for(var i = 0; i < 5; i++)
            {
                var t = Trans(x.Item1[i]);
                var t2 = Trans(y.Item1[i]);
                if (t > t2)
                {
                    return 1;
                }
                if (t < t2)
                {
                    return -1;
                }
            }
            return 0;
        }

        public int Trans(char c)
        {
            if (c == 'A') return 13;
            if (c == 'K') return 12;
            if (c == 'Q') return 11;
            if (c == 'J') return 10;
            if (c == 'T') return 9;
            if (c == '9') return 8;
            if (c == '8') return 7;
            if (c == '7') return 6;
            if (c == '6') return 5;
            if (c == '5') return 4;
            if (c == '4') return 3;
            if (c == '3') return 2;
            return 1;
        }

        public int GetRank(string str)
        {
            var dict = new Dictionary<char, int>();
            for (var i = 0; i < str.Length; i++)
            {
                if (!dict.ContainsKey(str[i]))
                {
                    dict.Add(str[i], 0);
                }
                dict[str[i]]++;
            }
            if (dict.Count == 1)
            {
                return 7;
            }
            if (dict.Count == 2)
            {
                if (dict.First().Value == 4 || dict.First().Value == 1)
                {
                    return 6;
                }
                return 5;
            }
            if (dict.Count == 3)
            {
                if (dict.First().Value == 3 || dict.Skip(1).First().Value == 3 || dict.Last().Value == 3)
                {
                    return 4;
                }
                return 3;
            }
            if (dict.Count == 4)
            {
                return 2;
            }
            return 1;
        }
    }
}