namespace AoC2022
{
    public class Solution14 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution7.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var lst = lines[0].Split(",").Select(x => int.Parse(x)).ToList();
            lst.Sort();
            var index = lst[lst.Count / 2];

            var sum = CalcSum(lst, index);
            var sumLeft = CalcSum(lst, index - 1);
            var sumRight = CalcSum(lst, index + 1);
            if (sumLeft < sum)
            {
                long sumNext = sumLeft;
                int diff = 2;
                while(sumNext < sum)
                {
                    sum = sumNext;
                    sumNext = CalcSum(lst, index - diff);
                    diff++;
                }
            } 
            else
            {
                long sumNext = sumRight;
                int diff = 2;
                while (sumNext < sum)
                {
                    sum = sumNext;
                    sumNext = CalcSum(lst, index + diff);
                    diff++;
                }
            }
            return sum.ToString();
        }

        private long CalcSum(List<int> lst, int index)
        {
            var sum = 0;
            foreach (var i in lst)
            {
                var diff = Math.Abs(i - index);
                var diffFuel = diff * (diff + 1) / 2;
                sum += diffFuel;
            }
            return sum;
        }
    }
}