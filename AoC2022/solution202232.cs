namespace AoC2022
{
    public class Solution202232 : IProvideSolution
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
            var minutesLeft = 26;
            var opened = new List<string>();
            var sum = MaxFind(locs, beginLoc, beginLoc, opened, minutesLeft, minutesLeft);            
            return sum.ToString();
        }

        private static int MaxFind(Dictionary<string, LocationValve> locs, LocationValve beginLoc, LocationValve beginLoc2, List<string> opened, int minutesLeftA, int minutesLeftB)
        {
            var max = 0;
            if (minutesLeftA == minutesLeftB)
            {

                foreach (var loc in locs.Values.Where(x => x.Valve > 0 && !opened.Contains(x.Name) && x != beginLoc && x != beginLoc2))
                {
                    var minLeft = (minutesLeftA - beginLoc.DistanceTo[loc.Name] - 1);
                    if (minLeft > 0)
                    {
                        var copy = opened.ToList();
                        copy.Add(loc.Name);
                        var curr = MaxFind(locs, loc, beginLoc2, copy, minLeft, minutesLeftB) + minLeft * loc.Valve;
                        if (curr > max)
                        {
                            max = curr;
                        }
                        //var copy = opened.ToList();
                        //copy.Add(loc.Name);

                        //foreach (var loc2 in locs.Values.Where(x => x.Valve > 0 && !copy.Contains(x.Name) && x != beginLoc && x != beginLoc2))
                        //{
                        //    var minLeft2 = minutesLeftB - beginLoc2.DistanceTo[loc.Name] - 1;
                        //    if (minLeft2 > 0)
                        //    {
                        //        var copy2 = copy.ToList();
                        //        copy2.Add(loc2.Name);
                        //        var curr = MaxFind(locs, loc, loc2, copy, minLeft, minLeft2) + minLeft * loc.Valve + minLeft2 * loc2.Valve;
                        //        if (curr > max)
                        //        {
                        //            max = curr;
                        //        }
                        //    }
                        //}
                    }
                }
            }
            else if (Math.Max(minutesLeftA, minutesLeftB) == minutesLeftA)
            {
                foreach (var loc in locs.Values.Where(x => x.Valve > 0 && !opened.Contains(x.Name) && x != beginLoc && x != beginLoc2))
                {
                    var minLeft = minutesLeftA - beginLoc.DistanceTo[loc.Name] - 1;
                    if (minLeft > 0)
                    {
                        var copy = opened.ToList();
                        copy.Add(loc.Name);
                        var curr = MaxFind(locs, loc, beginLoc2, copy, minLeft, minutesLeftB) + minLeft * loc.Valve;
                        if (curr > max)
                        {
                            max = curr;
                        }
                    }
                }
            }
            else
            {
                foreach (var loc in locs.Values.Where(x => x.Valve > 0 && !opened.Contains(x.Name) && x != beginLoc && x != beginLoc2))
                {
                    var minLeft = minutesLeftB - beginLoc2.DistanceTo[loc.Name] - 1;
                    if (minLeft > 0)
                    {
                        var copy = opened.ToList();
                        copy.Add(loc.Name);
                        var curr = MaxFind(locs, beginLoc, loc, copy, minutesLeftA, minLeft) + minLeft * loc.Valve;
                        if (curr > max)
                        {
                            max = curr;
                        }
                    }
                }
            }
            return max;
        }

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
}