using System;
using System.Drawing;
using System.Windows.Forms;


namespace HuntTheWumpus
{
    public class Map
    {
        public int BufferX = 30;
        public int BufferY = 40;
        public int hexWidth;
        public int[,] map;
        private int mapHeight, mapWidth;
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
        public void Draw(Player playerInstance)
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Point center = new Point( hexWidth * (j * 2 + 1) , hexWidth * (i * 2 + 1 + j % 2));
                    Point[] edges = new Point[6];

                    int half_width = hexWidth ;
                    int tmp = (int)(half_width * 1.732 / 2);
                    edges[0] = new Point((center.X + half_width), center.Y);
                    edges[1] = new Point((center.X + half_width/2), center.Y + tmp);
                    edges[2] = new Point((center.X - half_width/2), center.Y + tmp);
                    edges[3] = new Point((center.X - half_width), center.Y);
                    edges[4] = new Point((center.X - half_width/2), center.Y - tmp);
                    edges[5] = new Point((center.X + half_width/2), center.Y - tmp);
                    if (playerInstance.getRow() == i && playerInstance.getCol() == j)
                    
                    {   
                        mainGame.myGraphics.DrawPolygon(mainGame.myBlue, edges);
                        mainGame.myGraphics.DrawEllipse(mainGame.myPurple, center.X - half_width / 2, center.Y - tmp / 2, half_width, tmp);
                    }
                    else
                    {
                        mainGame.myGraphics.DrawPolygon(mainGame.myRed, edges);
                    }
                }

            }
          
        }
        public void Clear()
        {
            mainGame.myGraphics.FillRectangle(mainGame.myWhiteBrush, 0, 0, mainGame.width, mainGame.height);
        }
 
    }
}