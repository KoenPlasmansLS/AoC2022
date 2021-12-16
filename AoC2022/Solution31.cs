namespace AoC2022
{
    public class Solution31 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution16.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var sum = 0;
            foreach(var line in lines)
            {
                var trimLine = line.Trim();
                var bits = new bool[4 * trimLine.Length];
                for(var i = 0; i < trimLine.Length; i++)
                {
                    var nr = GetHexVal(trimLine[i]);
                    if (nr >> 3 == 1) bits[4 * i] = true;
                    if ((nr % 8) >> 2 == 1) bits[4 * i+ 1] = true;
                    if ((nr % 4) >> 1 == 1) bits[4 * i + 2] = true;
                    if ((nr % 2) == 1) bits[4 * i + 3] = true;
                }
                var pack = Parse(bits);
                sum += pack.GetSumVersion();
            }
            return sum.ToString();
        }

        public static int GetHexVal(char hex)
        {
            int val = hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public static Package Parse(bool[] bits)
        {
            var version = GetInt(bits[0..3]);
            var type = GetInt(bits[3..6]);
            if (type == 4)
            {
                return ParseLiteral(bits, version, type);
            } 
            else
            {
                return ParseOperator(bits, version, type);
            }
        }

        private static LiteralPackage ParseLiteral(bool[] bits, int version, int type)
        {
            var literalPackage = new LiteralPackage { Version = version, Type = type };
            var i = 6;
            long literal = 0;
            do
            {
                literal *= 16;
                literal += GetInt(bits[(i + 1)..(i + 5)]);
                i += 5;
            }
            while (bits[i - 5]);
            literalPackage.Literal = literal;
            literalPackage.LengthBits = i;
            return literalPackage;
        }

        private static OperatorPackage ParseOperator(bool[] bits, int version, int type)
        {
            var opPackage = new OperatorPackage { Version = version, Type = type };
            opPackage.LengthType = bits[6];
            if (opPackage.LengthType)
            {
                opPackage.Length = GetInt(bits[7..18]);
                var begin = 18;
                while(opPackage.Packages.Count < opPackage.Length)
                {
                    var package = Parse(bits[begin..]);
                    opPackage.Packages.Add(package);
                    begin += package.LengthBits;
                }
                opPackage.LengthBits = begin;
            } 
            else
            {
                opPackage.Length = GetInt(bits[7..22]);
                var begin = 22;
                while (begin - 22 < opPackage.Length)
                {
                    var package = Parse(bits[begin..]);
                    opPackage.Packages.Add(package);
                    begin += package.LengthBits;
                }
                opPackage.LengthBits = begin;
            }
            return opPackage;
        }

        private static int GetInt(bool[] bits)
        {
            var i = 0;
            for(var j = 0; j < bits.Length; j++)
            {
                i *= 2;
                i += bits[j] ? 1 : 0;
            }
            return i;
        }
    }

    public class OperatorPackage : Package
    {
        public bool LengthType { get; set; }
        public int Length { get; set; }
        public List<Package> Packages { get; } = new List<Package>();

        public override int GetSumVersion()
        {
            var sum = Version;
            foreach(var package in Packages)
            {
                sum += package.GetSumVersion();
            }
            return sum;
        }

        public override long GetValue()
        {
            if (Type == 0) return Packages.Sum(x => x.GetValue());
            if (Type == 1)
            {
                long prod = 1;
                foreach(var p in Packages)
                {
                    prod *= p.GetValue();
                }
                return prod;
            }
            if (Type == 2) return Packages.Min(x => x.GetValue());
            if (Type == 3) return Packages.Max(x => x.GetValue());
            if (Type == 5) return Packages[0].GetValue() > Packages[1].GetValue() ? 1 : 0;
            if (Type == 6) return Packages[0].GetValue() < Packages[1].GetValue() ? 1 : 0;
            if (Type == 7) return Packages[0].GetValue() == Packages[1].GetValue() ? 1 : 0;
            return 0;
        }
    }

    public abstract class Package
    {
        public int Version { get; set; }
        public int Type { get; set; }
        public int LengthBits { get; set; }
        public abstract int GetSumVersion();

        public abstract long GetValue();
    }

    public class LiteralPackage: Package
    {
        public long Literal { get; set; }

        public override int GetSumVersion()
        {
            return Version;
        }

        public override long GetValue()
        {
            return Literal;
        }
    }
}