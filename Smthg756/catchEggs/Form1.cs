using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rain
{
    public partial class Form1 : Form
    {
        int personX, ID = 0, period = 0, score = 0;
        int[] rainX = new int[100];
        int[] rainY = new int[100];

        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "30";
            ID = 0; period = 0; score = 0;
            for(int i = 0; i < 100; i++)
            {
                rainX[i] = 0;
                rainY[i] = 0;
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            personX = e.X;
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for(int i = 1; i <= ID; i++)
            {
                e.Graphics.FillEllipse(Pens.Blue.Brush, rainX[i], rainY[i], 40, 40);
            }
            e.Graphics.FillRectangle(Pens.Orange.Brush, personX - 40, 440, 80, 40);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            period++;
            if (period == 5)
            {
                if(ID%2==0) label1.Text = Convert.ToString(int.Parse(label1.Text) - 1);
                if (int.Parse(label1.Text) == 0)
                {
                    timer1.Stop();
                    MessageBox.Show("Your Score is: " + score);
                }

                ID++;
                rainX[ID] = random.Next(pictureBox1.Width - 40);
                rainY[ID] = 0;
                period = 0;
            }
            for(int i = 1; i <= ID; i++)
            {
                rainY[i]+=20;
                if(rainY[i]==440 && rainX[i] >= personX-40 && rainX[i]+40 <= personX+40)
                {
                    rainY[i] = 600;
                    score++;
                }
            }
            label2.Text = Convert.ToString(score);
            label2.Location = new Point(personX-10,455);
            Refresh();
        }
    }
}
