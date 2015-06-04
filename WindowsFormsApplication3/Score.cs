using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus
{
    class Score
    {
        public Score()
        {

        }
        public void addScore(string score)
        {
            if (!System.IO.File.Exists(@"C:\Users\Public\Test\scores.txt"))
            {
                System.IO.File.Create(@"C:\Users\Public\Test\scores.txt");
            }

            string[] read = System.IO.File.ReadAllLines(@"C:\Users\Public\Test\scores.txt");
            string[] sorted = new string[read.Length + 1];
            for (int i = 0; i < read.Length; i++)
            {
                sorted[i] = read[i];
            }
            for (int i = sorted.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    sorted[i] = score;
                    i = -1;
                }
                else if (Convert.ToInt16(score.Substring(3)) > Convert.ToInt16(sorted[i - 1].Substring(3)))
                {
                    sorted[i] = sorted[i - 1];
                }
                else
                {
                    sorted[i] = score;
                    i = -1;
                }
            }

            System.IO.File.WriteAllLines(@"C:\Users\Public\Test\scores.txt", sorted);
        }
        public string[] getScores()
        {
            if (!System.IO.File.Exists(@"C:\Users\Public\Test\scores.txt"))
            {
                System.IO.File.Create(@"C:\Users\Public\Test\scores.txt");
            }

            string[] read = System.IO.File.ReadAllLines(@"C:\Users\Public\Test\scores.txt");

            return read;

        }

        public void print()
        {
            if (!System.IO.File.Exists(@"C:\Users\Public\Test\scores.txt"))
            {
                System.IO.File.Create(@"C:\Users\Public\Test\scores.txt");
            }

            string[] read = System.IO.File.ReadAllLines(@"C:\Users\Public\Test\scores.txt");

            for (int i = 0; i < read.Length; i++)
            {
                Console.Out.WriteLine(read[i]);
            }
        }
    }
}
