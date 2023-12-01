namespace AoC2022
{
    public class Solution20232 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution1.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            for (var i = 0; i < lines.Count(); i++)
            {
                var now = 0;
                for (var j = 0; j < lines[i].Length; j++)
                {
                    if (j + 3 < lines[i].Length && lines[i][j..(j + 3)].ToString() == "one")
                    {
                        now += 10;
                        break;
                    }
                    if (j + 3 < lines[i].Length && lines[i][j..(j + 3)].ToString() == "two")
                    {
                        now += 20;
                        break;
                    }
                    if (j + 5 < lines[i].Length && lines[i][j..(j + 5)].ToString() == "three")
                    {
                        now += 30;
                        break;
                    }
                    if (j + 4 < lines[i].Length && lines[i][j..(j + 4)].ToString() == "four")
                    {
                        now += 40;
                        break;
                    }
                    if (j + 4 < lines[i].Length && lines[i][j..(j + 4)].ToString() == "five")
                    {
                        now += 50;
                        break;
                    }
                    if (j + 3 < lines[i].Length && lines[i][j..(j + 3)].ToString() == "six")
                    {
                        now += 60;
                        break;
                    }
                    if (j + 5 < lines[i].Length && lines[i][j..(j + 5)].ToString() == "seven")
                    {
                        now += 70;
                        break;
                    }
                    if (j + 5 < lines[i].Length && lines[i][j..(j + 5)].ToString() == "eight")
                    {
                        now += 80;
                        break;
                    }
                    if (j + 4 < lines[i].Length && lines[i][j..(j + 4)].ToString() == "nine")
                    {
                        now += 90;
                        break;
                    }
                    if (int.TryParse(lines[i][j].ToString(), out int d1))
                    {
                        now += d1 * 10;
                        break;
                    }
                }
                for (var j = lines[i].Length - 1; j >= 0; j--)
                {
                    if (j - 3 >= 0 && lines[i][(j-3)..j].ToString() == "one")
                    {
                        now += 1;
                        break;
                    }
                    if (j - 3 >= 0 && lines[i][(j-3)..j].ToString() == "two")
                    {
                        now += 2;
                        break;
                    }
                    if (j - 5 >= 0 && lines[i][(j-5)..j].ToString() == "three")
                    {
                        now += 3;
                        break;
                    }
                    if (j - 4 >= 0 && lines[i][(j-4)..j].ToString() == "four")
                    {
                        now += 4;
                        break;
                    }
                    if (j - 4 >= 0 && lines[i][(j-4)..j].ToString() == "five")
                    {
                        now += 5;
                        break;
                    }
                    if (j - 3 >= 0 && lines[i][(j-3)..j].ToString() == "six")
                    {
                        now += 6;
                        break;
                    }
                    if (j - 5 >= 0 && lines[i][(j-5)..j].ToString() == "seven")
                    {
                        now += 7;
                        break;
                    }
                    if (j - 5 >= 0 && lines[i][(j-5)..j].ToString() == "eight")
                    {
                        now += 8;
                        break;
                    }
                    if (j - 4 >= 0 && lines[i][(j-4)..j].ToString() == "nine")
                    {
                        now += 9;
                        break;
                    }
                    if (int.TryParse(lines[i][j-1].ToString(), out int d))
                    {
                        now += d;
                        break;
                    }
                }
                sum += now;
            }
            return sum.ToString();
        }
    }
}