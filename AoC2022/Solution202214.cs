namespace AoC2022
{
    public class Solution202214 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution7.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            Folder202214 currentFolder = null;
            Folder202214 root = null;
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("$ ls"))
                {
                }
                else if (line.Trim().StartsWith("dir "))
                {
                }
                else if (line.Trim().StartsWith("$ cd "))
                {
                    if (line.Trim() == "$ cd ..")
                    {
                        currentFolder = currentFolder.Parent;
                    }
                    else
                    {
                        var folder = new Folder202214();
                        folder.Name = line.Trim().Split(" ")[2];
                        if (currentFolder == null)
                        {
                            currentFolder = folder;
                            root = currentFolder;
                        }
                        else
                        {
                            currentFolder.Folders.Add(folder);
                            folder.Parent = currentFolder;
                            currentFolder = folder;
                        }
                    }
                }
                else
                {
                    currentFolder.Size += int.Parse(line.Trim().Split(" ")[0]);
                }
            }
            (long dirSum, long allSum) = CalculateSize(root);
            var neededToDelete = 30000000 - (70000000 - dirSum);
            (long dirSum2, long toDelete) = CalculateToDelete(root, neededToDelete);
            return toDelete.ToString();
        }

        public static (long, long) CalculateToDelete(Folder202214 folder, long neededToDelete)
        {
            long thisDirectSum = 0;
            long thisToDelete = long.MaxValue;
            foreach (var foldr in folder.Folders)
            {
                (long directSum, long toDelete) = CalculateToDelete(foldr, neededToDelete);
                thisDirectSum += directSum;
                if (toDelete < thisToDelete)
                {
                    thisToDelete = toDelete;
                }
            }

            thisDirectSum += folder.Size;
            if (thisDirectSum >= neededToDelete && thisDirectSum < thisToDelete)
            {
                thisToDelete = thisDirectSum;
            }
            return (thisDirectSum, thisToDelete);
        }

        public static (long, long) CalculateSize(Folder202214 folder)
        {
            long thisDirectSum = 0;
            long thisAllSum = 0;

            foreach (var foldr in folder.Folders)
            {
                (long directSum, long allSum) = CalculateSize(foldr);
                thisDirectSum += directSum;
                thisAllSum += allSum;
            }

            thisDirectSum += folder.Size;
            if (thisDirectSum <= 100000)
            {
                thisAllSum += thisDirectSum;
            }
            return (thisDirectSum, thisAllSum);
        }
    }

    public class Folder202214
    {
        public Folder202214 Parent { get; set; }
        public List<Folder202214> Folders { get; } = new List<Folder202214>();
        public long Size { get; set; }

        public string Name { get; set; }
    }
}