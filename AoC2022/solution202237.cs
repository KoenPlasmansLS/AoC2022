namespace AoC2022
{
    public class Solution202237 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution19.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 0; i <= lines.Count(); i+=6)
            {
                var orecost = (int.Parse(lines[i+1].Trim().Split()[4]), 0, 0);
                var claycost = (int.Parse(lines[i+2].Trim().Split()[4]), 0, 0);
                var obscost = (int.Parse(lines[i+3].Trim().Split()[4]), int.Parse(lines[i+3].Trim().Split()[7]), 0);
                var geocost = (int.Parse(lines[i+4].Trim().Split()[4]), 0, int.Parse(lines[i+4].Trim().Split()[7]));

                found = new();
                var max = FindMax(orecost, claycost, obscost, geocost, 1, 0, 0, 0, 0, 0, 0, 0, 0);
                sum += ((i / 6) + 1) * max;
            }
            return sum.ToString();
        }

        public HashSet<(int, int, int, int, int, int, int, int)> found = new();

        private int FindMax((int, int, int) orecost, (int, int, int) claycost, (int, int, int) obscost, (int, int, int) geocost, int oreRobot, int clayRobot, int obsRobot, int geoRobot, int ore, int clay, int obs, int geo, int minutes)
        {
            if (found.Contains((ore, clay, obs, oreRobot, clayRobot, obsRobot, geoRobot, minutes)))
            {
                return 0;
            }
            else
            {
                found.Add((ore, clay, obs, oreRobot, clayRobot, obsRobot, geoRobot, minutes));
            }
            if (minutes == 24) return geo;

            var max = 0;
            if (ore >= geocost.Item1 && clay >= geocost.Item2 && obs >= geocost.Item3)
            {
                var max5 = FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot, obsRobot, geoRobot + 1, ore - geocost.Item1 + oreRobot, clay - geocost.Item2 + clayRobot, obs - geocost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
                if (max5 > max) { max = max5; }
            }
            else
            {
                if (ore >= obscost.Item1 && clay >= obscost.Item2 && obs >= obscost.Item3)
                {
                    var max4 = FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot, obsRobot + 1, geoRobot, ore - obscost.Item1 + oreRobot, clay - obscost.Item2 + clayRobot, obs - obscost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
                    if (max4 > max) { max = max4; }
                }
                if (ore >= claycost.Item1 && clay >= claycost.Item2 && obs >= claycost.Item3)
                {
                    var max3 = FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot + 1, obsRobot, geoRobot, ore - claycost.Item1 + oreRobot, clay - claycost.Item2 + clayRobot, obs - claycost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
                    if (max3 > max) { max = max3; }
                }
                if (ore >= orecost.Item1 && clay >= orecost.Item2 && obs >= orecost.Item3)
                {
                    var max6 = FindMax(orecost, claycost, obscost, geocost, oreRobot + 1, clayRobot, obsRobot, geoRobot, ore - orecost.Item1 + oreRobot, clay - orecost.Item2 + clayRobot, obs - orecost.Item3 + obsRobot, geo + geoRobot, minutes + 1);
                    if (max6 > max) { max = max6; }
                }
                var max2 = FindMax(orecost, claycost, obscost, geocost, oreRobot, clayRobot, obsRobot, geoRobot, ore + oreRobot, clay + clayRobot, obs + obsRobot, geo + geoRobot, minutes + 1);
                if (max2 > max) { max = max2; }
            }
            return max;
        }
    }
}