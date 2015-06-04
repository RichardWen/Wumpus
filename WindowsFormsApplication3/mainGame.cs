using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HuntTheWumpus
{
    public partial class mainGame : Form
    {
        private Bitmap Overlay2 = Properties.Resources.Border5;
        public static System.Drawing.Pen myBlue = new Pen(Color.Blue, 2);
        public static System.Drawing.Pen myRed = new Pen(Color.Crimson, 2);
        public static System.Drawing.Pen myWhite = new Pen(Color.White, 2);
        public static System.Drawing.Pen myPurple = new Pen(Color.Purple, 2);
        public static System.Drawing.Pen myBlack = new Pen(Color.Black, 2);
        public static System.Drawing.Pen myYellow = new Pen(Color.Yellow, 2);
        public static System.Drawing.SolidBrush myWhiteBrush = new SolidBrush(Color.White);
        public static System.Drawing.SolidBrush myTealBrush = new SolidBrush(Color.FromArgb(10, 18, 20));
        public static System.Drawing.Graphics myGraphics = null;
        public static int width;
        public static int height;

        public Map myMap;
        public Player myPlayer;
        public Wumpus myWumpus;
        public Wumpi[] myWumpis;


        public int score = 0;
        
        public mainGame()
        {
            
            Random rnd = new Random();
            myMap = new Map(10, 18, 50);
            myPlayer = new Player(5, 5, 0);
            myWumpus = new Wumpus(4, 4, 3);
            myWumpis = new Wumpi[15];
            for (int i = 0; i < 15; i++ )
            {
                myWumpis[i] = new Wumpi(rnd.Next(9), rnd.Next(17), rnd.Next(6));
            }
            this.Width = myMap.BufferX * 3 + myMap.getWidth() * myMap.getHex() * 2;
            width = myMap.BufferX * 3 + myMap.getWidth() * myMap.getHex() * 2;
            this.Height = myMap.BufferY * 3 + myMap.getHeight() * myMap.getHex() * 2;
            height = myMap.BufferY * 3 + myMap.getHeight() * myMap.getHex() * 2;
            this.Refresh();
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Q:
                    MenuStart();
                    return true;
                case Keys.Space:
                    Start();
                    return true;
                case Keys.Up:
                    myPlayer.MoveForward(myMap);
                    Update(true);
                    score++;
                    return true;
                case Keys.Left:
                    myPlayer.changeDir(-1);
                    Update(false);
                    return true;
                case Keys.Right:
                    myPlayer.changeDir(1);
                    Update(false);
                    return true;
            }
            return false;
        }

        public void Start()
        {
            myGraphics = this.CreateGraphics();
            myMap.drawBackground();
            myGraphics = this.CreateGraphics();
       
            myMap.StartDraw(myPlayer, myWumpus, myWumpis, score);
        }

        public void MenuStart()
        {
            myGraphics = this.CreateGraphics();
            Bitmap CoverImage = Properties.Resources.Cover1;
            myGraphics.DrawImage(CoverImage, 0, 0);
        }

        public void Update(bool wumpi)
        {
            if(myPlayer.UpdateSight(myMap, myWumpis, score))
            {
                score += 100;
            }
            foreach(int[] seenTile in myPlayer.sight)
            {
                Console.WriteLine(seenTile[0] + "," + seenTile[1]);
            }
            
            if (wumpi)
            {
                myWumpus.Update(myMap);
                foreach (Wumpi wumpiInstance in myWumpis)
                {
                    wumpiInstance.Update(myMap);
                }
            }
            myMap.Clear();
            myMap.StartDraw(myPlayer, myWumpus, myWumpis, score);
        }      
        public void incScore(int amount)
        {
            score += amount;
        }
    }
}
