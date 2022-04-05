using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        private Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(pictureBox1_Paint);
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

        private void draw_Bezier()
        {
            PointF[] points = new PointF[]
            {
                new PointF(50, 100),
                new PointF(75, 120),
                new PointF(60, 80),
                new PointF(100, 50),
                new PointF(90, 150),
                new PointF(30, 130),
            };

            int j = 0;
            float step = 0.01f;

            PointF[] result = new PointF[101];
            for (float t = 0; t < 1; t += step)
            {
                float x = 0;
                float y = 0;
                for (int i = 0; i < points.Length; ++i)
                {
                    float b = coefficient_B(i, points.Length - 1, t);
                    x += points[i].X * b;
                    y += points[i].Y * b;
                }
                result[j] = new PointF(x, y);
                if (j >= 1)
                {
                    graphics.DrawLines(new Pen(Color.Black), new PointF[] { result[j - 1], result[j] });
                    Thread.Sleep(50);
                }
                ++j;
            }
            //graphics.DrawLines(new Pen(Color.Red), points);
            //graphics.DrawLines(new Pen(Color.Black), result);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            draw_Bezier();
        }
    }
}