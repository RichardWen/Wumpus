﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace HuntTheWumpus
{
    public class Wumpi : Entity
    {
        private Bitmap WumpiBack= Properties.Resources.WumpiBack;
        private Bitmap WumpiFront = Properties.Resources.Wumpi;
        private Bitmap WumpiNW = Properties.Resources.WumpiNorthLeft;
        private Bitmap WumpiNE = Properties.Resources.WumpiNorthRight;
        private Bitmap WumpiSW = Properties.Resources.WumpiSouthLeft;
        private Bitmap WumpiSE = Properties.Resources.WumpiSouthRight;
       
        public Wumpi(int row, int col, int dir)
            : base(row, col, dir)
        {

        }
        public void Update(Map map)
        {
            MoveForward(map);
            UpdateSight(map);
        }
        public void Draw()
        {
            Point center = new Point(50 * (colNum * 2 + 1), 50 * (rowNum * 2 + 1 + colNum % 2));
            if (dir == 0)
            {
                mainGame.myGraphics.DrawImage(WumpiBack, center.X - 50, center.Y - 50);      
            }
            else if (dir == 1)
            {
                mainGame.myGraphics.DrawImage(WumpiNE, center.X - 50, center.Y - 50);
            }
            else if (dir == 2)
            {
                mainGame.myGraphics.DrawImage(WumpiSE, center.X - 50, center.Y - 50);
            }
            else if (dir == 3)
            {
                mainGame.myGraphics.DrawImage(WumpiFront, center.X - 50, center.Y - 50);            
            }
            else if (dir == 4)
            {
               mainGame.myGraphics.DrawImage(WumpiSW, center.X - 50, center.Y - 50); 
            }
            else if (dir == 5)
            {
                mainGame.myGraphics.DrawImage(WumpiNW, center.X - 50, center.Y - 50);
            
            }
        }
    }
}
