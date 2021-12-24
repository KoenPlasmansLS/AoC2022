namespace AoC2022
{
    public class Solution47 : IProvideSolution
    {
        private List<int> addX = new();
        private List<int> divZ = new();
        private List<int> addY = new();
        private List<long> multZ = new();

        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution24.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);            
        }

        private string BaseAlgorithm(string[] lines)
        {
            for (var j = 0; j < 14; j++)
            {
                divZ.Add(int.Parse(lines[(18 * j) + 4].Split(" ")[2]));
                addX.Add(int.Parse(lines[(18 * j) + 5].Split(" ")[2]));
                addY.Add(int.Parse(lines[(18 * j) + 15].Split(" ")[2]));
            }
            for (int j = 0; j < divZ.Count; j++)
            {
                multZ.Add(divZ.Skip(j).Aggregate((long) 1, (a, b) => a * b));
            }
            var i = Find(0, 0);
            return i.Value.ToString();
        }

        private long CalcZ(int serialIndex, long previousZ, long newInput)
        {
            long z = previousZ;
            long x = addX[serialIndex] + z % 26;
            z /= divZ[serialIndex];
            if (x != newInput)
            {
                z *= 26;
                z += newInput + addY[serialIndex];
            }
            return z;
        }

        private long? Find(int serialIndex, long previousZ)
        {
            if (serialIndex >= 14)
            {
                if (previousZ == 0) return 0;
                return null;
            }

            if (previousZ > multZ[serialIndex])
            {
                return null;
            }
            long nextX = addX[serialIndex] + previousZ % 26;

            
            long nextZ;
            if (0 < nextX && nextX < 10)
            {
                nextZ = CalcZ(serialIndex, previousZ, nextX);
                var found = Find(serialIndex + 1, nextZ);
                if (found.HasValue)
                {
                    found = found.Value + (long) (nextX * Math.Pow(10, 13 - serialIndex));
                    return found;
                }
            }
            else
            {
                for (var i = 9; i > 0; i--)
                {
                    nextZ = CalcZ(serialIndex, previousZ, i);
                    var found = Find(serialIndex + 1, nextZ);
                    if (found.HasValue)
                    {
                        found = found.Value + (long) (i * Math.Pow(10, 13 - serialIndex));
                        return found;
                    }
                }
            }
            return null;
        }
    }
}