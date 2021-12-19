namespace AoC2022
{
    public class Solution37 : IProvideSolution
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
            foreach(var line in lines)
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
            var beaconsToLoop = beacons.Skip(1).ToList();
            var hasNew = false;
            do
            {
                hasNew = false;
                foreach (var beacon in beaconsToLoop.ToList())
                {
                    var foundBeacon = false;
                    foreach(var lst in beacon.GetAllPoints())
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
            return constellation.Points.Count().ToString();
        }
    }

    public class Beacon
    {
        public List<Point3D> Points { get; } = new List<Point3D>();

        public List<List<Point3D>> GetAllPoints()
        {
            var p = new List<List<Point3D>>
            {
                Points,
                RotateLeft(Points),
                RotateHalf(Points),
                RotateRight(Points)
            };
            var minusX = Reverse(Points);
            p.Add(minusX);
            p.Add(RotateLeft(minusX));
            p.Add(RotateHalf(minusX));
            p.Add(RotateRight(minusX));
            var rotY = MakeY(Points);
            p.Add(rotY);
            p.Add(RotateLeft(rotY));
            p.Add(RotateHalf(rotY));
            p.Add(RotateRight(rotY));
            var minusY = Reverse(rotY);
            p.Add(minusY);
            p.Add(RotateLeft(minusY));
            p.Add(RotateHalf(minusY));
            p.Add(RotateRight(minusY));

            var rotZ = MakeZ(Points);
            p.Add(rotZ);
            p.Add(RotateLeft(rotZ));
            p.Add(RotateHalf(rotZ));
            p.Add(RotateRight(rotZ));
            var minusZ = Reverse(rotZ);
            p.Add(minusZ);
            p.Add(RotateLeft(minusZ));
            p.Add(RotateHalf(minusZ));
            p.Add(RotateRight(minusZ));

            return p;
        }

        private List<Point3D> MakeY(List<Point3D> points)
        {
            var p = new List<Point3D>();
            foreach (var point in points)
            {
                //X = Y, Y = -X
                p.Add(new Point3D { X = point.Y, Y = -point.X, Z = point.Z });
            }
            return p;
        }

        private List<Point3D> MakeZ(List<Point3D> points)
        {
            var p = new List<Point3D>();
            foreach (var point in points)
            {
                p.Add(new Point3D { X = point.Z, Y = point.Y, Z = -point.X });
            }
            return p;
        }

        private List<Point3D> Reverse(List<Point3D> points)
        {
            var p = new List<Point3D>();
            foreach (var point in points)
            {
                p.Add(new Point3D { X = -point.X, Y = -point.Y, Z = point.Z });
            }
            return p;
        }

        private List<Point3D> RotateLeft(List<Point3D> points)
        {
            var p = new List<Point3D>();
            foreach(var point in points)
            {
                //y=z & z = -y
                p.Add(new Point3D { X = point.X, Y = point.Z, Z = -point.Y });
            }
            return p;
        }

        private List<Point3D> RotateHalf(List<Point3D> points)
        {
            var p = new List<Point3D>();
            foreach (var point in points)
            {
                //y=-y & z = -z
                p.Add(new Point3D { X = point.X, Y = -point.Y, Z = -point.Z });

            }
            return p;
        }

        private List<Point3D> RotateRight(List<Point3D> points)
        {
            var p = new List<Point3D>();
            foreach (var point in points)
            {
                //y=-z & z = y
                p.Add(new Point3D { X = point.X, Y = -point.Z, Z = point.Y });
            }
            return p;
        }
    }

    public struct Point3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}