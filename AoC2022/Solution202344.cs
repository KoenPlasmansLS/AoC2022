using System.Drawing;

namespace AoC2022
{
    public class Solution202344 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution22.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] input)
        {
            var lst = new List<Block>();
            var i = 0;
            foreach(var line in input)
            {
                var splt = line.Trim().Split("~");
                var splt1 = splt[0].Split(",");
                var splt2 = splt[1].Split(",");
                lst.Add(new Block(int.Parse(splt1[0]), int.Parse(splt1[1]), int.Parse(splt1[2]), int.Parse(splt2[0]), int.Parse(splt2[1]), int.Parse(splt2[2]), i));
                i++;
            }
            var sorted = lst.OrderBy(x => x.Z);
            var minX = sorted.Min(x => Math.Min(x.X, x.X2));
            var maxX = sorted.Max(x => Math.Max(x.X, x.X2));
            var minY = sorted.Min(x => Math.Min(x.Y, x.Y2));
            var maxY = sorted.Max(x => Math.Max(x.Y, x.Y2));
            var minZ = sorted.Min(x => Math.Min(x.Z, x.Z2));
            var maxZ = sorted.Max(x => Math.Max(x.Z, x.Z2));
            var arr = new Block?[maxX+1, maxY+1, maxZ+1];

            foreach (var block in sorted)
            {
                while(CanDrop(arr, block))
                {
                    block.Drop();
                }
            }
            var sum = 0;
            foreach (var block in sorted)
            {
                sum += GoUp(block);
            }
            return sum.ToString();
        }

        private int GoUp(Block block)
        {
            var queue = new Queue<Block>(new Block[] { block });
            var falls = new HashSet<int>(new int[] { block.Nr });

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                foreach (var supported in next.Supports)
                {
                    if (supported.SupportedBy.All(x => falls.Contains(x.Nr)))
                    {
                        queue.Enqueue(supported);
                        falls.Add(supported.Nr);
                    }
                }
            }

            return falls.Count - 1;
        }

        private static bool CanDrop(Block?[,,] arr, Block block)
        {
            var found = false;
            if (block.Z == 1)
            {
                found = true;
            }
            else
            {
                for (var i = block.X; i <= block.X2; i++)
                {
                    for (var j = block.Y; j <= block.Y2; j++)
                    {
                        var blocki = arr[i, j, block.Z - 1];
                        if (blocki != null)
                        {
                            found = true;
                            if (!block.SupportedBy.Contains(blocki))
                            {
                                block.SupportedBy.Add(blocki);
                                blocki.Supports.Add(block);
                            }
                        }
                    }
                }
            }
            if (found)
            {
                for (var i = block.X; i <= block.X2; i++)
                {
                    for (var j = block.Y; j <= block.Y2; j++)
                    {
                        for (var k = block.Z; k <= block.Z2; k++)
                        {
                            arr[i, j, k] = block;
                        }
                    }
                }
            }
            return !found;
        }

        public class Block
        {
            public int X;
            public int Y;
            public int Z;
            public int X2;
            public int Y2;
            public int Z2;
            public int Nr;

            public Block(int x, int y, int z, int x2, int y2, int z2, int i)
            {
                X = x;
                Y = y;
                Z = z;
                X2 = x2;    
                Y2 = y2;
                Z2 = z2;
                Nr = i;
            }

            public List<Block> Supports = new List<Block>();
            public List<Block> SupportedBy = new List<Block>();

            public void Drop()
            {
                Z = Z - 1;
                Z2 = Z2 - 1;
            }
        }
    }
}