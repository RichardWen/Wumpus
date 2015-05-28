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
        public static System.Drawing.Pen myBlue = new Pen(Color.Blue, 2);
        public static System.Drawing.Pen myRed = new Pen(Color.Crimson, 2);
        public static System.Drawing.Pen myWhite = new Pen(Color.White, 2);
        public static System.Drawing.Pen myPurple = new Pen(Color.Purple, 2);
        public static System.Drawing.Pen myBlack = new Pen(Color.Black, 2);
        public static System.Drawing.Pen myYellow = new Pen(Color.Yellow, 2);
        public static System.Drawing.SolidBrush myWhiteBrush = new SolidBrush(Color.White);
        public static System.Drawing.Graphics myGraphics = null;
        public static int width;
        public static int height;

        public Map myMap;
        public Player myPlayer;
        public Wumpus myWumpus;
        public Wumpi[] myWumpis;

        public int turn = 0;
        
        public mainGame()
        {
          
            Random rnd = new Random();
            myMap = new Map(10, 18, 50);
            myPlayer = new Player(5, 5, 0);
            myWumpus = new Wumpus(4, 4, 3);
            myWumpis = new Wumpi[5];
            for (int i = 0; i < 5; i++ )
            {
                myWumpis[i] = new Wumpi(rnd.Next(10), rnd.Next(10), rnd.Next(6));
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
                case Keys.Space:
                    Start();
                    return true;
                case Keys.Up:
                    myPlayer.MoveForward(myMap);
                    Update();
                    return true;
                case Keys.Left:
                    Turn(false);
                    Update();
                    return true;
                case Keys.Right:
                    Turn(true);
                    Update();
                    return true;
            }
            return false;
        }

        public void Start()
        {
            myGraphics = this.CreateGraphics();
            myMap.Draw(myPlayer, myWumpus, myWumpis);
        }

        public void Update()
        {
            myPlayer.UpdateSight(myMap);
            foreach(int[] seenTile in myPlayer.sight)
            {
                Console.WriteLine(seenTile[0] + "," + seenTile[1]);
            }
            myWumpus.Update(myMap);
            foreach (Wumpi wumpiInstance in myWumpis)
            {
                wumpiInstance.Update(myMap);
            }
            myMap.Clear();
            myMap.Draw(myPlayer, myWumpus, myWumpis);
        }

        public void Turn(bool isRight)
        {
            if (isRight == true)
            {
                myPlayer.changeDir(1);
            }
            else
            {
                myPlayer.changeDir(-1);
            }
        }       
    }
}
