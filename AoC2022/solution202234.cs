using System.Drawing;
using System.Text;

namespace AoC2022
{
    public class Solution202234 : IProvideSolution
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

            
            var max = -1;
            var block = 0;
            var i = 0;
            var posblockX = 2;
            var posblockY = 3;
            var pattern = lines[0].Trim();
            Cyclotron ctron = new Cyclotron(pattern.Length);
            var arr = new bool[7, 5000];
            int j = 1;
            while(j <= 1000000000000)
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
                                arr[posblockX + k, (posblockY) + l] = true;
                            }
                        }
                    }
                    posblockX = 2;
                    posblockY = max + 4;
                    j++;

                    if (ctron.ready(j, 1000000000000, block, i, max)) break;
                    block = (block + 1) % 5;

                }
                i = (i + 1) % pattern.Length;
            }
          
            return (max + 1 + ctron.cycle_height + 2).ToString();
        }

        private static bool Overlaps(bool[,] shape, bool[,] arr, int x, int y)
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

    public class Cyclotron
    {
        int[] last_seen, last_height;
        public long cycle_len = 0, last_diff = 0, cycle_height = 0;
        public Cyclotron(int size)
        {
            last_seen = new int[size * 32];
            last_height = new int[size * 32];
        }
        public bool ready(int round, long rounds, int p, int w, int h)
        {
            int code = (w << 5) + p;
            if (last_seen[code] != 0)
            {
                long dt = round - last_seen[code];
                long dh = h - last_height[code];
                if (last_diff == 0) last_diff = dt;
                if (last_diff == dt) cycle_len++;
                if (cycle_len > 10 && (round % last_diff) == ((rounds - 1) % last_diff))
                {
                    long nrounds = (rounds - round) / last_diff;
                    cycle_height = nrounds * dh;
                    return true;
                }
            }
            else cycle_len = last_diff = 0;
            last_seen[code] = round;
            last_height[code] = h;
            return false;
        }
    }
}