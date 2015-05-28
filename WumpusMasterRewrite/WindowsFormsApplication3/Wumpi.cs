using System;

namespace HuntTheWumpus
{
    public class Wumpi : Entity
    {
        public Wumpi(int row, int col, int dir)
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
