using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank
{
    public partial class Form1 : Form
    {
        enum dir
        {
            Up,
            Right,
            Down,
            Left
        }
        SoundPlayer sp = new SoundPlayer(@"C:\Users\turgu\Downloads\shot.wav");
        SoundPlayer sp2 = new SoundPlayer(@"C:\Users\turgu\Downloads\hit.wav");

        dir dir1, dir2;
        Size size = new Size(100, 100),
            small = new Size(10, 10);
        int score1 = 100, score2 = 100;

        List<int> bullet_player1_x = new List<int>(),
            bullet_player1_y = new List<int>(), 
            bullet_player2_x = new List<int>(),
            bullet_player2_y = new List<int>();

        List<dir> dirbullet1 = new List<dir>();
        List<dir> dirbullet2 = new List<dir>();

        int bullet1 = -1 , bullet2 = -1;

        Point position_player1 = new Point(0,0);
        Point position_player2 = new Point(1265,580);

        Rectangle rectangle1, rectangle2;

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        Bitmap bm_p1, bm_p2;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {   for(int i = 0; i < bullet_player1_x.Count; i++)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.Red), bullet_player1_x[i], bullet_player1_y[i], 10, 10);
            }
            for (int i = 0; i < bullet_player2_x.Count; i++)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.Blue), bullet_player2_x[i], bullet_player2_y[i], 10, 10);
            }
            e.Graphics.DrawImage(bm_p1, new Rectangle(position_player1, size));
            e.Graphics.DrawImage(bm_p2, new Rectangle(position_player2, size));
        }

        public Form1()
        {
            InitializeComponent();
            bm_p1 = Resource1.player1_up;
            bm_p2 = Resource1.player2_up;
            rectangle1 = new Rectangle(position_player1, size);
            rectangle2 = new Rectangle(position_player2, size);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < bullet_player1_x.Count; i++)
            {
                switch (dirbullet1[i])
                {
                    case dir.Up:
                        bullet_player1_y[i] -=10;
                        break;
                    case dir.Right:
                        bullet_player1_x[i]+=10;
                        break;
                    case dir.Down:
                        bullet_player1_y[i]+=10;
                        break;
                    case dir.Left:
                        bullet_player1_x[i]-=10;
                        break;
                }
                if(bullet_player1_x[i]<-10 || bullet_player1_x[i] > pictureBox1.Width+10 || bullet_player1_y[i] < -10 || bullet_player1_y[i] > pictureBox1.Height+10)
                {//вылет пуль за окно
                    bullet_player1_x.Remove(bullet_player1_x[i]);
                    bullet_player1_y.Remove(bullet_player1_y[i]);
                    dirbullet1.RemoveAt(i);
                    
                }
                else if (bullet_player1_x[i] > position_player2.X && bullet_player1_x[i] < position_player2.X + 100 && bullet_player1_y[i] > position_player2.Y && bullet_player1_y[i] < position_player2.Y + 100)
                {
                    bullet_player1_x.Remove(bullet_player1_x[i]);
                    bullet_player1_y.Remove(bullet_player1_y[i]);
                    dirbullet1.RemoveAt(i);
                    score1-=10;
                    sp2.Play();
                }

            }
            for (int i = 0; i < bullet_player2_x.Count; i++)
            {
                switch (dirbullet2[i])
                {
                    case dir.Up:
                        bullet_player2_y[i] -= 10;
                        break;
                    case dir.Right:
                        bullet_player2_x[i] += 10;
                        break;
                    case dir.Down:
                        bullet_player2_y[i] += 10;
                        break;
                    case dir.Left:
                        bullet_player2_x[i] -= 10;
                        break;
                }
                if (bullet_player2_x[i] <= 0 || bullet_player2_x[i] > pictureBox1.Width + 10 || bullet_player2_y[i] < 0 || bullet_player2_y[i] > pictureBox1.Height + 10)
                {
                    bullet_player2_x.Remove(bullet_player2_x[i]);
                    bullet_player2_y.Remove(bullet_player2_y[i]);
                    dirbullet2.RemoveAt(i);
                }
                else if (bullet_player2_x[i] > position_player1.X && bullet_player2_x[i] < position_player1.X + 100 && bullet_player2_y[i] > position_player1.Y && bullet_player2_y[i] < position_player1.Y + 100)
                {
                    bullet_player2_x.Remove(bullet_player2_x[i]);
                    bullet_player2_y.Remove(bullet_player2_y[i]);
                    dirbullet2.RemoveAt(i);
                    score2-=10;
                    sp2.Play();
                }
            }
            groupBox1.Text = "\n1 Player HP: " + score2 + "\n2 Player HP: " + score1;
            if (score1 == 0)
            {
                while(MessageBox.Show("Player 1 Wins!!!") != DialogResult.OK)
                {

                }
                score1 = -1;
                timer1.Stop();
            }
            if (score2 ==0)
            {
                score2 = -1;
                while (MessageBox.Show("Player 2 Wins!!!") != DialogResult.OK)
                {

                }
                timer1.Stop();
            }
            else Refresh();
            toolStripStatusLabel1.Text = string.Format("bullets1: {0} , bullters2 : {1}", bullet_player1_x.Count,bullet_player2_x.Count);
        }

        private void Form1_KeyDown1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    dir1 = dir.Up;
                    position_player1.Y-=5;
                    break;
                case Keys.D:
                    dir1 = dir.Right;
                    position_player1.X+=5;

                    break;
                case Keys.S:
                    dir1 = dir.Down;
                    position_player1.Y+=5;
                    break;
                case Keys.A:
                    dir1 = dir.Left;
                    position_player1.X-=5;
                    break;
                case Keys.Space:
                    bullet1++;
                    bullet_player1_x.Add(position_player1.X+45);
                    bullet_player1_y.Add(position_player1.Y+45);
                    //sp.Play();
                    dirbullet1.Add(dir1);
                    break;

                case Keys.Up:
                    dir2 = dir.Up;
                    position_player2.Y -= 5;
                    break;
                case Keys.Right:
                    dir2 = dir.Right;
                    position_player2.X += 5;

                    break;
                case Keys.Down:
                    dir2 = dir.Down;
                    position_player2.Y += 5;
                    break;
                case Keys.Left:
                    dir2 = dir.Left;
                    position_player2.X -= 5;
                    break;
                case Keys.L:
                    bullet2++;
                    bullet_player2_x.Add(position_player2.X + 45);
                    bullet_player2_y.Add(position_player2.Y + 45);
                    //sp.Play();
                    dirbullet2.Add(dir2);
                    break;
            }
            changedir1(dir1);
            changedir2(dir2);
            
            Refresh();
        }
        
            private void changedir1(dir dir1)
        {
            switch (dir1)
            {
                case dir.Up:
                    bm_p1 = Resource1.player1_up;
                    break;
                case dir.Right:
                    bm_p1 = Resource1.player1_right;
                    break;
                case dir.Down:
                    bm_p1 = Resource1.player1_down;
                    break;
                case dir.Left:
                    bm_p1 = Resource1.player1_left;
                    break;

            }
        }
        private void changedir2(dir dir2)
        {
            switch (dir2)
            {
                case dir.Up:
                    bm_p2 = Resource1.player2_up;
                    break;
                case dir.Right:
                    bm_p2 = Resource1.player2_right;
                    break;
                case dir.Down:
                    bm_p2 = Resource1.player2_down;
                    break;
                case dir.Left:
                    bm_p2 = Resource1.player2_left;
                    break;

            }
        }

        private void Form1_KeyDown2(object sender, KeyEventArgs e)
        {

        }
    }
}
