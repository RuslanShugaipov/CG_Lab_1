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

namespace Lab_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = 500;
            this.Height = 500;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(Color.Coral);

            graphics.DrawLine(pen, 2, 2, 400, 450);
        }
    }

    class Cube
    {
        public Cube(float side)
        {
            this.side = side;
            vertecies = new Matrix[]
            {
                new Matrix(new float[1, 3] { { -1.0f * side, -1.0f * side,  1.0f * side } }),
                new Matrix(new float[1, 3] { {  1.0f * side, -1.0f * side,  1.0f  * side} }),
                new Matrix(new float[1, 3] { {  1.0f * side,  1.0f * side,  1.0f  * side} }),
                new Matrix(new float[1, 3] { { -1.0f * side,  1.0f * side,  1.0f  * side} }),
                new Matrix(new float[1, 3] { { -1.0f * side, -1.0f * side, -1.0f  * side} }),
                new Matrix(new float[1, 3] { {  1.0f * side, -1.0f * side, -1.0f  * side} }),
                new Matrix(new float[1, 3] { {  1.0f * side,  1.0f * side, -1.0f  * side} }),
                new Matrix(new float[1, 3] { { -1.0f * side,  1.0f * side, -1.0f  * side} })
            };
    }
        private float side = 1;
        public float Side
        {
            get { return side; }
            set { this.side = value; }
        }
        private Matrix[] vertecies;
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
            this.m = data.GetUpperBound(0) + 1;
            this.n = data.Length;
            this.data = data;
        }

        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            this.data = new float[m, n];
        }

        public float this[int x, int y]
        {
            get { return this.data[x, y]; }
            set { this.data[x, y] = value; }
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
                for (int k = 0; k < matrixB.N; ++i)
                {
                    for (int j = 0; j < matrixA.M; ++j)
                    {
                        result[i, j] += matrixA[i, j] * matrixB[j, k];
                    }
                }
            }
            return result;
        }
    }
}
