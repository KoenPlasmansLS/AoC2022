namespace AoC2022
{
    public class Gear
    {
        public List<int> Nrs = new();
    }

    public class Solution20236 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution3.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var dictGear = new Dictionary<(int, int), Gear>();
            var matrix = new char[lines.Count(), lines[0].Trim().Length];
            for (var i = 0; i < lines.Count(); i++)
            {
                for(var j =0; j < lines[i].Trim().Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                    if (matrix[i,j] == 42)
                    {
                        dictGear.Add((i, j), new Gear());
                    }
                }
            }
            for (var i = 0; i < lines.Count(); i++)
            {
                var nr = 0;
                var symbolFound = false;
                var gears = new List<Gear>();
                for (var j = 0; j < lines[i].Trim().Length; j++)
                {
                    if (matrix[i, j] < 48 || matrix[i,j] >= 58)
                    {
                        if (nr > 0 && symbolFound && gears.Count > 0)
                        {
                            foreach(var gear in gears)
                            {
                                gear.Nrs.Add(nr);
                            }
                        }
                        nr = 0;
                        gears = new List<Gear>();
                        symbolFound = false;
                    }
                    else
                    {
                        if (matrix[i, j] >= 48 && matrix[i, j] < 58)
                        {
                            nr = nr * 10 + (matrix[i, j] - 48);
                        }
                        //D
                        if (j < lines[i].Trim().Length - 1 && matrix[i, j + 1] != 46 && (matrix[i, j + 1] == 42))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i, j+1), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                        //U
                        if (j > 0 && matrix[i, j - 1] != 46 && (matrix[i, j - 1] < 48 || matrix[i, j - 1] >= 58))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i, j - 1), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                        //R
                        if (i < lines.Length - 1 && matrix[i + 1, j] != 46 && (matrix[i + 1, j] < 48 || matrix[i + 1, j] >= 58))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i+1, j), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                        //L
                        if (i > 0 && matrix[i - 1, j] != 46 && (matrix[i - 1, j] < 48 || matrix[i - 1, j] >= 58))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i-1, j), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                        //UR
                        if (i < lines.Length - 1 && j > 0 && matrix[i + 1, j - 1] != 46 && (matrix[i + 1, j - 1] < 48 || matrix[i + 1, j - 1] >= 58))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i+1, j - 1), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                        //UL
                        if (i > 0 && j > 0 && matrix[i - 1, j - 1] != 46 && (matrix[i - 1, j - 1] < 48 || matrix[i - 1, j - 1] >= 58))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i-1, j - 1), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                        //DR
                        if (i < lines.Length - 1 && j < lines[i].Trim().Length - 1 && matrix[i + 1, j + 1] != 46 && (matrix[i + 1, j + 1] < 48 || matrix[i + 1, j + 1] >= 58))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i+1, j + 1), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                        //DL
                        if (i > 0 && j < lines[i].Trim().Length - 1 && matrix[i - 1, j + 1] != 46 && (matrix[i - 1, j + 1] < 48 || matrix[i - 1, j + 1] >= 58))
                        {
                            symbolFound = true;
                            if (dictGear.TryGetValue((i-1, j + 1), out Gear g) && !gears.Contains(g))
                            {
                                gears.Add(g);
                            }
                        }
                    }
                }
                if (nr > 0 && symbolFound && gears.Count > 0)
                {
                    foreach (var gear in gears)
                    {
                        gear.Nrs.Add(nr);
                    }
                }
            }
            long sum = 0;
            foreach(var gear in dictGear.Values.Where(x => x.Nrs.Count == 2))
            {
                sum += gear.Nrs[0] * gear.Nrs[1];
            }
            return sum.ToString();
        }
    }
}