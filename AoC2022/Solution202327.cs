namespace AoC2022
{
    public class Solution202327 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution14.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var k = 0;
            var arr = new int[lines[0].Trim().Length, lines.Count()];
            foreach (var line in lines)
            {
                for (var j = 0; j < line.Trim().Length; j++)
                {
                    if (line[j] == '#')
                    {
                        arr[j, k] = 1;
                    }
                    if (line[j] == 'O')
                    {
                        arr[j, k] = 2;
                    }
                }
                k++;
            }
            //WriteArr(arr);
            //Console.WriteLine("11111111111111");
            arr = TiltNorth(arr);
            WriteArr(arr);
            var sum = CountNorth(arr);
            return sum.ToString();
        }

        private static void WriteArr(int[,] arr)
        {
            for (var j = 0; j < arr.GetLength(1); j++)
            {
                for (var i = 0; i < arr.GetLength(0); i++)
                {
                    Console.Write(arr[i, j] == 2 ? 'O' : arr[i, j] == 1 ? '#' : '.');
                }
                Console.WriteLine();
            }
        }

        private static int[,] TiltNorth(int[,] arr)
        {
            for(var j = 0; j<arr.GetLength(0); j++)
            {
                var end = 0;
                for (var i =0;i<arr.GetLength(1);i++)
                {
                    if (arr[j,i]==1)
                    {
                        end = i+1;
                    }
                    if (arr[j,i]==2)
                    {
                        arr[j, i] = 0;
                        arr[j, end] = 2;
                        end++;
                    }
                }
            }
            return arr;
        }

        private static int[,] TiltWest(int[,] arr)
        {
            for (var j = 0; j < arr.GetLength(1); j++)
            {
                var end = 0;
                for (var i = 0; i < arr.GetLength(0); i++)
                {
                    if (arr[i,j] == 1)
                    {
                        end = i + 1;
                    }
                    if (arr[i, j] == 2)
                    {
                        arr[i, j] = 0;
                        arr[end, j] = 2;
                        end++;
                    }
                }
            }
            return arr;
        }

        private static int[,] TiltEast(int[,] arr)
        {
            for (var j = 0; j < arr.GetLength(1); j++)
            {
                var end = arr.GetLength(0) - 1;
                for (var i = arr.GetLength(0) - 1; i >= 0; i++)
                {
                    if (arr[i, j] == 1)
                    {
                        end = i - 1;
                    }
                    if (arr[i, j] == 2)
                    {
                        arr[i, j] = 0;
                        arr[end, j] = 2;
                        end--;
                    }
                }
            }
            return arr;
        }

        private static int[,] TiltSouth(int[,] arr)
        {
            for (var j = 0; j < arr.GetLength(0); j++)
            {
                var end = arr.GetLength(1) - 1;
                for (var i = arr.GetLength(1) - 1; i >=0; i--)
                {
                    if (arr[j, i] == 1)
                    {
                        end = i - 1;
                    }
                    if (arr[j, i] == 2)
                    {
                        arr[j, i] = 0;
                        arr[j, end] = 2;
                        end--;
                    }
                }
            }
            return arr;
        }

        private static int CountNorth(int[,] arr)
        {
            var sum = 0;
            for (var j = 0; j < arr.GetLength(0); j++)
            {
                for (var i = arr.GetLength(1) - 1; i >= 0; i--)
                {
                    if (arr[j,i]==2)
                    {
                        sum+=arr.GetLength(1) - i;
                    }
                }
            }
            return sum;
        }
    }
}