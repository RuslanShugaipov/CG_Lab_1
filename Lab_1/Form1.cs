using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;

namespace Lab_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Width = 500;
            Height = 500;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        double angleX = 0;
        double angleY = 0;
        double angleZ = 0;
        bool is_draw_dimetric = false;
        bool is_draw_isometric = false;

        private void button1_Click(object sender, EventArgs e)
        {
            angleY += 5;
            button6_Click(sender, e);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            angleZ += 5;
            button6_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            angleX += 5;
            button6_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            is_draw_dimetric = !is_draw_dimetric;
            using (var g = Graphics.FromImage(pictureBox2.Image))
            {
                g.Clear(pictureBox2.BackColor);
                pictureBox2.Invalidate();
                float centerX = (float)pictureBox1.Width / 2;
                float centerY = (float)pictureBox1.Height / 2;
                g.TranslateTransform(centerX, centerY);
                Cube cube_dimetric = new Cube(50.0f, new int[] { 0, 0, 0 });
                Pen pen = new Pen(Brushes.Red);
                cube_dimetric.rotationY(22.208);
                cube_dimetric.rotationX(20.705);
                cube_dimetric.ortho();
                if (is_draw_dimetric)
                    cube_dimetric.Draw(g, pen);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            is_draw_isometric = !is_draw_isometric;
            using (var g = Graphics.FromImage(pictureBox3.Image))
            {
                g.Clear(pictureBox3.BackColor);
                pictureBox3.Invalidate();
                float centerX = (float)pictureBox3.Width / 2;
                float centerY = (float)pictureBox3.Height / 2;
                g.TranslateTransform(centerX, centerY);
                Cube cube_isometric = new Cube(50.0f, new int[] { 0, 0, 0 });
                Pen pen = new Pen(Brushes.Red);
                cube_isometric.rotationY(45.0);
                cube_isometric.rotationX(35.26);
                cube_isometric.ortho();
                if (is_draw_isometric)
                    cube_isometric.Draw(g, pen);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(pictureBox1.BackColor);
                pictureBox1.Invalidate();
                float centerX = (float)pictureBox1.Width / 2;
                float centerY = (float)pictureBox1.Height / 2;
                g.TranslateTransform(centerX, centerY);
                Cube cube = new Cube(50.0f, new int[] { 0, 0, 0 });
                Pen pen = new Pen(Brushes.Red);
                cube.rotationX(angleX);
                cube.rotationY(angleY);
                cube.rotationZ(angleZ);
                cube.Draw(g, pen);
            }
        }
    }
}
