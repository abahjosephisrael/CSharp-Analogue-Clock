using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analog_Clock
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        int WIDTH = 300, HEIGHT = 300, secHAND = 140, minHAND = 110, hrHAND = 80;

        //center
        int cx, cy;
        string meri;
        Bitmap bmp;
        Graphics g;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);

            // Center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            //Background color
            this.BackColor = Color.White;

            //Timer
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        public Form1()
        {
            InitializeComponent();
        }



        private void t_Tick(object sender, EventArgs e)
        {
            // create a graphics
            g = Graphics.FromImage(bmp);
            // get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;
            
            if (hh > 12)
            {
                hh = hh - 12;
                meri = "PM";
            }
            else if(hh==0)
                {
                hh = 12;
                meri = "AM";
                }
            else
            {
                meri = "AM";
            }
            lblDigital.Text = "[ " + hh + ":" + mm + ":" + ss + "  " + meri + " ]";

            int[] handCoord = new int[2];

            //clear
            g.Clear(Color.Transparent);

            g.DrawEllipse (new Pen(Color.Black, 1f), 0, 0, WIDTH, HEIGHT);

            // draw numbers
            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new Point(140, 2));
            g.DrawString("11", new Font("Arial", 12), Brushes.Black, new Point(75, 25));
            g.DrawString("10", new Font("Arial", 12), Brushes.Black, new Point(25, 75));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new Point(0, 140));
            g.DrawString("8", new Font("Arial", 12), Brushes.Black, new Point(15, 210));
            g.DrawString("7", new Font("Arial", 12), Brushes.Black, new Point(75, 262));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new Point(142, 282));
            g.DrawString("5", new Font("Arial", 12), Brushes.Black, new Point(215, 262));
            g.DrawString("4", new Font("Arial", 12), Brushes.Black, new Point(270,208));
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new Point(286, 140));
            g.DrawString("2", new Font("Arial", 12), Brushes.Black, new Point(262, 75));
            g.DrawString("1", new Font("Arial", 12), Brushes.Black, new Point(210, 25));

            //second hand
            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //minute hand
            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //hour hand
            handCoord = hrCoord(hh%12, mm, hrHAND);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //load picture into picture box
            pictureBox1.Image = bmp;

            //display time
            if (hh > 12)
            {
                hh = hh - 12;
                //this.Text = "Analog Clock [ " + hh + ":" + mm + ":" + ss + " "+ meri + " ]";
            }
            else
            {
                //this.Text = "Analog Clock [ " + hh + ":" + mm + ":" + ss + " " + meri + " ]";
            }
           // this.Text = "Analog Clock [ " + hh + ":" + mm + ":" + ss + meri + "]";

            //dispose
            g.Dispose();

        }

        //Coordinate for minute
        private int [] msCoord(int val, int hlen)
        {
            int [] coord = new int[2];
            val *= 6; //
            if(val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        //coord for hour hand

        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];


            // each second makes 30 degrees
            // each minute makes 0.5 degree
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

    }
}
