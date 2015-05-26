using System;
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
        public static System.Drawing.SolidBrush myWhiteBrush = new SolidBrush(Color.White);
        public static System.Drawing.Graphics myGraphics = null;
        public static int width;
        public static int height;
        public Map myMap;
        public Player myPlayer;
        
        public mainGame()
        {
            InitializeComponent();
            myPlayer = new Player(5, 5, 0);
            myMap = new Map(10, 10, 50);
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
                    moveForward();
                    return true;
                case Keys.Left:
                    Turn(false);
                    return true;
                case Keys.Right:
                    Turn(true);
                    return true;
            }
            return false;
        }

        public void Start()
        {
            myGraphics = this.CreateGraphics();
            myMap.Draw(myPlayer);
        }
        public void Turn(bool isRight)
        {
            if (isRight == true)
            {
                if(myPlayer.getDir() == 5) {
                    myPlayer.updateDir(0);
                }
                else
                {
                    myPlayer.updateDir(myPlayer.getDir() + 1);
                }
            }
            else
            {
                if (myPlayer.getDir() == 0)
                {
                    myPlayer.updateDir(5);
                }
                else
                {
                    myPlayer.updateDir(myPlayer.getDir() - 1);
                }
            }
        }
        public void moveForward()
        {
            if (myPlayer.getDir() == 0)
            {
                myPlayer.setRow(myPlayer.getRow() - 1);
                myMap.Clear();
                myMap.Draw(myPlayer);
            }
            else if (myPlayer.getDir() == 1)
            {
                myPlayer.setCol(myPlayer.getCol() + 1);
                myMap.Clear();
                myMap.Draw(myPlayer);
            
            }
            else if (myPlayer.getDir() == 2)
            {
                myPlayer.setRow(myPlayer.getRow() + 1);
                myPlayer.setCol(myPlayer.getCol() + 1);
                myMap.Clear();
                myMap.Draw(myPlayer);
            
            }
            else if (myPlayer.getDir() == 3)
            {
                myPlayer.setRow(myPlayer.getRow() + 1);
                myMap.Clear();
                myMap.Draw(myPlayer);
            
            }
            else if (myPlayer.getDir() == 4)
            {
                myPlayer.setRow(myPlayer.getRow() + 1);
                myPlayer.setCol(myPlayer.getCol() - 1);
                myMap.Clear();
                myMap.Draw(myPlayer);
            
            }
            else if (myPlayer.getDir() == 5)
            {
                myPlayer.setCol(myPlayer.getCol() - 1);
                myMap.Clear();
                myMap.Draw(myPlayer);
            }
        }

        
    }
}
