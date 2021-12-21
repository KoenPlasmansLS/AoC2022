namespace AoC2022
{
    public class Solution42 : IProvideSolution
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
            var dict1 = new Dictionary<int, long>(); //steps, many for 1
            var dict2 = new Dictionary<int, long>(); //steps, many for 2
            
            var q1 = new List<GameState>();
            q1.Add(new GameState(p1, 0, 1));
            var step = 0;
            while(q1.Count > 0)
            {
                step++;
                dict1.Add(step, 0);
                var newq1 = new List<GameState>();
                foreach(var q in q1)
                {
                    var lst = GetNextState(q);
                    foreach(var l in lst)
                    {
                        if (l.score1 >= 21)
                        {
                            dict1[step] = dict1[step] + l.many;
                        } 
                        else
                        {
                            newq1.Add(l);
                        }
                    }
                }
                q1 = newq1;
            }

            q1 = new List<GameState>();
            q1.Add(new GameState(p2, 0, 1));
            step = 0;
            while (q1.Count > 0)
            {
                step++;
                dict2.Add(step, 0);
                var newq1 = new List<GameState>();
                foreach (var q in q1)
                {
                    var lst = GetNextState(q);
                    foreach (var l in lst)
                    {
                        if (l.score1 >= 21)
                        {
                            dict2[step] = dict2[step] + l.many;
                        }
                        else
                        {
                            newq1.Add(l);
                        }
                    }
                }
                q1 = newq1;
            }


            long sum1 = 0;
            long sum2 = 0;
            long sumUniversesNotWon1 = 1;
            long sumUniversesNotWon2 = 1;
            for (var i = 1; i <= dict1.Keys.Max(); i++)
            {
                sumUniversesNotWon1 *= 27;
                if (dict1.ContainsKey(i))
                {
                    sum1 += dict1[i] * sumUniversesNotWon2;
                    sumUniversesNotWon1 -= dict1[i];
                }
                sumUniversesNotWon2 *= 27;
                if (dict2.ContainsKey(i))
                {
                    sum2 += dict2[i] * sumUniversesNotWon1;
                    sumUniversesNotWon2 -= dict2[i];
                }
            }
            return ((sum1 > sum2) ? sum1 : sum2).ToString();
        }

        private List<GameState> GetNextState(GameState next)
        {
            var lst = new List<GameState>();
            var nextV3 = (next.p1 + 3) % 10;
            var newRoll3 = new GameState(nextV3, next.score1 + (nextV3 == 0 ? 10 : nextV3), next.many);
            lst.Add(newRoll3);
            var nextV4 = (next.p1 + 4) % 10;
            var newRoll4 = new GameState(nextV4, next.score1 + (nextV4 == 0 ? 10 : nextV4), next.many * 3);
            lst.Add(newRoll4);
            var nextV5 = (next.p1 + 5) % 10;
            var newRoll5 = new GameState(nextV5, next.score1 + (nextV5 == 0 ? 10 : nextV5), next.many * 6);
            lst.Add(newRoll5);
            var nextV6 = (next.p1 + 6) % 10;
            var newRoll6 = new GameState(nextV6, next.score1 + (nextV6 == 0 ? 10 : nextV6), next.many * 7);
            lst.Add(newRoll6);
            var nextV9 = (next.p1 + 9) % 10;
            var newRoll9 = new GameState(nextV9, next.score1 + (nextV9 == 0 ? 10 : nextV9), next.many);
            lst.Add(newRoll9);
            var nextV8 = (next.p1 + 8) % 10;
            var newRoll8 = new GameState(nextV8, next.score1 + (nextV8 == 0 ? 10 : nextV8), next.many * 3);
            lst.Add(newRoll8);
            var nextV7 = (next.p1 + 7) % 10;
            var newRoll7 = new GameState(nextV7, next.score1 + (nextV7 == 0 ? 10 : nextV7), next.many * 6);
            lst.Add(newRoll7);
            return lst;
        }
    }

    public class GameState
    {
        public int p1 { get; set; }
        public int score1 { get; set; }
        public long many { get; set; }

        public GameState(int p1, int score1, long many)
        {
            this.p1 = p1;
            this.score1 = score1;
            this.many = many;
        }
    }
}