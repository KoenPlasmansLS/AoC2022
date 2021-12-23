namespace AoC2022
{
    public class Solution43 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution22.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var cube = new bool[101, 101, 101];
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
                if (x1 <= 50 && x1 >= -50 && x2 <= 50 && x2 >= -50 && y1 <= 50 && y1 >= -50 && y2 <= 50 && y2 >= -50 && z1 <= 50 && z1 >= -50 && z2 <= 50 && z2 >= -50)
                {
                    for (var i = x1; i <= x2; i++)
                    {
                        for (var j = y1; j <= y2; j++)
                        {
                            for (var k = z1; k <= z2; k++)
                            {
                                cube[i + 50, j + 50, k + 50] = isOn;
                            }
                        }
                    }
                }
            }

            var sum = 0;
            for (var i = -50; i <= 50; i++)
            {
                for (var j = -50; j <= 50; j++)
                {
                    for (var k = -50; k <= 50; k++)
                    {
                        if (cube[i+50, j+50, k+50]) sum++;
                    }
                }
            }

            return sum.ToString();
        }
    }
}