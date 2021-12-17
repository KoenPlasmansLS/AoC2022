namespace AoC2022
{
    public class Solution34 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution17.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var line = lines[0].Trim();
            var beginIndex = line.IndexOf("x=") + 2;
            var endIndex = line.IndexOf(",");
            var xS = line.Substring(beginIndex, endIndex - beginIndex);
            var xSS = xS.Split(".");
            var minX = int.Parse(xSS[0]);
            var maxX = int.Parse(xSS[2]);
            beginIndex = line.IndexOf("y=") + 2;
            var yS = line.Substring(beginIndex);
            var ySS = yS.Split(".");
            var minY = int.Parse(ySS[0]);
            var maxY = int.Parse(ySS[2]);

            //lets bruteforce it
            var sum = 0;
            for (var j = 0; j <= maxX; j++)
            {
                for (var i = minY; i <= Math.Abs(minY); i++)
                {
                    var currentX = j;
                    var currentXSpeed = j;
                    var currentY = i;
                    var currentYSpeed = i;
                    while (currentY >= minY)
                    {
                        if (currentY >= minY && currentY <= maxY && currentX >= minX && currentX <= maxX)
                        {
                            sum++;
                            break;
                        }
                        currentXSpeed--;
                        if (currentXSpeed < 0) currentXSpeed = 0;
                        currentX += currentXSpeed;
                        currentYSpeed--;
                        currentY += currentYSpeed;
                    }
                }
            }
            return sum.ToString();
        }
    }
}