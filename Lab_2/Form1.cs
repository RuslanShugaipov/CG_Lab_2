using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            pictureBox1.Image = new Bitmap(450, 450);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int factorial(int n)
        {
            int result = 1;
            for (int i = 2; i <= n; ++i)
            {
                result *= i;
            }
            return result;
        }

        private float coefficient_B(int i, int n, float t)
        {
            return (factorial(n) / (factorial(i) * factorial(n - i))
                * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i));
        }

        private List<PointF> points = new List<PointF>
        {
                new PointF(50, 100),
                new PointF(75, 120),
                new PointF(60, 80),
                new PointF(100, 50),
                new PointF(90, 150),
                new PointF(30, 130),
        };

        private PointF[] bezier()
        {
            int j = 0;
            float step = 0.01f;

            PointF[] result = new PointF[101];
            for (float t = 0; t < 1; t += step)
            {
                float x = 0;
                float y = 0;
                for (int i = 0; i < points.Count; ++i)
                {
                    float b = coefficient_B(i, points.Count - 1, t);
                    x += points[i].X * b;
                    y += points[i].Y * b;
                }
                result[j] = new PointF(x, y);
                ++j;
            }
            return result;
        }

        private void InitializeTimer()
        {
            timer1.Interval = 50;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Enabled = false;
        }

        private int i = 0;
        private PointF[] arr = new PointF[] { };

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                g.DrawLine(new Pen(Color.Black), arr[i], arr[i + 1]);
                pictureBox1.Invalidate();
                ++i;
                if (i == arr.Length - 1)
                {
                    timer1.Enabled = false;
                    i = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arr = bezier();
            listView1.Items.Clear();
            for (int i = 0; i < points.Count; ++i)
            {
                add_to_list_View(points[i]);
            }
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(pictureBox1.BackColor);
                points.Clear();
                pictureBox1.Invalidate();
                listView1.Items.Clear();
            }
        }

        private void add_to_list_View(PointF point)
        {
            ListViewItem item = new ListViewItem(point.X.ToString());
            item.SubItems.Add(point.Y.ToString());
            listView1.Items.Add(item);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count < 6)
            {
                add_to_list_View(new PointF((float)numericUpDown1.Value, (float)numericUpDown2.Value));
                if (points.Count < 6)
                {
                    points.Add(new PointF((float)numericUpDown1.Value, (float)numericUpDown2.Value));
                }
            }
        }
    }
}