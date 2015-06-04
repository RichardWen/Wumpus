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
        private int knifeParts = 0;
       
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
        public void Claer()
        {
            Point center = new Point(50 * (colNum * 2 + 1), 50 * (rowNum * 2 + 1 + colNum % 2)); 
        }
        public void addKnife()
        {
            knifeParts++;
        }
        public bool hasKnife()
        {
            if (knifeParts > 2)
            {
                return true;
            }
            return false;
        }
        public bool UpdateSight(Map map, Wumpi[] wumpis, int score)
        {
            Console.Out.WriteLine("PLAYER VERSION");
            sight.Clear();
            int currentRow = rowNum;
            int currentCol = colNum;
            int leftDir = dir - 1;
            if (leftDir < 0) { leftDir = 5; }
            int rightDir = dir + 1;
            if (rightDir > 5) { rightDir = 0; }

            // Check to the Left of the Entity
            for (int i = 0; i < 4; i++)
            {
                if (!map.GetDoors()[currentRow, currentCol, leftDir])
                {
                    break;
                }
                map.FindForward(currentRow, currentCol, leftDir, out currentRow, out currentCol);
                sight.Add(new int[] { currentRow, currentCol });
            }

            // Check directly in Front of the Entity
            currentRow = rowNum;
            currentCol = colNum;

            for (int i = 0; i < 4; i++)
            {
                if (!map.GetDoors()[currentRow, currentCol, dir])
                {
                    break;
                }
                map.FindForward(currentRow, currentCol, dir, out currentRow, out currentCol);
                sight.Add(new int[] { currentRow, currentCol });
            }

            // Check to the Right of the Entity

            currentRow = rowNum;
            currentCol = colNum;
            for (int i = 0; i < 4; i++)
            {
                if (!map.GetDoors()[currentRow, currentCol, rightDir])
                {
                    break;
                }
                map.FindForward(currentRow, currentCol, rightDir, out currentRow, out currentCol);
                sight.Add(new int[] { currentRow, currentCol });
            }
            foreach (Wumpi wumpi in wumpis)
            {
                if (wumpi.getRow().Equals(this.getRow()) && wumpi.getCol().Equals(this.getCol()))
                {
                    Console.Out.WriteLine(Math.Abs(wumpi.getDir() - this.getDir()));
                    if(Math.Abs(wumpi.getDir() - this.getDir()) < 2)
                    {
                        wumpi.kill();
                        score += 100;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
