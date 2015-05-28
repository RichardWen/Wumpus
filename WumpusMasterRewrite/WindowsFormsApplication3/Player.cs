using System;
using System.Drawing;
using System.Windows.Forms;

namespace HuntTheWumpus
{
    public class Player : Entity
    {
        private Bitmap PlayerBack = Properties.Resources.MainBBy1;
        private Bitmap PlayerFront = Properties.Resources.MainBby2;
        private Bitmap PlayerNW = Properties.Resources.MainBBy6;
        private Bitmap PlayerNE = Properties.Resources.MainBby5;
        private Bitmap PlayerSW = Properties.Resources.MainBby3;
        private Bitmap PlayerSE = Properties.Resources.MainBby4;
       
        public Player(int row, int col, int dir)
            : base(row, col, dir)
        {

        }
        public void Draw()
        {
            Point center = new Point(50 * (colNum * 2 + 1), 50 * (rowNum * 2 + 1 + colNum % 2));
            if (dir == 0)
            {
                mainGame.myGraphics.DrawImage(PlayerBack, center.X - 50, center.Y - 50);
            }
            else if (dir == 1)
            {
                mainGame.myGraphics.DrawImage(PlayerNE, center.X - 50, center.Y - 50);
            }
            else if (dir == 2)
            {
                mainGame.myGraphics.DrawImage(PlayerSE, center.X - 50, center.Y - 50);
            }
            else if (dir == 3)
            {
                mainGame.myGraphics.DrawImage(PlayerFront, center.X - 50, center.Y - 50);
            }
            else if (dir == 4)
            {
                mainGame.myGraphics.DrawImage(PlayerSW, center.X - 50, center.Y - 50);
            }
            else if (dir == 5)
            {
                mainGame.myGraphics.DrawImage(PlayerNW, center.X - 50, center.Y - 50);

            }
        }
    }
}
