namespace AoC2022
{
    public class Solution202319 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution10.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var arr = new char[lines[0].Count(), lines.Count()];
            var i = 0;
            var beginX = 0;
            var beginY = 0;
            foreach (var line in lines)
            {
                for (var j = 0; j < line.Trim().Length; j++)
                {
                    arr[j, i] = line[j];
                    if (line[j] == 'S')
                    {
                        beginX = j;
                        beginY = i;
                    }
                }
                i++;
            }
            var previousX = beginX;
            var previousY = beginY;

            var nextX = beginX;
            var nextY = beginY;
            if (arr[nextX, nextY+1] == '|' || arr[nextX, nextY + 1] == 'L' || arr[nextX, nextY + 1] == 'J')
            {
                nextY++;
            }
            else if (arr[nextX, nextY - 1] == '|' || arr[nextX, nextY - 1] == 'F' || arr[nextX, nextY - 1] == '7')
            {
                nextY--;
            }
            else if (arr[nextX+1, nextY] == '-' || arr[nextX+1, nextY] == 'J' || arr[nextX+1, nextY] == '7')
            {
                nextX++;
            }
            var nr = 1;
            while(nextX != beginX || nextY != beginY)
            {
                if (arr[nextX,nextY] == '|')
                {
                    var temp = nextY;
                    nextY += (nextY - previousY);
                    previousY = temp;
                }
                else if (arr[nextX, nextY] == '-')
                {
                    var temp = nextX;
                    nextX += (nextX - previousX);
                    previousX = temp;
                }
                else if (arr[nextX, nextY] == 'L')
                {
                    if (nextY == previousY)
                    {
                        nextY--;
                        previousX--;
                    }
                    else
                    {
                        nextX++;
                        previousY++;
                    }
                }
                else if (arr[nextX, nextY] == 'J')
                {
                    if (nextY == previousY)
                    {
                        nextY--;
                        previousX++;
                    }
                    else
                    {
                        nextX--;
                        previousY++;
                    }
                }
                else if (arr[nextX, nextY] == '7')
                {
                    if (nextY == previousY)
                    {
                        nextY++;
                        previousX++;
                    }
                    else
                    {
                        nextX--;
                        previousY--;
                    }
                }
                else if (arr[nextX, nextY] == 'F')
                {
                    if (nextY == previousY)
                    {
                        nextY++;
                        previousX--;
                    }
                    else
                    {
                        nextX++;
                        previousY--;
                    }
                }
                else
                {
                    throw new Exception("le");
                }
                nr++;
            }
            return (nr/2).ToString();
        }
    }
}