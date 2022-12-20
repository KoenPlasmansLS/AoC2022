namespace AoC2022
{
    public class Solution202239 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution20.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var lst = new List<(int, int)>();
            var i = 0;
            foreach (var line in lines)
            {
                lst.Add((i, int.Parse(line.Trim())));
                i++;
            }
            var copy = lst.ToList();
            var length = copy.Count - 1;
            foreach ((var index, var nr) in lst)
            {
                var oldindex = copy.IndexOf((index, nr));
                copy.RemoveAt(oldindex);
                var newindex = (nr + oldindex) % length;
                while (newindex < 0)
                {
                    newindex += length;
                }
                copy.Insert(newindex, (index, nr));
            }
            var zero = lst.First(i => i.Item2 == 0);
            var indexZero = copy.IndexOf(zero);
            var x = copy[(indexZero + 1000) % lst.Count];
            var y = copy[(indexZero + 2000) % lst.Count];
            var Z = copy[(indexZero + 3000) % lst.Count];
            return (x.Item2 + y.Item2 + Z.Item2).ToString();
        }

        private static List<(int Index, long Value)> GetInitialFile(List<string> input, long decryptionKey)
        {
            return input.Select((i, index) => (Index: index, Value: long.Parse(i) * decryptionKey)).ToList();
        }

        private static long GetEncryptedCoordinates(List<(int Index, long Value)> initialList, int numberOfMixes)
        {
            var numbers = new List<(int Index, long Value)>(initialList);
            var length = numbers.Count - 1;
            for (var i = 0; i < numberOfMixes; ++i)
            {
                foreach (var (index, value) in initialList)
                {
                    var pos = numbers.IndexOf((index, value));
                    var newPos = (int)((pos + value) % length);
                    if (newPos < 0)
                    {
                        newPos += length;
                    }

                    numbers.RemoveAt(pos);
                    numbers.Insert(newPos, (index, value));
                }
            }

            var zero = initialList.First(i => i.Value == 0);
            var zeroPos = numbers.IndexOf(zero);
            var sum = numbers[(zeroPos + 1000) % numbers.Count].Value;
            sum += numbers[(zeroPos + 2000) % numbers.Count].Value;
            sum += numbers[(zeroPos + 3000) % numbers.Count].Value;

            return sum;
        }
    }
}