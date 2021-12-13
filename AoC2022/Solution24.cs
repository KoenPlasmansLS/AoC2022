namespace AoC2022
{
    public class Solution24 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution12.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var start = Solution23.GetGraph(lines);
            var sum = GetPaths(start, new List<Node>(), false);
            return sum.ToString();
        }

        private int GetPaths(Node currentNode, List<Node> visited, bool wildCard)
        {
            if (currentNode.Name == "end") return 1;

            var sum = 0;
            foreach(var linkNode in currentNode.Links)
            {
                var canI = false;
                var usingWildCard = wildCard;
                if (linkNode.IsLarge || !visited.Contains(linkNode))
                {
                    canI = true;
                }
                else if (!usingWildCard && !linkNode.Name.Equals("start") && !linkNode.Name.Equals("end"))
                {
                    usingWildCard = true;
                    canI = true;
                }
                if (canI)
                {
                    var lst = visited.ToList();
                    lst.Add(currentNode);
                    sum += GetPaths(linkNode, lst, usingWildCard);
                }
            }
            return sum;
        }
    }
}