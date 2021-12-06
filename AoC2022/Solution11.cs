using System.Numerics;

namespace AoC2022
{
    public class Solution11 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/solution6.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines, int days = 80)
        {
            var ages = new BigInteger[9];
            foreach(var age in lines[0].Split(","))
            {
                var index = int.Parse(age);
                ages[index]++;
            }
            for(var i = 1; i<= days; i++)
            {
                var newAges = new BigInteger[9];
                var comingOfAge = ages[0];
                for(var j = 1; j<=8;j++)
                {
                    newAges[j - 1] = ages[j];
                }
                newAges[6] += comingOfAge;
                newAges[8] = comingOfAge;
                ages = newAges;
            }
            BigInteger toReturn = 0;
            foreach(var age in ages)
            {
                toReturn += age;
            }
            return toReturn.ToString();
        }
    }
}