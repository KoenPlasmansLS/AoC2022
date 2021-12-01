namespace AoC2022
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var line = Console.ReadLine();
            while (line?[0] != 'q')
            {
                if (int.TryParse(line, out int solution))
                {
                    var type = Type.GetType($"AoC2022.Solution{solution}");
                    if (type != null)
                    {
                        var sol = Activator.CreateInstance(type) as IProvideSolution;
                        if (sol != null)
                        {
                            Console.WriteLine($"Solution{solution}:{sol.GetSolution()}");
                        }
                        else
                        {
                            Console.WriteLine("Sorry, something went wrong when creating the solution object.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sorry, type not found.");
                    }
                }

                line = Console.ReadLine();
            }
        }
    }
}