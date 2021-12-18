namespace AoC2022
{
    public class Solution35 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution18.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        private string BaseAlgorithm(string[] lines)
        {
            TreeNode tree = null;
            foreach(var line in lines)
            {
                var newTree = Parse(line.Trim());
                Console.Write("  ");
                Print(tree);
                Console.Write("+ ");
                Print(newTree);
                tree = Merge(tree, newTree);
                Console.Write("= ");
                Print(tree);
                Console.WriteLine();
            }
            return tree.GetValue().ToString();
        }

        private void Print(TreeNode tree)
        {
            tree?.Print();
            Console.WriteLine();
        }

        public static TreeNode Parse(string str)
        {
            TreeNode currentNode = null;
            var i = 0;
            while(i < str.Length - 1)
            {
                if (str[i] == '[')
                {
                    var newNode = new CompositeNode();
                    FillParent(currentNode, newNode);
                    currentNode = newNode;
                }
                else if (str[i] == ']')
                {
                    currentNode = currentNode.Parent;
                }
                else if (int.TryParse(str[i].ToString(), out int result))
                {
                    var newNode = new Leaf();
                    FillParent(currentNode, newNode);
                    newNode.Value = result;
                }
                i++;
            }
            return currentNode;
        }

        private static void FillParent(TreeNode currentNode, TreeNode newNode)
        {
            if (currentNode == null) return;
            var currentCompNode = currentNode as CompositeNode;
            if (currentCompNode?.LeftNode == null)
            {
                currentCompNode.LeftNode = newNode;           
            } 
            else
            {
                currentCompNode.RightNode = newNode;
            }
            newNode.Parent = currentCompNode;
        }

        public static TreeNode Merge(TreeNode left, TreeNode right)
        {
            if (left == null) return right;
            var compositeNode = new CompositeNode();
            compositeNode.LeftNode = left;
            compositeNode.RightNode = right;
            left.Parent = compositeNode;
            right.Parent = compositeNode;
            var broken = true;
            while(broken)
            {
                broken = TraverseTree1(compositeNode, 0);
                if (!broken)
                {
                    broken = TraverseTree2(compositeNode);
                }
            }
            return compositeNode;
        }

        private static bool TraverseTree1(TreeNode node, int depth)
        {
            var comp = node as CompositeNode;
            if (comp != null)
            {
                var broken = TraverseTree1(comp.LeftNode, depth + 1);
                if (!broken)
                {
                    if (depth >= 4 && comp != null)
                    {
                        FindLeftUp(comp.Parent, comp, comp.LeftNode as Leaf);
                        FindRightUp(comp.Parent, comp, comp.RightNode as Leaf);

                        var newNode = new Leaf { Value = 0 };
                        if (node.Parent.LeftNode == node) { node.Parent.LeftNode = newNode; }
                        if (node.Parent.RightNode == node) { node.Parent.RightNode = newNode; }
                        newNode.Parent = node.Parent;
                        return true;
                    }
                    broken = TraverseTree1(comp.RightNode, depth + 1);
                }
                return broken;
            }
            return false;
        }

        private static bool TraverseTree2(TreeNode node)
        {
            var leaf = node as Leaf;
            var comp = node as CompositeNode;
            if (leaf != null && leaf.Value >= 10)
            {
                var newNode = new CompositeNode();
                newNode.LeftNode = new Leaf { Value = (int)Math.Floor(((double)leaf.Value) / 2) };
                newNode.RightNode = new Leaf { Value = (int)Math.Ceiling(((double)leaf.Value) / 2) };
                newNode.LeftNode.Parent = newNode;
                newNode.RightNode.Parent = newNode;
                if (node.Parent.LeftNode == node) { node.Parent.LeftNode = newNode; }
                if (node.Parent.RightNode == node) { node.Parent.RightNode = newNode; }
                newNode.Parent = node.Parent;
                return true;
            }

            if (comp != null)
            {
                var broken = TraverseTree2(comp.LeftNode);
                if (!broken)
                {
                    broken = TraverseTree2(comp.RightNode);
                }
                return broken;
            }
            return false;
        }

        private static void FindLeftUp(CompositeNode parent, TreeNode previous, Leaf toAttach)
        {
            if (parent != null)
            {
                if (parent.LeftNode == previous)
                {
                    FindLeftUp(parent.Parent, parent, toAttach);
                } 
                else
                {
                    FindLeftDown(parent.LeftNode, toAttach);
                }
            }
        }

        private static void FindLeftDown(TreeNode node, Leaf toAttach)
        {
            var leaf = node as Leaf;
            var com = node as CompositeNode;
            if (leaf != null) leaf.Value += toAttach.Value;
            else
            {
                FindLeftDown(com.RightNode, toAttach);
            }
        }

        private static void FindRightUp(CompositeNode parent, TreeNode previous, Leaf toAttach)
        {
            if (parent != null)
            {
                if (parent.RightNode == previous)
                {
                    FindRightUp(parent.Parent, parent, toAttach);
                }
                else
                {
                    FindRightDown(parent.RightNode, toAttach);
                }
            }
        }

        private static void FindRightDown(TreeNode node, Leaf toAttach)
        {
            var leaf = node as Leaf;
            var com = node as CompositeNode;
            if (leaf != null) leaf.Value += toAttach.Value;
            else
            {
                FindRightDown(com.LeftNode, toAttach);
            }
        }
    }

    public class CompositeNode : TreeNode
    {
        public TreeNode LeftNode { get; set; }
        public TreeNode RightNode { get; set; }

        public override long GetValue()
        {
            return LeftNode.GetValue() * 3 + RightNode.GetValue() * 2;
        }

        public override void Print()
        {
            Console.Write("[");
            LeftNode.Print();
            Console.Write(",");
            RightNode.Print();
            Console.Write("]");
        }

        public override TreeNode Clone(CompositeNode parent)
        {
            var treeNode = new CompositeNode();
            treeNode.LeftNode = LeftNode.Clone(treeNode);
            treeNode.RightNode = RightNode.Clone(treeNode);
            treeNode.Parent = parent;
            return treeNode;
        }
    }

    public abstract class TreeNode
    {
        public abstract void Print();
        public abstract long GetValue();
        public CompositeNode Parent { get; set; }

        public abstract TreeNode Clone(CompositeNode parent);
    }

    public class Leaf : TreeNode
    {
        public int Value { get; set; }

        public override long GetValue()
        {
            return Value;
        }

        public override void Print()
        {
            Console.Write(Value);
        }

        public override TreeNode Clone(CompositeNode parent)
        {
            var leaf = new Leaf { Value = Value };
            leaf.Parent = parent;
            return leaf;
        }
    }
}