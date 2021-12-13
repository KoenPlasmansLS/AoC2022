namespace AoC2022
{
    public class Solution23 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution12.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            var start = GetGraph(lines);
            var sum = GetPaths(start, new List<Node>());
            return sum.ToString();
        }

        private int GetPaths(Node currentNode, List<Node> visited)
        {
            if (currentNode.Name == "end") return 1;

            var sum = 0;
            foreach(var linkNode in currentNode.Links)
            {
                if (linkNode.IsLarge || !visited.Contains(linkNode))
                {
                    var lst = visited.ToList();
                    lst.Add(currentNode);
                    sum += GetPaths(linkNode, lst);
                }
            }
            return sum;
        }

        public static Node GetGraph(string[] lines)
        {
            var dict = new Dictionary<string, Node>
            {
                { "start", new Node { Name = "start" } },
                { "end", new Node { Name = "end" } }
            };
            foreach (var line in lines)
            {
                var left = line.Split('-')[0];
                var right = line.Split('-')[1].Trim();
                var leftNode = GetOrAdd(dict, left);
                var rightNode = GetOrAdd(dict, right);
                leftNode.AddNode(rightNode);
            }
            return dict["start"];
        }

        public static Node GetOrAdd(Dictionary<string, Node> dict, string nodeName)
        {
            if (!dict.ContainsKey(nodeName)) dict.Add(nodeName, new Node { Name = nodeName });
            return dict[nodeName];
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public List<Node> Links { get; set; } = new List<Node> { };
        public bool IsLarge 
        {
            get 
            { 
                return Name.ToUpper().Equals(Name);
            } 
        }

        public void AddNode(Node node)
        {
            Links.Add(node);
            node.Links.Add(this);
        }
    }
}