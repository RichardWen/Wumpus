using System;

namespace HuntTheWumpus
{
    public class Wumpus : Entity
    {
        public Wumpus(int row, int col, int dir)
            : base(row, col, dir)
        {

        }

        public void Update(Map map)
        {
            MoveForward(map);
            UpdateSight(map);
        }
    }
}
