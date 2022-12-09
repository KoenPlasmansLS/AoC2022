namespace AoC2022
{
    public class Solution202217 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution9.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var xPosT = 0;
            var yPosT = 0;
            var xPosH = 0;
            var yPosH = 0;
            var lst = new List<(int, int)>();
            lst.Add((xPosT, yPosT));
            foreach (var line in lines)
            {
                var pos = line.Trim().Split(" ")[0];
                var times = int.Parse(line.Trim().Split(" ")[1]);
                for (var i = 0; i < times; i++)
                {
                    if (pos == "R")
                    {
                        xPosH++;
                    }
                    if (pos == "L")
                    {
                        xPosH--;
                    }
                    if (pos == "U")
                    {
                        yPosH++;
                    }
                    if (pos == "D")
                    {
                        yPosH--;
                    }
                    if (xPosH - xPosT == 2)
                    {
                        xPosT++;
                        yPosT = yPosH;
                    }
                    if (xPosT - xPosH == 2)
                    {
                        xPosT--;
                        yPosT = yPosH;
                    }
                    if (yPosT - yPosH == 2)
                    {
                        yPosT--;
                        xPosT = xPosH;
                    }
                    if (yPosH - yPosT == 2)
                    {
                        yPosT++;
                        xPosT = xPosH;
                    }
                    if (!lst.Contains((xPosT, yPosT)))
                    {
                        lst.Add((xPosT, yPosT));
                    }
                }
            }
            return lst.Count.ToString();
        }
    }
}