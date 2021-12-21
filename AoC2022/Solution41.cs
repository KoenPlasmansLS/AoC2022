namespace AoC2022
{
    public class Solution41 : IProvideSolution
    {
        public string GetSolution()
        {
            var lines = new FileInfo("Input/solution21.txt").OpenText().ReadToEnd().Split("\n");
            var play1 = int.Parse(lines[0].Split(":")[1].Trim());
            var play2 = int.Parse(lines[1].Split(":")[1].Trim());
            return Play(play1, play2);
        }

        private string Play(int p1, int p2)
        {
            var score1 = 0;
            var score2 = 0;
            var dieRolled = 0;
            var currentDie = 1;
            var loser = 0;
            while(true)
            {
                var val = currentDie;
                currentDie++;
                val += currentDie;
                currentDie++;
                val += currentDie;
                currentDie++;
                p1 += val;
                p1 %= 10;
                score1 += p1 == 0 ? 10 : p1;
                dieRolled += 3;
                if (score1 >= 1000)
                {
                    loser = score2;
                    break;
                }

                val = currentDie;
                currentDie++;
                val += currentDie;
                currentDie++;
                val += currentDie;
                currentDie++;
                p2 += val;
                p2 %= 10;
                score2 += p2 == 0 ? 10 : p2;
                dieRolled += 3;
                if (score2 >= 1000)
                {
                    loser = score1;
                    break;
                }
            }
            return (loser * dieRolled).ToString();
        }
    }
}