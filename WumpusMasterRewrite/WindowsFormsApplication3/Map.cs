using System;
using System.Drawing;
using System.Windows.Forms;

namespace HuntTheWumpus
{
    public class Map
    {
        Random rand = new Random();
        public int BufferX = 30;
        public int BufferY = 40;
        public int hexWidth;
        public int[,] map;
        bool[] doors = new bool[300];
        private int mapHeight, mapWidth;
        private Bitmap FloorRed = Properties.Resources.FloorRed;
        private Bitmap FloorBlue = Properties.Resources.Floor;
        private Bitmap FloorYellow = Properties.Resources.FloorYellow;
        public Map(int height, int width, int hexagonWidth)
        {
            mapHeight = height;
            mapWidth = width;
            hexWidth = hexagonWidth;
            map = new int[height, width];
            int count = 1;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = count;
                    count++;
                }
            }
        }
        public int getHeight()
        {
            return mapHeight;
        }
        public int getWidth()
        {
            return mapWidth;
        }
        public int getHex()
        {
            return hexWidth;
        }

        public void FindForward(int rowNum, int colNum, int dir, out int newRow, out int newCol)
        {
            if (dir == 0)
            {
                newRow = rowNum - 1;
                newCol = colNum;
            }
            else if (dir == 1)
            {
                if (colNum % 2 == 1)
                {
                    newRow = rowNum;
                    newCol = colNum + 1;
                }
                else
                {
                    newRow = rowNum - 1;
                    newCol = colNum + 1;
                }
            }
            else if (dir == 2)
            {
                if (colNum % 2 == 1)
                {
                    newRow = rowNum + 1;
                    newCol = colNum + 1;
                }
                else
                {
                    newRow = rowNum;
                    newCol = colNum + 1;
                }
            }
            else if (dir == 3)
            {

                newRow = rowNum + 1;
                newCol = colNum;

            }
            else if (dir == 4)
            {
                if (colNum % 2 == 1)
                {
                    newRow = rowNum + 1;
                    newCol = colNum - 1;
                }
                else
                {
                    newRow = rowNum;
                    newCol = colNum - 1;
                }

            }
            else if (dir == 5)
            {
                if (colNum % 2 == 1)
                {
                    newRow = rowNum;
                    newCol = colNum - 1;
                }
                else
                {
                    newRow = rowNum - 1;
                    newCol = colNum - 1;
                }
            }
            else
            {
                newRow = rowNum;
                newCol = colNum;
            }

            if (newCol > 9)
            {
                newCol = 0;
            }
            else if (newCol < 0)
            {
                newCol = 9;
            }
            if (newRow > 7)
            {
                newRow = 0;
            }
            else if (newRow < 0)
            {
                newRow = 7;
            }
        }

        public void Draw(Player playerInstance, Wumpus wumpusInstance, Wumpi[] wumpisInstance)
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Point center = new Point( hexWidth * (j * 2 + 1) , hexWidth * (i * 2 + 1 + j % 2));
                    Point[] edges = new Point[6];
                    if (playerInstance.getRow() == i && playerInstance.getCol() == j)
                    {
                       mainGame.myGraphics.DrawImage(FloorRed, center.X - 50, center.Y - 50);
                    }
                    else if(wumpusInstance.getRow() == i && wumpusInstance.getCol() == j)
                    {
                        mainGame.myGraphics.DrawImage(FloorRed, center.X - 50, center.Y - 50);
                    }
                    else
                    {
                        foreach (Wumpi wumpiInstance in wumpisInstance)
                        {
                            if(wumpiInstance.getRow() == i && wumpiInstance.getCol() == j)
                            {
                                mainGame.myGraphics.DrawImage(FloorYellow, center.X - 50, center.Y - 50);
                                wumpiInstance.Draw();
                            }
                            else
                            {
                                mainGame.myGraphics.DrawImage(FloorBlue, center.X - 50, center.Y - 50);
                            }
                        }
                    }
                }

            }
            
            foreach(int[] seenTile in playerInstance.sight)
            {
                Point seenCenter = new Point(hexWidth * (seenTile[1] * 2 + 1), hexWidth * (seenTile[0] * 2 + 1 + seenTile[1] % 2));
                mainGame.myGraphics.DrawImage(FloorYellow, seenCenter.X - 50, seenCenter.Y - 50);
            }
          
        }
        public void Clear()
        {
            mainGame.myGraphics.FillRectangle(mainGame.myWhiteBrush, 0, 0, mainGame.width, mainGame.height);
        }
        private void addDoors()
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (rand.Next(0, 2) == 1)
                {
                    doors[i] = true;
                }
            }

        }

        public int doorNum(int roomNum, int dir)
        {
            //5 = up, 0-5 clockwise
            switch (dir)
            {
                case 4:
                    return roomNum * 3;
                case 5:
                    return roomNum * 3 + 1;
                case 0:
                    return roomNum * 3 + 2;
                case 3:
                    if (roomNum % 2 == 0)
                    {
                        if (roomNum % mapWidth == 0)
                        {
                            return (roomNum + mapWidth) * 3 - 1;
                        }
                        return (roomNum * 3) - 1;
                    }
                    return (roomNum + mapWidth * 3) - 1;
                case 2:
                    return (roomNum + mapWidth) * mapWidth * 3 + 1;
                case 1:
                    if (roomNum % 2 == 1)
                    {
                        if ((roomNum + 1) % mapWidth == 0)
                        {
                            return (roomNum + 1) * 3;
                        }
                        return (roomNum + mapWidth) * 3 - 1;
                    }
                    return (roomNum + 1) * 3;
                default:
                    break;
            }
            return 0;
        }

    }
}