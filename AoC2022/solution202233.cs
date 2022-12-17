namespace AoC2022
{
    public class Solution202233 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution17.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var blocks = new List<Block>();
            var b = new bool[4, 1];
            b[0, 0] = true;
            b[1, 0] = true;
            b[2, 0] = true;
            b[3, 0] = true;
            blocks.Add(new Block { Shape = b });
            var b2 = new bool[3,3];
            b2[0, 1] = true;
            b2[1, 0] = true;
            b2[1, 1] = true;
            b2[2, 1] = true;
            b2[1, 2] = true;
            blocks.Add(new Block { Shape = b2 });
            var b3 = new bool[3, 3];
            b3[2, 0] = true;
            b3[2, 1] = true;
            b3[2, 2] = true;
            b3[1, 0] = true;
            b3[0, 0] = true;
            blocks.Add(new Block { Shape = b3 });
            var b4 = new bool[1, 4];
            b4[0, 0] = true;
            b4[0, 1] = true;
            b4[0, 2] = true;
            b4[0, 3] = true;
            blocks.Add(new Block { Shape = b4 });
            var b5 = new bool[2, 2];
            b5[0, 0] = true;
            b5[0, 1] = true;
            b5[1, 0] = true;
            b5[1, 1] = true;
            blocks.Add(new Block { Shape = b5 });


            long max = 0;
            var block = 0;
            var i = 0;
            var posblockX = 2;
            long posblockY = 3;
            var pattern = lines[0].Trim();
            var arr = new bool[7, 5000];
            long j = 1;
            while(j <= 2022)
            {
                var patt = pattern[i];
                if (patt == '>' && posblockX + blocks[block].Shape.GetLength(0) < 7)
                {
                    posblockX += 1;
                    if (Overlaps(blocks[block].Shape, arr, posblockX, posblockY))
                    {
                        posblockX-=1;
                    }
                }
                else if (patt == '<' && posblockX > 0)
                {
                    posblockX -= 1;
                    if (Overlaps(blocks[block].Shape, arr, posblockX, posblockY))
                    {
                        posblockX += 1;
                    }
                }
                posblockY--;
                var fallen = false;
                if (posblockY == -1)
                {
                    posblockY = 0;
                    fallen = true;
                }
                else
                {
                    if (Overlaps(blocks[block].Shape, arr, posblockX, posblockY))
                    {
                        posblockY += 1;
                        fallen = true;
                    }            
                }

                if (fallen)
                {
                    for (var k = 0; k < blocks[block].Shape.GetLength(0); k++)
                    {
                        for (var l = 0; l < blocks[block].Shape.GetLength(1); l++)
                        {
                            if (blocks[block].Shape[k, l])
                            {
                                if (posblockY + l > max)
                                {
                                    max = posblockY + l;
                                }
                                arr[posblockX + k, posblockY + l] = true;
                            }
                        }
                    }
                    posblockX = 2;
                    posblockY = max + 4;
                    j++;
                    block = (block + 1) % 5;
                }
                i = (i + 1) % pattern.Length;
            }
          
            return (max + 1).ToString();
        }

        private static bool Overlaps(bool[,] shape, bool[,] arr, int x, long y)
        {
            var fallen = false;
            for (var k = 0; k < shape.GetLength(0); k++)
            {
                for (var l = 0; l < shape.GetLength(1); l++)
                {
                    if (shape[k, l] && arr[k + x, l + y])
                    {
                        fallen = true;
                        break;
                    }
                }
                if (fallen)
                {
                    break;
                }
            }
            return fallen;
        }
    }

    public class Block
    {
        public bool[,] Shape { get; set; }
    }
}