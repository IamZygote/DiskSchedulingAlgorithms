using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_Project
{
    public partial class Form2 : Form
    {
        Bitmap off;
        int NumberOfDisk;
        List<int> shoratValue = new List<int>();
        List<int> shoratX = new List<int>();
        List<int> seq = new List<int>();
        public Form2(int nDisk, int start, int end, int curr_disk,List<int> s, int[] disk)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form2_Load;
            this.Paint += Form2_Paint;
            NumberOfDisk = nDisk + 3;
            for(int i=0;i<disk.Length;i++)
            {
                shoratValue.Add(disk[i]);
            }
            for (int i = 0; i < s.Count; i++)
            {
                seq.Add(s[i]);
            }
            shoratValue.Add(start);
            shoratValue.Add(end);
            shoratValue.Add(curr_disk);
            shoratValue.Sort();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Dubblebuffer(e.Graphics);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            for (int i = 40; i < 80 * NumberOfDisk; i += 80)
            {
                shoratX.Add(i);
            }
        }

        void Dubblebuffer(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            Drawscene(g2);
            g.DrawImage(off, 0, 0);
        }
        void Drawscene(Graphics g)
        {
            Pen p = new Pen(Brushes.Black, 2);
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            StringFormat drawFormat = new StringFormat();
            int s = 0;
            for (int i = 40; i < 80 * NumberOfDisk; i += 80,s++) 
            {
                g.DrawLine(p, i, 80, i, 70);

                g.DrawString(Convert.ToString(shoratValue[s]), drawFont, drawBrush, i-8, 40, drawFormat);


            }
            g.DrawLine(p, 40, 80,(80 * NumberOfDisk)-40, 80);
            int yalue = 120;
            int drawF = 0;
            int temp=0;
            for(int i=0;i<seq.Count;i++,yalue+=40)
            {
                for(int j=0;j<shoratValue.Count;j++)
                {
                    if(seq[i]==shoratValue[j])
                    {
                        drawF++;
                        g.FillEllipse(Brushes.Red, shoratX[j] - 7, yalue, 15, 15);
                        if(drawF==2)
                        {
                            drawF = 1;
                            g.DrawLine(p, shoratX[temp] - 7, yalue-40, shoratX[j] - 7, yalue);
                        }
                        temp = j;
                    }
                }
            }
        }
    }
}
