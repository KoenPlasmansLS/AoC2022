namespace AoC2022
{
    public class Solution44 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution22.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var lst = new List<Range3D>();
            foreach(var line in lines)
            {
                var isOn = line.StartsWith("on");
                var corline = line.Trim().Replace("off x=", string.Empty).Replace("on x=", string.Empty).Replace("y=", string.Empty).Replace("z=", string.Empty);
                var split = corline.Split(',');
                var x1 = int.Parse(split[0].Split('.')[0]);
                var x2 = int.Parse(split[0].Split('.')[2]);
                var y1 = int.Parse(split[1].Split('.')[0]);
                var y2 = int.Parse(split[1].Split('.')[2]);
                var z1 = int.Parse(split[2].Split('.')[0]);
                var z2 = int.Parse(split[2].Split('.')[2]);
                var newR = new Range3D { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2, Z1 = z1, Z2 = z2, IsOn = isOn };
                lst = Merge(lst, newR);
            }

            long sum = 0;
            foreach(var l in lst)
            {
                sum += l.Sum();
            }
            return sum.ToString();
        }

        private List<Range3D> Merge(List<Range3D> lst, Range3D newR)
        {
            lst.AddRange(
                lst
                    .Select(oldR => Overlap(newR, oldR))
                    .Where(overR => overR.X1 <= overR.X2
                        && overR.Y1 <= overR.Y2
                        && overR.Z1 <= overR.Z2)
                    .ToList());

            if (newR.IsOn)
            {
                lst.Add(newR);
            }
            return lst;
        }

        private static Range3D Overlap(Range3D left, Range3D right)
        {
            return new Range3D {
                IsOn = !right.IsOn,
                X1 = Math.Max(left.X1, right.X1),
                X2 = Math.Min(left.X2, right.X2),
                Y1 = Math.Max(left.Y1, right.Y1),
                Y2 = Math.Min(left.Y2, right.Y2),
                Z1 = Math.Max(left.Z1, right.Z1),
                Z2 = Math.Min(left.Z2, right.Z2)
            };
        }

        private class Range3D
        {
            public bool IsOn;
            public int X1;
            public int X2;
            public int Y1;
            public int Y2;
            public int Z1;
            public int Z2;

            public long Sum()
            {
                return ((long) (X2 - X1 + 1)) * (Y2 - Y1 +1) * (Z2 - Z1 + 1) * (IsOn ? 1 : -1);
            }
        }
    }
}