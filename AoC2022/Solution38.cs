namespace AoC2022
{
    public class Solution38 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution19.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var beacons = new List<Beacon>();
            var currentBeacon = new Beacon();
            beacons.Add(currentBeacon);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line.Trim()))
                {
                    currentBeacon = new Beacon();
                    beacons.Add(currentBeacon);
                }
                else if (line.StartsWith("--")) { }
                else
                {
                    var split = line.Trim().Split(',');
                    currentBeacon.Points.Add(new Point3D { X = int.Parse(split[0]), Y = int.Parse(split[1]), Z = int.Parse(split[2]) });
                }
            }
            var constellation = beacons.First();
            var pointsToKeep = new List<Point3D>();
            pointsToKeep.Add(new Point3D { X = 0, Y = 0, Z = 0 });
            var beaconsToLoop = beacons.Skip(1).ToList();
            var hasNew = false;
            do
            {
                hasNew = false;
                foreach (var beacon in beaconsToLoop.ToList())
                {
                    var foundBeacon = false;
                    foreach (var lst in beacon.GetAllPoints())
                    {
                        foreach (var pL in constellation.Points)
                        {
                            foreach (var pR in lst)
                            {
                                var pointsInCommon = 0;
                                var diffX = pL.X - pR.X;
                                var diffY = pL.Y - pR.Y;
                                var diffZ = pL.Z - pR.Z;
                                foreach (var pL2 in constellation.Points)
                                {
                                    foreach (var pR2 in lst)
                                    {
                                        if (pL2.X - diffX == pR2.X && pL2.Y - diffY == pR2.Y && pL2.Z - diffZ == pR2.Z) pointsInCommon++;
                                    }
                                }
                                if (pointsInCommon >= 12)
                                {
                                    pointsToKeep.Add(new Point3D { X = diffX, Y = diffY, Z = diffZ });
                                    foreach (var pR2 in lst)
                                    {
                                        var found = false;
                                        foreach (var pL2 in constellation.Points)
                                        {
                                            if (pL2.X - diffX == pR2.X && pL2.Y - diffY == pR2.Y && pL2.Z - diffZ == pR2.Z)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (!found)
                                        {
                                            hasNew = true;
                                            var newP = new Point3D { X = pR2.X + diffX, Y = pR2.Y + diffY, Z = pR2.Z + diffZ };
                                            constellation.Points.Add(newP);
                                        }
                                    }
                                    foundBeacon = true;
                                    break;
                                }
                            }
                            if (foundBeacon) break;
                        }
                        if (foundBeacon) break;
                    }
                    if (foundBeacon)
                    {
                        beaconsToLoop.Remove(beacon);
                    }
                }
            }
            while (hasNew);

            var max = 0;
            foreach(var p in pointsToKeep)
            {
                foreach(var p2 in pointsToKeep)
                {
                    var mat = Math.Abs(p.X - p2.X) + Math.Abs(p.Y - p2.Y) + Math.Abs(p.Z - p2.Z);
                    if (mat > max) max = mat;
                }
            }
            return max.ToString();
        }
    }
}