namespace AoC2022
{
    public class Solution202238 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution19.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 6; i <= lines.Length; i+=6)
            {
                var orecost = (int.Parse(lines[i+1].Trim().Split()[4]), 0, 0);
                var claycost = (int.Parse(lines[i+2].Trim().Split()[4]), 0, 0);
                var obscost = (int.Parse(lines[i+3].Trim().Split()[4]), int.Parse(lines[i+3].Trim().Split()[7]), 0);
                var geocost = (int.Parse(lines[i+4].Trim().Split()[4]), 0, int.Parse(lines[i+4].Trim().Split()[7]));

                Max = 0;
                FindMax(orecost, claycost, obscost, geocost, 1, 0, 0, 0, 0, 0, 0, 0, 0);
                sum *= Max;
            }
            return 17.ToString();
        }

        public int Max;

        private void FindMax((int, int, int) orecost, (int, int, int) claycost, (int, int, int) obscost, (int, int, int) geocost, int oreRobot, int clayRobot, int obsRobot, int geoRobot, int ore, int clay, int obs, int geo, int minutes)
        {
            if (Max - geo - geoRobot >= 1 && minutes == 31) return;
            if (Max - geo - geoRobot * 2 >= 1 && minutes == 30) return;
            if (Max - geo - geoRobot * 3 >= 3 && minutes == 29) return;
            if (Max - geo - geoRobot * 4 >= 6 && minutes == 28) return;
            if (Max - geo - geoRobot * 5 >= 10 && minutes == 27) return;
            if (Max - geo - geoRobot * 6 >= 15 && minutes == 26) return;
            if (Max - geo - geoRobot * 7 >= 21 && minutes == 25) return;
            if (Max - geo - geoRobot * 8 >= 28 && minutes == 24) return;
            if (Max - geo - geoRobot * 9 >= 36 && minutes == 23) return;
            if (Max - geo - geoRobot * 10 >= 45 && minutes == 22) return;
            if (Max - geo - geoRobot * 11 >= 55 && minutes == 21) return;
            if (minutes == 32)
            {
                if (geo > Max) Max = geo;
                return;
            }

            if (ore >= geocost.Item1 && clay >= geocost.Item2 && obs >= geocost.Item3)
            {
                FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot, obsRobot, geoRobot + 1, ore - geocost.Item1 + oreRobot, clay - geocost.Item2 + clayRobot, obs - geocost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
            }
            else
            {
                if (ore >= obscost.Item1 && clay >= obscost.Item2 && obs >= obscost.Item3)
                {
                    FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot, obsRobot + 1, geoRobot, ore - obscost.Item1 + oreRobot, clay - obscost.Item2 + clayRobot, obs - obscost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
                }
                if (ore >= claycost.Item1 && clay >= claycost.Item2 && obs >= claycost.Item3)
                {
                    FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot + 1, obsRobot, geoRobot, ore - claycost.Item1 + oreRobot, clay - claycost.Item2 + clayRobot, obs - claycost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
                }
                if (ore >= orecost.Item1 && clay >= orecost.Item2 && obs >= orecost.Item3)
                {
                    FindMax(orecost, claycost, obscost, geocost, oreRobot + 1, clayRobot, obsRobot, geoRobot, ore - orecost.Item1 + oreRobot, clay - orecost.Item2 + clayRobot, obs - orecost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
                }
                FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot, obsRobot, geoRobot, ore + oreRobot, clay + clayRobot, obs + obsRobot, geo + geoRobot, minutes + 1);
            }
        }
    }
}