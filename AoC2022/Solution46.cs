namespace AoC2022
{
    public class Solution46 : IProvideSolution
    {
        public string GetSolution()
        {
            return BaseAlgorithm();
        }

        private string BaseAlgorithm()
        {
            var visited = new HashSet<RoomState>();
            var queue = new PriorityQueue<RoomState, int>();
            var dictCost = new Dictionary<RoomState, int>();
            var startState = new RoomState
            {
                Room11 = Amphi.C,
                Room12 = Amphi.D,
                Room13 = Amphi.D,
                Room14 = Amphi.B,
                Room21 = Amphi.B,
                Room22 = Amphi.C,
                Room23 = Amphi.B,
                Room24 = Amphi.C,
                Room31 = Amphi.A,
                Room32 = Amphi.B,
                Room33 = Amphi.A,
                Room34 = Amphi.D,
                Room41 = Amphi.D,
                Room42 = Amphi.A,
                Room43 = Amphi.C,
                Room44 = Amphi.A
            };
            dictCost[startState] = 0;
            var goal = new RoomState
            {
                Room11 = Amphi.A,
                Room12 = Amphi.A,
                Room13 = Amphi.A,
                Room14 = Amphi.A,
                Room21 = Amphi.B,
                Room22 = Amphi.B,
                Room23 = Amphi.B,
                Room24 = Amphi.B,
                Room31 = Amphi.C,
                Room32 = Amphi.C,
                Room33 = Amphi.C,
                Room34 = Amphi.C,
                Room41 = Amphi.D,
                Room42 = Amphi.D,
                Room43 = Amphi.D,
                Room44 = Amphi.D,
            };

            queue.Enqueue(startState, 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!visited.Contains(current))
                {
                    visited.Add(current);

                    if (current == goal)
                        break;

                    var currentCost = dictCost[current];

                    var nextMoves = GetNextMoves(current);
                    foreach (var (nextMove, nextCost) in nextMoves)
                    {
                        var alt = currentCost + nextCost;
                        if (alt < dictCost.GetValueOrDefault(nextMove, int.MaxValue))
                        {
                            dictCost[nextMove] = alt;
                            queue.Enqueue(nextMove, alt);
                        }
                    }
                }
            }

            return dictCost[goal].ToString();
        }

        private List<(RoomState, int)> GetNextMoves(RoomState current)
        {
            var lst = new List<(RoomState, int)>();
            if (current.Room11 != null)
            {
                lst.AddRange(FromRoom1(current with { Room11 = null }, current.Room11.Value, 1));
            }
            else if(current.Room12 != null)
            {
                lst.AddRange(FromRoom1(current with { Room12 = null }, current.Room12.Value, 2));
            }
            else if (current.Room13 != null)
            {
                lst.AddRange(FromRoom1(current with { Room13 = null }, current.Room13.Value, 3));
            }
            else if (current.Room14 != null)
            {
                lst.AddRange(FromRoom1(current with { Room14 = null }, current.Room14.Value, 4));
            }

            if (current.Room21 != null)
            {
                lst.AddRange(FromRoom2(current with { Room21 = null }, current.Room21.Value, 1));
            }
            else if (current.Room22 != null)
            {
                lst.AddRange(FromRoom2(current with { Room22 = null }, current.Room22.Value, 2));
            }
            else if (current.Room23 != null)
            {
                lst.AddRange(FromRoom2(current with { Room23 = null }, current.Room23.Value, 3));
            }
            else if (current.Room24 != null)
            {
                lst.AddRange(FromRoom2(current with { Room24 = null }, current.Room24.Value, 4));
            }

            if (current.Room31 != null)
            {
                lst.AddRange(FromRoom3(current with { Room31 = null }, current.Room31.Value, 1));
            }
            else if (current.Room32 != null)
            {
                lst.AddRange(FromRoom3(current with { Room32 = null }, current.Room32.Value, 2));
            }
            else if (current.Room33 != null)
            {
                lst.AddRange(FromRoom3(current with { Room33 = null }, current.Room33.Value, 3));
            }
            else if (current.Room34 != null)
            {
                lst.AddRange(FromRoom3(current with { Room34 = null }, current.Room34.Value, 4));
            }

            if (current.Room41 != null)
            {
                lst.AddRange(FromRoom4(current with { Room41 = null }, current.Room41.Value, 1));
            }
            else if (current.Room42 != null)
            {
                lst.AddRange(FromRoom4(current with { Room42 = null }, current.Room42.Value, 2));
            }
            else if (current.Room43 != null)
            {
                lst.AddRange(FromRoom4(current with { Room43 = null }, current.Room43.Value, 3));
            }
            else if (current.Room44 != null)
            {
                lst.AddRange(FromRoom4(current with { Room44 = null }, current.Room44.Value, 4));
            }

            if (current.Pos1 != null && current.Pos2 == null)
            {
                lst.AddRange(IntoRoom1(current with { Pos1 = null }, current.Pos1.Value, 2));
                if (current.Pos4 == null)
                {
                    lst.AddRange(IntoRoom2(current with { Pos1 = null }, current.Pos1.Value, 4));
                    if (current.Pos6 == null)
                    {
                        lst.AddRange(IntoRoom3(current with { Pos1 = null }, current.Pos1.Value, 6));
                        if (current.Pos8 == null)
                        {
                            lst.AddRange(IntoRoom4(current with { Pos1 = null }, current.Pos1.Value, 8));
                        }
                    }
                }
            }
            if (current.Pos2 != null)
            {
                lst.AddRange(IntoRoom1(current with { Pos2 = null }, current.Pos2.Value, 1));
                if (current.Pos4 == null)
                {
                    lst.AddRange(IntoRoom2(current with { Pos2 = null }, current.Pos2.Value, 3));
                    if (current.Pos6 == null)
                    {
                        lst.AddRange(IntoRoom3(current with { Pos2 = null }, current.Pos2.Value, 5));
                        if (current.Pos8 == null)
                        {
                            lst.AddRange(IntoRoom4(current with { Pos2 = null }, current.Pos2.Value, 7));
                        }
                    }
                }
            }

            if (current.Pos4 != null)
            {
                lst.AddRange(IntoRoom1(current with { Pos4 = null }, current.Pos4.Value, 1));
                lst.AddRange(IntoRoom2(current with { Pos4 = null }, current.Pos4.Value, 1));
                if (current.Pos6 == null)
                {
                    lst.AddRange(IntoRoom3(current with { Pos4 = null }, current.Pos4.Value, 3));
                    if (current.Pos8 == null)
                    {
                        lst.AddRange(IntoRoom4(current with { Pos4 = null }, current.Pos4.Value, 5));
                    }
                }
            }

            if (current.Pos6 != null)
            {
                lst.AddRange(IntoRoom2(current with { Pos6 = null }, current.Pos6.Value, 1));
                lst.AddRange(IntoRoom3(current with { Pos6 = null }, current.Pos6.Value, 1));
                if (current.Pos4 == null)
                {
                    lst.AddRange(IntoRoom1(current with { Pos6 = null }, current.Pos6.Value, 3));
                }
                if (current.Pos8 == null)
                {
                    lst.AddRange(IntoRoom4(current with { Pos6 = null }, current.Pos6.Value, 3));
                }
            }

            if (current.Pos8 != null)
            {
                lst.AddRange(IntoRoom3(current with { Pos8 = null }, current.Pos8.Value, 1));
                lst.AddRange(IntoRoom4(current with { Pos8 = null }, current.Pos8.Value, 1));
                if (current.Pos6 == null)
                {
                    lst.AddRange(IntoRoom2(current with { Pos8 = null }, current.Pos8.Value, 3));
                    if (current.Pos4 == null)
                    {
                        lst.AddRange(IntoRoom1(current with { Pos8 = null }, current.Pos8.Value, 5));
                    }
                }       
            }

            if (current.Pos10 != null)
            {
                lst.AddRange(IntoRoom4(current with { Pos10 = null }, current.Pos10.Value, 1));
                if (current.Pos8 == null)
                {
                    lst.AddRange(IntoRoom3(current with { Pos10 = null }, current.Pos10.Value, 3));
                    if (current.Pos6 == null)
                    {
                        lst.AddRange(IntoRoom2(current with { Pos10 = null }, current.Pos10.Value, 5));
                        if (current.Pos4 == null)
                        {
                            lst.AddRange(IntoRoom1(current with { Pos10 = null }, current.Pos10.Value, 7));
                        }
                    }
                }
            }

            if (current.Pos11 != null && current.Pos10 == null)
            {
                lst.AddRange(IntoRoom4(current with { Pos11 = null }, current.Pos11.Value, 2));
                if (current.Pos8 == null)
                {
                    lst.AddRange(IntoRoom3(current with { Pos11 = null }, current.Pos11.Value, 4));
                    if (current.Pos6 == null)
                    {
                        lst.AddRange(IntoRoom2(current with { Pos11 = null }, current.Pos11.Value, 6));
                        if (current.Pos4 == null)
                        {
                            lst.AddRange(IntoRoom1(current with { Pos11 = null }, current.Pos11.Value, 8));
                        }
                    }
                }
            }

            return lst;
        }

        private List<(RoomState, int)> IntoRoom1(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (currentA == Amphi.A)
            {
                if (current.Room11 == null && current.Room12 == currentA && current.Room13 == currentA && current.Room14 == currentA) lst.Add((current with { Room11 = currentA }, cost * (1 + steps)));
                if (current.Room11 == null && current.Room12 == null && current.Room13 == currentA && current.Room14 == currentA) lst.Add((current with { Room12 = currentA }, cost * (2 + steps)));
                if (current.Room11 == null && current.Room12 == null && current.Room13 == null && current.Room14 == currentA) lst.Add((current with { Room13 = currentA }, cost * (3 + steps)));
                if (current.Room11 == null && current.Room12 == null && current.Room13 == null && current.Room14 == null) lst.Add((current with { Room14 = currentA }, cost * (4 + steps)));
            }
            return lst;
        }

        private List<(RoomState, int)> IntoRoom2(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (currentA == Amphi.B)
            {
                if (current.Room21 == null && current.Room22 == Amphi.B && current.Room23 == Amphi.B && current.Room24 == Amphi.B) lst.Add((current with { Room21 = currentA }, cost * (1 + steps)));
                if (current.Room21 == null && current.Room22 == null && current.Room23 == Amphi.B && current.Room24 == Amphi.B) lst.Add((current with { Room22 = currentA }, cost * (2 + steps)));
                if (current.Room21 == null && current.Room22 == null && current.Room23 == null && current.Room24 == Amphi.B) lst.Add((current with { Room23 = currentA }, cost * (3 + steps)));
                if (current.Room21 == null && current.Room22 == null && current.Room23 == null && current.Room24 == null) lst.Add((current with { Room24 = currentA }, cost * (4 + steps)));
            }
            return lst;
        }

        private List<(RoomState, int)> IntoRoom3(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (currentA == Amphi.C)
            {
                if (current.Room31 == null && current.Room32 == Amphi.C && current.Room33 == Amphi.C && current.Room34 == Amphi.C) lst.Add((current with { Room31 = currentA }, cost * (1 + steps)));
                if (current.Room31 == null && current.Room32 == null && current.Room33 == Amphi.C && current.Room34 == Amphi.C) lst.Add((current with { Room32 = currentA }, cost * (2 + steps)));
                if (current.Room31 == null && current.Room32 == null && current.Room33 == null && current.Room34 == Amphi.C) lst.Add((current with { Room33 = currentA }, cost * (3 + steps)));
                if (current.Room31 == null && current.Room32 == null && current.Room33 == null && current.Room34 == null) lst.Add((current with { Room34 = currentA }, cost * (4 + steps)));
            }
            return lst;
        }

        private List<(RoomState, int)> IntoRoom4(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (currentA == Amphi.D)
            {
                if (current.Room41 == null && current.Room42 == Amphi.D && current.Room43 == Amphi.D && current.Room44 == Amphi.D) lst.Add((current with { Room41 = currentA }, cost * (1 + steps)));
                if (current.Room41 == null && current.Room42 == null && current.Room43 == Amphi.D && current.Room44 == Amphi.D) lst.Add((current with { Room42 = currentA }, cost * (2 + steps)));
                if (current.Room41 == null && current.Room42 == null && current.Room43 == null && current.Room44 == Amphi.D) lst.Add((current with { Room43 = currentA }, cost * (3 + steps)));
                if (current.Room41 == null && current.Room42 == null && current.Room43 == null && current.Room44 == null) lst.Add((current with { Room44 = currentA }, cost * (4 + steps)));
            }
            return lst;
        }

        private List<(RoomState, int)> FromRoom1(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (current.Pos2 == null) lst.Add((current with { Pos2 = currentA }, cost * (1+steps)));
            if (current.Pos1 == null && current.Pos2 == null) lst.Add((current with { Pos1 = currentA }, cost * (2+steps)));

            if (current.Pos4 == null) lst.Add((current with { Pos4 = currentA }, cost * (1 + steps)));
            if (current.Pos6 == null && current.Pos4 == null) lst.Add((current with { Pos6 = currentA }, cost * (3 + steps)));
            if (current.Pos6 == null && current.Pos4 == null && current.Pos8 == null) lst.Add((current with { Pos8 = currentA }, cost * (5 + steps)));
            if (current.Pos6 == null && current.Pos4 == null && current.Pos8 == null && current.Pos10 == null) lst.Add((current with { Pos10 = currentA }, cost * (7 + steps)));
            if (current.Pos6 == null && current.Pos4 == null && current.Pos8 == null && current.Pos10 == null && current.Pos11 == null) lst.Add((current with { Pos11 = currentA }, cost * (8 + steps)));
            return lst;
        }

        private List<(RoomState, int)> FromRoom2(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (current.Pos4 == null) lst.Add((current with { Pos4 = currentA }, cost * (1 + steps)));
            if (current.Pos4 == null && current.Pos2 == null) lst.Add((current with { Pos2 = currentA }, cost * (3 + steps)));
            if (current.Pos4 == null && current.Pos1 == null && current.Pos2 == null) lst.Add((current with { Pos1 = currentA }, cost * (4 + steps)));

            if (current.Pos6 == null) lst.Add((current with { Pos6 = currentA }, cost * (1 + steps)));
            if (current.Pos6 == null && current.Pos8 == null) lst.Add((current with { Pos8 = currentA }, cost * (3 + steps)));
            if (current.Pos6 == null && current.Pos8 == null && current.Pos10 == null) lst.Add((current with { Pos10 = currentA }, cost * (5 + steps)));
            if (current.Pos6 == null && current.Pos8 == null && current.Pos10 == null && current.Pos11 == null) lst.Add((current with { Pos11 = currentA }, cost * (6 + steps)));
            return lst;
        }

        private List<(RoomState, int)> FromRoom3(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (current.Pos6 == null) lst.Add((current with { Pos6 = currentA }, cost * (1 + steps)));
            if (current.Pos6 == null && current.Pos4 == null) lst.Add((current with { Pos4 = currentA }, cost * (3 + steps)));
            if (current.Pos6 == null && current.Pos4 == null && current.Pos2 == null) lst.Add((current with { Pos2 = currentA }, cost * (5 + steps)));
            if (current.Pos6 == null && current.Pos4 == null && current.Pos1 == null && current.Pos2 == null) lst.Add((current with { Pos1 = currentA }, cost * (6 + steps)));

            if (current.Pos8 == null) lst.Add((current with { Pos8 = currentA }, cost * (1 + steps)));
            if (current.Pos8 == null && current.Pos10 == null) lst.Add((current with { Pos10 = currentA }, cost * (3 + steps)));
            if (current.Pos8 == null && current.Pos10 == null && current.Pos11 == null) lst.Add((current with { Pos11 = currentA }, cost * (4 + steps)));
            return lst;
        }

        private List<(RoomState, int)> FromRoom4(RoomState current, Amphi currentA, int steps)
        {
            var cost = GetCost(currentA);
            var lst = new List<(RoomState, int)>();
            if (current.Pos8 == null) lst.Add((current with { Pos8 = currentA }, cost * (1 + steps)));
            if (current.Pos8 == null && current.Pos6 == null) lst.Add((current with { Pos6 = currentA }, cost * (3 + steps)));
            if (current.Pos8 == null && current.Pos6 == null && current.Pos4 == null) lst.Add((current with { Pos4 = currentA }, cost * (5 + steps)));
            if (current.Pos8 == null && current.Pos6 == null && current.Pos4 == null && current.Pos2 == null) lst.Add((current with { Pos2 = currentA }, cost * (7 + steps)));
            if (current.Pos8 == null && current.Pos6 == null && current.Pos4 == null && current.Pos1 == null && current.Pos2 == null) lst.Add((current with { Pos1 = currentA }, cost * (8 + steps)));

            if (current.Pos10 == null) lst.Add((current with { Pos10 = currentA }, cost * (1 + steps)));
            if (current.Pos10 == null && current.Pos11 == null) lst.Add((current with { Pos11 = currentA }, cost * (2 + steps)));
            return lst;
        }

        private static int GetCost(Amphi a)
        {
            switch(a)
            {
                case Amphi.A:
                    return 1;
                case Amphi.B:
                    return 10;
                case Amphi.C:
                    return 100;
                case Amphi.D:
                    return 1000;
            }
            return 0;
        }

        private record RoomState
        {
            public Amphi? Pos1;
            public Amphi? Pos2;
            public Amphi? Pos4;
            public Amphi? Pos6;
            public Amphi? Pos8;
            public Amphi? Pos10;
            public Amphi? Pos11;

            public Amphi? Room11;
            public Amphi? Room12;
            public Amphi? Room13;
            public Amphi? Room14;

            public Amphi? Room21;
            public Amphi? Room22;
            public Amphi? Room23;
            public Amphi? Room24;

            public Amphi? Room31;
            public Amphi? Room32;
            public Amphi? Room33;
            public Amphi? Room34;

            public Amphi? Room41;
            public Amphi? Room42;
            public Amphi? Room43;
            public Amphi? Room44;
        }

        public enum Amphi
        {
            A,B,C,D
        }
    }
}
