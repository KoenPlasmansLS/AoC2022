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
                var newR = new Range3D { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2, Z1 = z1, Z2 = z2 };
                lst = Merge(lst, newR, isOn);
            }

            long sum = 0;
            foreach(var l in lst)
            {
                sum += l.Sum();
            }
            return sum.ToString();
        }

        private List<Range3D> Merge(List<Range3D> lst, Range3D newR, bool isOn)
        {
            var point1 = new Point3D { X = newR.X1, Y = newR.Y1, Z = newR.Z1 };
            var point2 = new Point3D { X = newR.X2, Y = newR.Y1, Z = newR.Z1 };
            var point3 = new Point3D { X = newR.X1, Y = newR.Y2, Z = newR.Z1 };
            var point4 = new Point3D { X = newR.X2, Y = newR.Y2, Z = newR.Z1 };
            var point5 = new Point3D { X = newR.X1, Y = newR.Y1, Z = newR.Z2 };
            var point6 = new Point3D { X = newR.X2, Y = newR.Y1, Z = newR.Z2 };
            var point7 = new Point3D { X = newR.X1, Y = newR.Y2, Z = newR.Z2 };
            var point8 = new Point3D { X = newR.X2, Y = newR.Y2, Z = newR.Z2 };

            var newList = new List<Range3D>();
            foreach(var k in lst)
            {
                var pIn1 = IsPointIn(point1, k);
                var pIn2 = IsPointIn(point2, k);
                var pIn3 = IsPointIn(point3, k);
                var pIn4 = IsPointIn(point4, k);
                var pIn5 = IsPointIn(point5, k);
                var pIn6 = IsPointIn(point6, k);
                var pIn7 = IsPointIn(point7, k);
                var pIn8 = IsPointIn(point8, k);

                var opoint1 = new Point3D { X = k.X1, Y = k.Y1, Z = k.Z1 };
                var opoint2 = new Point3D { X = k.X2, Y = k.Y1, Z = k.Z1 };
                var opoint3 = new Point3D { X = k.X1, Y = k.Y2, Z = k.Z1 };
                var opoint4 = new Point3D { X = k.X2, Y = k.Y2, Z = k.Z1 };
                var opoint5 = new Point3D { X = k.X1, Y = k.Y1, Z = k.Z2 };
                var opoint6 = new Point3D { X = k.X2, Y = k.Y1, Z = k.Z2 };
                var opoint7 = new Point3D { X = k.X1, Y = k.Y2, Z = k.Z2 };
                var opoint8 = new Point3D { X = k.X2, Y = k.Y2, Z = k.Z2 };

                var opIn1 = IsPointIn(opoint1, newR);
                var opIn2 = IsPointIn(opoint2, newR);
                var opIn3 = IsPointIn(opoint3, newR);
                var opIn4 = IsPointIn(opoint4, newR);
                var opIn5 = IsPointIn(opoint5, newR);
                var opIn6 = IsPointIn(opoint6, newR);
                var opIn7 = IsPointIn(opoint7, newR);
                var opIn8 = IsPointIn(opoint8, newR);

                if (pIn1 && pIn2 && pIn3 && pIn4 && pIn5 && pIn6 && pIn7 && pIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z - 1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point8.Z + 1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z, X2 = k.X2, Y2 = point1.Y-1, Z2 = point8.Z });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point8.Y+1, Z1 = point1.Z, X2 = k.X2, Y2 = k.Z2, Z2 = point8.Z });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point1.Y, Z1 = point1.Z, X2 = point1.X-1, Y2 = point8.Y, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point8.X+1, Y1 = point1.Y, Z1 = point1.Z, X2 = k.X2, Y2 = point8.Y, Z2 = k.Z2 });
                }
                else if (opIn1 && opIn2 && opIn3 && opIn4 && opIn5 && opIn6 && opIn7 && opIn8)
                {
                    //keep nothing of old
                }
                else if (pIn1 && pIn2 && pIn3 && pIn4)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z - 1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z, X2 = k.X2, Y2 = point1.Y-1, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point3.Y+1, Z1 = point1.Z, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point1.Y, Z1 = point1.Z, X2 = point1.X-1, Y2 = point3.Y, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point2.X+1, Y1 = point1.Y, Z1 = point1.Z, X2 = k.X2, Y2 = point3.Y, Z2 = k.Z2 });
                }
                else if (pIn5 && pIn6 && pIn7 && pIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point5.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = point1.Y - 1, Z2 = point5.Z });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point3.Y + 1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point5.Z });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point1.Y, Z1 = k.Z1, X2 = point1.X - 1, Y2 = point3.Y, Z2 = point5.Z });
                    newList.Add(new Range3D { X1 = point2.X + 1, Y1 = point1.Y, Z1 = k.Z1, X2 = k.X2, Y2 = point3.Y, Z2 = point5.Z });
                }
                else if (pIn1 && pIn3 && pIn5 && pIn7)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point1.X -1, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z-1 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = k.Y1, Z1 = point5.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = k.Y1, Z1 = point1.Z, X2 = k.X2, Y2 = point1.Y-1, Z2 = point5.Z });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = point3.Y+1, Z1 = point1.Z, X2 = k.X2, Y2 = k.Y2, Z2 = point5.Z });
                }
                else if (pIn2 && pIn4 && pIn6 && pIn8)
                {
                    newList.Add(new Range3D { X1 = point2.X+1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point2.X, Y2 = k.Y2, Z2 = point2.Z - 1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point6.Z + 1, X2 = point2.X, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point2.Z, X2 = point2.X, Y2 = point2.Y - 1, Z2 = point6.Z });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point4.Y + 1, Z1 = point2.Z, X2 = point2.X, Y2 = k.Y2, Z2 = point6.Z });
                }
                else if (pIn1 && pIn2 && pIn5 && pIn6)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = point1.Y-1, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point1.Y, Z1 = k.Z1, X2 = point1.X-1, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point2.X+1, Y1 = point1.Y, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = point1.Y, Z1 = k.Z1, X2 = point2.X, Y2 = k.Y2, Z2 = point1.Z-1 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = point1.Y, Z1 = point5.Z+1, X2 = point2.X, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (pIn3 && pIn4 && pIn7 && pIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point3.Y+1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point1.X - 1, Y2 = point3.Y, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point2.X + 1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = point3.Y, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = k.Y1, Z1 = k.Z1, X2 = point2.X, Y2 = point3.Y, Z2 = point1.Z - 1 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = k.Y1, Z1 = point5.Z + 1, X2 = point2.X, Y2 = point3.Y, Z2 = k.Z2 });
                }
                else if (opIn1 && opIn2 && opIn3 && opIn4)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = newR.Z2+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 }); 
                }
                else if (opIn5 && opIn6 && opIn7 && opIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = newR.Z1-1 });
                }
                else if (opIn1 && opIn3 && opIn5 && opIn7)
                {
                    newList.Add(new Range3D { X1 = newR.X2+1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (opIn2 && opIn4 && opIn6 && opIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = newR.X1-1, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (opIn1 && opIn2 && opIn5 && opIn6)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = newR.Y2+1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (opIn3 && opIn4 && opIn7 && opIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = newR.Y1-1, Z2 = k.Z2 });
                }
                else if (pIn1 && pIn2)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z-1 });
                    //
                }
                else if (pIn1 && pIn3)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z - 1 });
                    //
                }
                else if (pIn1 && pIn5)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point1.X-1, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (pIn2 && pIn4)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z - 1 });
                    //
                }
                else if (pIn2 && pIn6)
                {
                    newList.Add(new Range3D { X1 = point2.X+1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (pIn3 && pIn4)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z - 1 });
                }
                else if (pIn3 && pIn7)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point1.X - 1, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (pIn4 && pIn8)
                {
                    newList.Add(new Range3D { X1 = point2.X + 1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (pIn5 && pIn6)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (pIn5 && pIn7)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (pIn6 && pIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z + 1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (pIn7 && pIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z + 1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    //
                }
                else if (opIn1 && opIn2)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point5.Z + 1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point5.Y+1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point5.Z });
                }
                else if (opIn1 && opIn3)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point5.Z + 1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point5.X+1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point5.Z });
                }
                else if (opIn1 && opIn5)
                {
                    //
                }
                else if (opIn2 && opIn4)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point6.Z + 1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point6.X-1, Y2 = k.Y2, Z2 = point6.Z });
                }
                else if (opIn2 && opIn6)
                {
                    //
                }
                else if (opIn3 && opIn4)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point7.Z + 1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = point7.Y-1, Z2 = point7.Z });
                }
                else if (opIn3 && opIn7)
                {
                    //
                }
                else if (opIn4 && opIn8)
                {
                    //
                }
                else if (opIn5 && opIn6)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1 + 1, X2 = k.X2, Y2 = k.Y2, Z2 =point1.Z - 1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point1.Y + 1, Z1 = point1.Z, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (opIn5 && opIn7)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z-1 });
                    newList.Add(new Range3D { X1 = point1.X + 1, Y1 = k.Y1, Z1 = point1.Z, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (opIn6 && opIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z - 1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z, X2 = point1.X - 1, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (opIn7 && opIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z-1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z, X2 = k.X2, Y2 = point1.Y - 1, Z2 = k.Z2 });
                }
                else if (pIn1)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point1.Z-1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point1.Z, X2 = point1.X-1, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point1.X, Y1 = k.Y1, Z1 = point1.Z, X2 = k.X2, Y2 = point1.Y-1, Z2 = k.Z2 });
                }
                else if (pIn2)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point2.Z-1 });
                    newList.Add(new Range3D { X1 = point2.X+1, Y1 = k.Y1, Z1 = point2.Z, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point2.Z, X2 = point2.X, Y2 = point2.Y-1, Z2 = k.Z2 });
                }
                else if (pIn3)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point3.Z-1 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point3.Z, X2 = point3.X-1, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point3.X, Y1 = point3.Y+1, Z1 = point3.Z, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (pIn4)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point4.Z-1 });
                    newList.Add(new Range3D { X1 = point4.X+1, Y1 = k.Y1, Z1 = point4.Z, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point4.Y+1, Z1 = point4.Z, X2 = point4.X, Y2 = k.Y2, Z2 = k.Z2 });
                }
                else if (pIn5)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point5.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point5.X-1, Y2 = k.Y2, Z2 = point5.Z });
                    newList.Add(new Range3D { X1 = point5.X, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = point5.Y-1, Z2 = point5.Z });
                }
                else if (pIn6)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point6.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point6.X+1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point6.Z });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point6.X, Y2 = point6.Y-1, Z2 = point6.Z });
                }
                else if (pIn7)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point7.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = k.Z1, X2 = point7.X-1, Y2 = k.Y2, Z2 = point7.Z });
                    newList.Add(new Range3D { X1 = point7.X, Y1 = point7.Y+1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point7.Z });
                }
                else if (pIn8)
                {
                    newList.Add(new Range3D { X1 = k.X1, Y1 = k.Y1, Z1 = point8.Z+1, X2 = k.X2, Y2 = k.Y2, Z2 = k.Z2 });
                    newList.Add(new Range3D { X1 = point8.X+1, Y1 = k.Y1, Z1 = k.Z1, X2 = k.X2, Y2 = k.Y2, Z2 = point8.Z });
                    newList.Add(new Range3D { X1 = k.X1, Y1 = point8.Y+1, Z1 = k.Z1, X2 = point8.X, Y2 = k.Y2, Z2 = point8.Z });
                }
                else
                {
                    newList.Add(k);
                }
                
            }
            if (isOn)
            {
                newList.Add(newR);
            }
            return newList;
        }


        private bool IsPointIn(Point3D point, Range3D k)
        {
            return point.X >= k.X1 && point.X <= k.X2 && point.Y >= k.Y1 && point.Y <= k.Y2 && point.Z >= k.Z1 && point.Z <= k.Z2;
        }
        

        private class Range3D
        {
            public int X1;
            public int X2;
            public int Y1;
            public int Y2;
            public int Z1;
            public int Z2;

            public long Sum()
            {
                return ((long) (X2 - X1 + 1)) * (Y2 - Y1 +1) * (Z2 - Z1 + 1);
            }

            public override string ToString()
            {
                return $"{X1}<=>{X2},{Y1}<=>{Y2},{Z1}<=>{Z2}:{Sum()}";
            }
        }
    }
}