using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus
{
    public class Entity
    {
        public int rowNum;
        public int colNum;
        public int dir;
        public List<int[]> sight;

        public Entity(int row, int col, int startDir)
        {
            rowNum = row;
            colNum = col;
            dir = startDir;
            sight = new List<int[]>();
        }
        public void changePos(int changeInRow, int changeInCol)
        {
            rowNum += changeInRow;
            colNum += changeInCol;
        }
        public void changeDir(int changeInDir)
        {
            dir += changeInDir;
            if (dir >= 6)
            {
                dir = 0;
            }
            else if (dir <= -1)
            {
                dir = 5;
            }
        }

        public void MoveForward(Map map)
        {
            map.FindForward(rowNum, colNum, dir, out rowNum, out colNum);
        }
        public List<int[]> ReturnSight ()
        {
            return sight;
        }
        public void UpdateSight(Map map)
        {
            sight.Clear();
            int currentRow = rowNum;
            int currentCol = colNum;
            int leftDir = dir - 1;
            if(leftDir < 0) {leftDir = 5;}
            int rightDir = dir + 1;
            if(rightDir > 5) {rightDir = 0;}

            // Check to the Left of the Entity
            for(int i = 0; i < 4; i++)
            {
                map.FindForward(currentRow, currentCol, leftDir, out currentRow, out currentCol);
                sight.Add(new int[]{currentRow, currentCol});
            }

            // Check directly in Front of the Entity
            currentRow = rowNum;
            currentCol = colNum;

            for (int i = 0; i < 4; i++)
            {
                map.FindForward(currentRow, currentCol, dir, out currentRow, out currentCol);
                sight.Add(new int[] { currentRow, currentCol });
            }

            // Check to the Right of the Entity

            currentRow = rowNum;
            currentCol = colNum;
            for (int i = 0; i < 4; i++)
            {
                map.FindForward(currentRow, currentCol, rightDir, out currentRow, out currentCol);
                sight.Add(new int[] { currentRow, currentCol });
            }
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
        public void setDir(int newDir)
        {
            dir = newDir;
        }
        public int getDir()
        {
            return dir;
        }
    }
}
