namespace AoC2022
{
    public class Solution202218 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution9.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var posses = new (int, int)[10];
            var lst = new List<(int, int)>();
            lst.Add((posses[9].Item1, posses[9].Item2));
            foreach (var line in lines)
            {
                var pos = line.Trim().Split(" ")[0];
                var times = int.Parse(line.Trim().Split(" ")[1]);
                for (var i = 0; i < times; i++)
                {
                    if (pos == "R")
                    {
                        posses[0].Item1++;
                    }
                    if (pos == "L")
                    {
                        posses[0].Item1--;
                    }
                    if (pos == "U")
                    {
                        posses[0].Item2++;
                    }
                    if (pos == "D")
                    {
                        posses[0].Item2--;
                    }
                    for (var j = 0; j < 9; j++)
                    {
                        var movedX = false;
                        var movedY = false;
                        if (posses[j].Item1 - posses[j + 1].Item1 == 2)
                        {
                            posses[j + 1].Item1++;
                            movedX = true;
                            //posses[j + 1].Item2 = posses[j].Item2;
                        }
                        if (posses[j + 1].Item1 - posses[j].Item1 == 2)
                        {
                            posses[j + 1].Item1--;
                            movedX = true;
                            //posses[j + 1].Item2 = posses[j].Item2;
                        }
                        if (posses[j].Item2 - posses[j + 1].Item2 == 2)
                        {
                            posses[j + 1].Item2++;
                            movedY = true;
                            //posses[j + 1].Item1 = posses[j].Item1;
                        }
                        if (posses[j + 1].Item2 - posses[j].Item2 == 2)
                        {
                            posses[j + 1].Item2--;
                            movedY = true;
                            //posses[j + 1].Item1 = posses[j].Item1;
                        }
                        if (movedX && !movedY)
                        {
                            posses[j + 1].Item2 = posses[j].Item2;
                        }
                        if (movedY && !movedX)
                        {
                            posses[j + 1].Item1 = posses[j].Item1;
                        }
                    }
                    if (!lst.Contains((posses[9].Item1, posses[9].Item2)))
                    {
                        lst.Add((posses[9].Item1, posses[9].Item2));
                    }
                }
            }
            return lst.Count.ToString();
        }
    }
}