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
                cube_dimetric.ortho(2.0);
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
                cube_isometric.ortho(0.8165);
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

    class Cube
    {
        public Cube(float side, int[] centerPoint)
        {
            this.side = side;
            this.centerPoint = centerPoint;
            vertecies = new Matrix[]
            {
                new Matrix(new float[1, 4] { { centerPoint[0] - side / 2, centerPoint[1] + side / 2, centerPoint[2] + side / 2, 1 } }),
                new Matrix(new float[1, 4] { { centerPoint[0] + side / 2, centerPoint[1] + side / 2, centerPoint[2] + side / 2, 1 } }),
                new Matrix(new float[1, 4] { { centerPoint[0] + side / 2, centerPoint[1] - side / 2, centerPoint[2] + side / 2, 1 } }),
                new Matrix(new float[1, 4] { { centerPoint[0] - side / 2, centerPoint[1] - side / 2, centerPoint[2] + side / 2, 1 } }),
                new Matrix(new float[1, 4] { { centerPoint[0] - side / 2, centerPoint[1] + side / 2, centerPoint[2] - side / 2, 1 } }),
                new Matrix(new float[1, 4] { { centerPoint[0] + side / 2, centerPoint[1] + side / 2, centerPoint[2] - side / 2, 1 } }),
                new Matrix(new float[1, 4] { { centerPoint[0] + side / 2, centerPoint[1] - side / 2, centerPoint[2] - side / 2, 1 } }),
                new Matrix(new float[1, 4] { { centerPoint[0] - side / 2, centerPoint[1] - side / 2, centerPoint[2] - side / 2, 1 } })
            };

        }

        public void Draw(Graphics graphics, Pen pen)
        {
            for (int j = 0; j < 6; ++j)
            {
                for (int i = 0; i < 7; i += 2)
                {
                    graphics.DrawLine(pen, vertecies[indicies[j, i]][0, 0], vertecies[indicies[j, i]][0, 1],
                        vertecies[indicies[j, i + 1]][0, 0], vertecies[indicies[j, i + 1]][0, 1]);
                }
            }
        }

        public void ortho(double angle)
        {
            Matrix ortho = new Matrix(new float[4, 4]{
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 }
            });
            for (int i = 0; i < 8; ++i)
            {
                vertecies[i] = vertecies[i] * ortho;
            }
        }
        public void rotationX(double angle)
        {
            angle = angle * (Math.PI / 180);
            Matrix rotationX = new Matrix(new float[4, 4]{
                {    1,                         0,                         0, 0 },
                {    0,    (float)Math.Cos(angle),    (float)Math.Sin(angle), 0 },
                {    0, -((float)Math.Sin(angle)),    (float)Math.Cos(angle), 0 },
                {    0,                         0,                         0, 1 }
            });
            for (int i = 0; i < 8; ++i)
            {
                vertecies[i] = vertecies[i] * rotationX;
            }
        }

        public void rotationY(double angle)
        {
            angle = angle * (Math.PI / 180);
            Matrix rotationY = new Matrix(new float[4, 4]{
                {    (float)Math.Cos(angle), 0, -((float)Math.Sin(angle)), 0 },
                {                         0, 1,                         0, 0 },
                {    (float)Math.Sin(angle), 0,    (float)Math.Cos(angle), 0 },
                {                         0, 0,                         0, 1 }
            });
            for (int i = 0; i < 8; ++i)
            {
                vertecies[i] = vertecies[i] * rotationY;
            }
        }

        public void rotationZ(double angle)
        {
            angle = angle * (Math.PI / 180);
            Matrix rotationZ = new Matrix(new float[4, 4]{
                {    (float)Math.Cos(angle), (float)Math.Sin(angle), 0, 0 },
                { -((float)Math.Sin(angle)), (float)Math.Cos(angle), 0, 0 },
                {                         0,                      0, 1, 0 },
                {                         0,                      0, 0, 1 }
            });
            for (int i = 0; i < 8; ++i)
            {
                vertecies[i] = vertecies[i] * rotationZ;
            }
        }

        public void translation(float l, float m, float n)
        {
            Matrix translation = new Matrix(new float[4, 4]{
                {  1, 0, 0, 0 },
                {  0, 1, 0, 0 },
                {  0, 0, 1, 0 },
                {  l, m, n, 1 }
            });
            for (int i = 0; i < 8; ++i)
            {
                vertecies[i] = vertecies[i] * translation;
            }
        }

        private int[,] indicies =
        {
            //Front face
            {0, 1, 1, 2, 2, 3, 3, 0 },
            //Back face
            {4, 5, 5, 6, 6, 7, 7, 4 },
            //Top face
            {3, 2, 2, 6, 6, 7, 7, 3 },
            //Bottom face
            {0, 1, 1, 5, 5, 4, 4, 0 },
            //Right face
            {1, 5, 5, 6, 6, 2, 2, 1 },
            //Left face
            {0, 4, 4, 7, 7, 3, 3, 0 }
        };
        private float side;
        public float Side
        {
            get { return side; }
            set { side = value; }
        }
        private Matrix[] vertecies;
        private int[] centerPoint;
    }

    public class Matrix
    {
        private float[,] data;

        private int m;
        public int M
        {
            get { return m; }
        }

        private int n;
        public int N
        {
            get { return n; }
        }

        public Matrix(float[,] data)
        {
            m = data.GetUpperBound(0) + 1;
            n = data.Length / m;
            this.data = data;
        }

        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            data = new float[m, n];
        }

        public float this[int x, int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }

        public static Matrix operator *(Matrix matrixA, Matrix matrixB)
        {
            if (matrixA.N != matrixB.M)
            {
                throw new ArgumentException("Matrixes cannot be multiplied!");
            }
            var result = new Matrix(matrixA.M, matrixB.N);
            for (int i = 0; i < matrixA.M; ++i)
            {
                for (int k = 0; k < matrixB.N; ++k)
                {
                    for (int j = 0; j < matrixB.N; ++j)
                    {
                        result[i, k] += matrixA[i, j] * matrixB[j, k];
                    }
                }
            }
            return result;
        }
    }
}
