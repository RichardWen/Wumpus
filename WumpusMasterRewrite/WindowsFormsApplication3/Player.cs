using System;

namespace HuntTheWumpus
{
    public class Player
    {
        public int rowNum;
        public int colNum;
        public int playerDir;
        public Player(int row, int col, int dir)
        {
            rowNum = row;
            colNum = col;
            playerDir = dir;
        }
        public void setRow(int newRow)
        {
            rowNum = newRow;
        }
        public int getRow()
        {
            return rowNum;
        }
        public void setCol(int newCol)
        {
             colNum = newCol;
        }
        public int getCol()
        {
            return colNum;
        }
        public void updateDir(int newDir)
        {
            playerDir = newDir;
        }
        public int getDir()
        {
            return playerDir;
        }
    }
}
