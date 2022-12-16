namespace AoC2022
{
    public class Solution202231 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution16.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var locs = new Dictionary<string, LocationValve>();
            foreach (var line in lines)
            {
                var splt = line.Trim().Split();
                var name = splt[1];
                var rate = int.Parse(splt[4].Split("=")[1].Replace(";", ""));
                var leads = new List<string>();
                for(var i = 9; i < splt.Length; i++)
                {
                    leads.Add(splt[i].Replace(",", ""));
                }
                locs.Add(name, new LocationValve { Name = name, Valve = rate, LeadsTo = leads });
            }
            foreach(var loc in locs.Values)
            {
                foreach(var loc2 in locs.Values)
                {
                    if (loc != loc2 && !loc.DistanceTo.ContainsKey(loc2.Name))
                    {
                        loc.DistanceTo[loc2.Name] = FindDistance(locs, new HashSet<LocationValve> { loc }, 0, loc2);
                        loc2.DistanceTo[loc.Name] = loc.DistanceTo[loc2.Name];
                    }
                }
            }
            var beginLoc = locs["AA"];
            var minutesLeft = 30;
            var opened = new List<string>();
            var sum = MaxFind(locs, beginLoc, opened, minutesLeft);            
            return sum.ToString();
        }

        private static int MaxFind(Dictionary<string, LocationValve> locs, LocationValve beginLoc, List<string> opened, int minutesLeft)
        {
            var max = 0;
            foreach(var loc in locs.Values.Where(x => x.Valve > 0 && !opened.Contains(x.Name) && x != beginLoc))
            {
                var minLeft = (minutesLeft - beginLoc.DistanceTo[loc.Name] - 1);
                if (minLeft > 0)
                {
                    var copy = opened.ToList();
                    copy.Add(loc.Name);
                    var curr = MaxFind(locs, loc, copy, minLeft) + minLeft * loc.Valve;
                    if (curr > max)
                    {
                        max = curr;
                    }
                }
            }
            return max;
        }

        //private static int CalculateValue(Dictionary<string, LocationValve> locs, LocationValve beginLoc, LocationValve loc, List<string> opened, int minutesLeft)
        //{
        //    var minutesLost = minutesLeft - beginLoc.DistanceTo[loc.Name] - 1;
        //    if (minutesLost > 0)
        //    {
        //        var distValue = minutesLost * loc.Valve;
        //        var lostValue = 0;
        //        foreach (var otherloc in locs.Values)
        //        {
        //            if (otherloc != loc && !opened.Contains(otherloc.Name) && otherloc.Valve > 0)
        //            {
        //                lostValue += otherloc.Valve * (loc.DistanceTo[otherloc.Name] - beginLoc.DistanceTo[otherloc.Name]);
        //            }
        //        }
        //        return distValue - lostValue;
        //    }
        //    return 0;
        //}

        private static int FindDistance(Dictionary<string, LocationValve> locs, HashSet<LocationValve> beginLocs, int dist, LocationValve endLoc)
        {
            var nextList = new HashSet<LocationValve>();
            foreach (var bLoc in beginLocs)
            {
                foreach (var locName in bLoc.LeadsTo)
                {
                    var otherLoc = locs[locName];
                    if (endLoc == otherLoc)
                    {
                        return dist + 1;
                    }
                    else
                    {
                        nextList.Add(otherLoc);
                    }
                }
            }
            return FindDistance(locs, nextList, dist + 1, endLoc);
        }
    }

    public class LocationValve
    {
        public string Name { get; set; }
        public int Valve { get; set; }
        public List<string> LeadsTo { get; set; }
        public Dictionary<string, int> DistanceTo { get; set; } = new Dictionary<string, int>();
    }
}