using System;
using System.Drawing;
using System.Windows.Forms;

namespace HuntTheWumpus
{
    public class Map
    {
        public int turn = 123;
        Random rand = new Random();
        public int BufferX = 30;
        public int BufferY = 40;
        public int hexWidth;
        public int[,] map;
        public bool[,,] doors;
        private int mapHeight, mapWidth;
        private Bitmap Overlay = Properties.Resources.Movingborder3;
        private Bitmap Overlay2 = Properties.Resources.Border5;
        private Bitmap GameOver = Properties.Resources.GameOver;
        private Bitmap FloorRed = Properties.Resources.FloorRed;
        private Bitmap FloorBlue = Properties.Resources.Floor;
        private Bitmap FloorYellow = Properties.Resources.FloorYellow;

        public Map(int height, int width, int hexagonWidth)
        {
            mapHeight = height;
            mapWidth = width;
            hexWidth = hexagonWidth;
            doors = new bool[height, width, 6];
            addDoors();
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
        public bool[,,] GetDoors()
        {
            return doors;
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

            if (newCol >= mapWidth)
            {
                newCol = 0;
            }
            else if (newCol < 0)
            {
                newCol = mapWidth - 1;
            }
            if (newRow >= mapHeight)
            {
                newRow = 0;
            }
            else if (newRow < 0)
            {
                newRow = mapHeight - 1;
            }
        }

        public bool StartDraw(Player playerInstance, Wumpus wumpusInstance, Wumpi[] wumpisInstance, int score)
        {
            int offsetX = 50 * (playerInstance.getCol() * 2 + 1);
            int offsetY = 50 * (playerInstance.getRow() * 2 + 1 + playerInstance.getCol() % 2);
            bool inGame = true;
            {
                for (int i = 0; i < mapHeight; i++)
                {
                    for (int j = 0; j < mapWidth; j++)
                    {
                        Point center = new Point(hexWidth * (j * 2 + 1), hexWidth * (i * 2 + 1 + j % 2));
                        Point[] edges = new Point[6];
                        mainGame.myGraphics.DrawImage(FloorBlue, center.X - 50, center.Y - 50);
                        if (playerInstance.getRow() == i && playerInstance.getCol() == j)
                        {
                            foreach (Wumpi wumpi in wumpisInstance)
                            {
                                
                                foreach (int[] seenTile in wumpi.sight)
                                {
                                    if (seenTile[0] == i && seenTile[1] == j)
                                    {
                                        mainGame.myGraphics.DrawImage(FloorRed, center.X - 50, center.Y - 50);
                                    }
                                    else
                                    {
                                        mainGame.myGraphics.DrawImage(FloorYellow, center.X - 50, center.Y - 50);

                                    }
                                }
                                if (wumpi.getRow() == i && wumpi.getCol() == j && !wumpi.isKill())
                                {
                                    mainGame.myGraphics.DrawImage(GameOver, 0, 0);
                                    inGame = false;
                                    Score scores = new Score();
                                    string scoreStr = "";
                                    if(score < 10)
                                    {
                                        scoreStr = "00" + score;
                                    }
                                    else if (score < 100)
                                    {
                                        scoreStr = "0" + score;
                                    }
                                    else
                                    {
                                        scoreStr = "" + score;
                                    }
                                    scores.addScore("ABC" + scoreStr);
                                    return false;
                                }
                            }
                        }

                        else if (wumpusInstance.getRow() == i && wumpusInstance.getCol() == j)
                        {
                            mainGame.myGraphics.DrawImage(FloorBlue, center.X - 50, center.Y - 50);
                        }

                    }

                    playerInstance.Draw();

                }

                foreach (int[] seenTile in playerInstance.sight)
                {
                    bool wumpiTile = false;
                    Point seenCenter = new Point(hexWidth * (seenTile[1] * 2 + 1), hexWidth * (seenTile[0] * 2 + 1 + seenTile[1] % 2));
                    foreach(Wumpi wumpi in wumpisInstance)
                    {
                        if (seenTile[0].Equals(wumpi.getRow()) && seenTile[1].Equals(wumpi.getCol()))
                        {
                            wumpiTile = true;
                            mainGame.myGraphics.DrawImage(FloorYellow, seenCenter.X - 50, seenCenter.Y - 50);
                            wumpi.Draw();
                        }
                    }
                    if (!wumpiTile)
                    {
                        mainGame.myGraphics.DrawImage(FloorYellow, seenCenter.X - 50, seenCenter.Y - 50);
                    }
            
                }
             //   mainGame.myGraphics.DrawImage(Overlay2, 0, 0);

            }
            return true;

       
        }
        public void Clear()
        {
            //mainGame.myGraphics.FillRectangle(mainGame.myTealBrush, 0, 0, mainGame.width, mainGame.height);
        }

        public void drawBackground()
        {
            mainGame.myGraphics.FillRectangle(mainGame.myTealBrush, 0, 0, mainGame.width, mainGame.height);
        }

        private void addDoors()
        {
            for (int i = 0; i < doors.GetLength(0); i++)
            {
                for (int j = 0; j < doors.GetLength(1); j++)
                {
                    for(int k = 0; k < doors.GetLength(2); k++)
                    {
                        int rowToSet = i;
                        int colToSet = j;

                        if (rand.Next(0, 2) == 1)
                        {
                            doors[i, j, k] = true;
                            FindForward(i, j, k, out rowToSet, out colToSet);
                            doors[rowToSet, colToSet, (k + 3) % 6] = true;
                        }
                        else
                        {
                            doors[i, j, k] = false;
                            FindForward(i, j, k, out rowToSet, out colToSet);
                            doors[rowToSet, colToSet, (k + 3) % 6] = false;
                        }
                    }
                }
            }
        }

        public int doorNum(int roomNum, int dir)
        {
            //0 = up, 0-5 clockwise
            switch (dir)
            {
                case 5:
                    return roomNum * 3;
                case 0:
                    return roomNum * 3 + 1;
                case 1:
                    return roomNum * 3 + 2;
                case 4:
                    if (roomNum % 2 == 0)
                    {
                        if (roomNum % mapWidth == 0)
                        {
                            return (roomNum + mapWidth) * 3 - 1;
                        }
                        return (roomNum * 3) - 1;
                    }
                    return (roomNum + mapWidth * 3) - 1;
                case 3:
                    return (roomNum + mapWidth) * 3 + 1; // originally multiplied by mw
                case 2:
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