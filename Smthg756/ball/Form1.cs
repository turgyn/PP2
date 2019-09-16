using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace angry_birds
{
    public partial class Form1 : Form
    {
        int x1=2000, x2=2000, dx, y1, y2, dy,score=0;
        Pen pen = new Pen(Color.Red, 4);
        Pen skyBluePen = new Pen(Brushes.DeepSkyBlue);
        

        
        bool dra = false;


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format("X = {0},Y = {0}", e.X, e.Y);
            x2 = e.Location.X;
            y2 = e.Location.Y;
            Refresh();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            skyBluePen.DashStyle = DashStyle.Dash;
            skyBluePen.EndCap = LineCap.ArrowAnchor;
            skyBluePen.StartCap = LineCap.AnchorMask;


            skyBluePen.Width = 6.0F;

            e.Graphics.FillEllipse(pen.Brush, x1 - 25, y1 - 25, 50, 50);
            e.Graphics.DrawLine(new Pen(Color.Green,10), 0, 408, 1000, 408);
            e.Graphics.DrawLine(new Pen(Color.Black, 7), 800, 0, 800, 80);

            e.Graphics.DrawLine(new Pen(Color.Black, 7), 800, 150, 800, 420);
            if(dra) e.Graphics.DrawLine(skyBluePen, x1, y1, x2, y2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (x1 > 760 && (y1 < 80 || y1 > 150)) dx = -1 * dx;
            label1.Text = "Score: " + Convert.ToString(score);
            x1 = x1 + dx/10;    
            if (y1 > 368) dy = -1 * dy;
            dy = dy + 5;
            y1 = y1 + dy / 10;
            Refresh();
        }
        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dra = true;
            x1 = e.Location.X;
            y1 = e.Location.Y;
            x2 = e.Location.X;
            y2 = e.Location.Y;
            Refresh();
            timer1.Enabled = false;

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dra = false;
            x2 = e.Location.X;
            y2 = e.Location.Y;
            dx = x2 - x1;
            dy = y2 - y1;

            timer1.Start();
        }
    }
}
