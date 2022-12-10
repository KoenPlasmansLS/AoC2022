namespace AoC2022
{
    public class Solution202220 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution10.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var cycle = 1;
            var value = 1;
            var arr = new bool[40, 6];
            foreach (var line in lines)
            {
                if (line.Trim() == "noop")
                {
                    TakeValue(arr, cycle, value);
                    cycle++;
                }
                else
                {
                    TakeValue(arr,cycle, value);
                    cycle++;
                    TakeValue(arr,cycle, value);
                    cycle++;
                    value += int.Parse(line.Trim().Split(" ")[1]);
                }
            }
            TakeValue(arr, cycle, value);
            for(var j =0;j < 6;j++)
            {
                for (var i = 0; i < 40; i++)
                {
                    if (arr[i,j])
                    {
                        System.Console.Write("#");
                    }
                    else
                    {
                        System.Console.Write(".");
                    }
                }
                System.Console.WriteLine();
            }
            return 837.ToString();
        }

        private static void TakeValue(bool[,] arr, int cycle, int value)
        {
            var posX = (cycle - 1) % 40;
            var posY = cycle / 40;
            if (cycle%40 >= value && cycle%40 <= value + 2)
            {
                arr[posX, posY] = true;
            }
        }
    }
}