namespace AoC2022
{
    public class Solution20229 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution5.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            var first = true;
            var lst = new List<Stack<char>>();
            for (var i = 0; i < lines[0].Length; i+=4)
            {
                lst.Add(new Stack<char>());
            }
            for (var j = 0; j < lines.Count(); j++)
            {
                if (string.IsNullOrEmpty(lines[j]))
                {
                } 
                else if (lines[j].Length == 1)
                { 
                    first = false;
                    for (var i = 0; i < lst.Count; i += 1)
                    {
                        lst[i] = new Stack<char>(lst[i]);
                    }
                }
                else if (first)
                {
                    for (var k = 1; k < lines[j].Length; k +=4)
                    {
                        if (lines[j][k] == '1') break;

                        if (!string.IsNullOrWhiteSpace(lines[j][k].ToString()))
                        {
                            lst[(k-1)/4].Push(lines[j][k]);
                        }
                    }
                }
                else
                {
                    var splt = lines[j].Split(" ");
                    //move 5 from 3 to 6
                    for(var t = 0; t < int.Parse(splt[1]); t++)
                    {
                        var cm = lst[int.Parse(splt[3])-1].Pop();
                        lst[int.Parse(splt[5])-1].Push(cm);
                    }
                }
            }

            var str = "";
            for (var i = 0; i < lst.Count; i++)
            {
                str += lst[i].Pop();
            }
            return str;
        }
    }
}