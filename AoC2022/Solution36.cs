namespace AoC2022
{
    public class Solution36 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution18.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var lst = new List<TreeNode>();
            long max = 0;
            foreach(var line in lines)
            {
                lst.Add(Solution35.Parse(line.Trim()));
            }
            foreach(var tree in lst)
            {
                foreach(var otherTree in lst)
                {
                    if (tree != otherTree)
                    {
                        var newTree = Solution35.Merge(tree.Clone(null), otherTree.Clone(null));
                        var value = newTree.GetValue();
                        if (value > max)
                        {
                            max = value;
                        }
                    }
                }
            }
            return max.ToString();
        }
    }
}