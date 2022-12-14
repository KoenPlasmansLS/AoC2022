namespace AoC2022
{
    public class Solution202227 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution14.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var arr = new bool[600,200];
            foreach (var line in lines)
            {
                var splt = line.Trim().Split(" -> ");
                for (var i = 1; i < splt.Length; i++)
                {
                    var beginX = int.Parse(splt[i - 1].Split(",")[0]);
                    var beginY = int.Parse(splt[i - 1].Split(",")[1]);
                    var endX = int.Parse(splt[i].Split(",")[0]);
                    var endY = int.Parse(splt[i].Split(",")[1]);
                    if (beginX == endX)
                    {
                        if (endY - beginY > 0)
                        {
                            for (var j = beginY; j <= endY; j++)
                            {
                                arr[beginX, j] = true;
                            }
                        }
                        else
                        {
                            for (var j = beginY; j >= endY; j--)
                            {
                                arr[beginX, j] = true;
                            }
                        }
                    }
                    else
                    {
                        if (endX - beginX > 0)
                        {
                            for (var j = beginX; j <= endX; j++)
                            {
                                arr[j, beginY] = true;
                            }
                        }
                        else
                        {
                            for (var j = beginX; j >= endX; j--)
                            {
                                arr[j, beginY] = true;
                            }
                        }
                    }
                }
            }
            var overflowing = false;
            var sum = 0;
            while(!overflowing)
            {
                var xpos = 500;
                var ypos = 0;
                var canMove = true;
                while(canMove)
                {
                    if (!arr[xpos, ypos + 1])
                    {
                        ypos++;
                        if (ypos == 199)
                        {
                            overflowing = true;
                            canMove = false;
                        }
                    }
                    else if (!arr[xpos - 1, ypos + 1])
                    {
                        xpos--;
                        ypos++;
                    }
                    else if (!arr[xpos + 1, ypos + 1])
                    {
                        xpos++;
                        ypos++;
                    }
                    else
                    {
                        sum++;
                        canMove = false;
                        arr[xpos, ypos] = true;
                    }
                }
            }
            return sum.ToString();
        }
    }
}