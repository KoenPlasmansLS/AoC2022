namespace AoC2022
{
    public class Solution202213 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2022solution7.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected static string BaseAlgorithm(string[] lines)
        {
            Folder currentFolder = null;
            Folder root = null;
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
                        var folder = new Folder();
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
            return allSum.ToString();
        }

        public static (long, long) CalculateSize(Folder folder)
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

    public class Folder
    {
        public Folder Parent { get; set; }
        public List<Folder> Folders { get; } = new List<Folder>();
        public long Size { get; set; }

        public string Name { get; set; }
    }
}