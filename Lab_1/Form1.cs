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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Cube cube = new Cube(50.0f, new int[]{ pictureBox1.Width / 2, pictureBox1.Height / 2 });
            Pen pen = new Pen(Brushes.Red);
            cube.Draw(e.Graphics, pen);
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
                new Matrix(new float[1, 3] { { centerPoint[0] - side / 2, centerPoint[1] - side / 2,  1.0f  * side} }),
                new Matrix(new float[1, 3] { { centerPoint[0] + side / 2, centerPoint[1] - side / 2,  1.0f  * side} }),
                new Matrix(new float[1, 3] { { centerPoint[0] + side / 2, centerPoint[1] + side / 2,  1.0f  * side} }),
                new Matrix(new float[1, 3] { { centerPoint[0] - side / 2, centerPoint[1] + side / 2,  1.0f  * side} }),
                new Matrix(new float[1, 3] { { centerPoint[0] - side / 2, centerPoint[1] - side / 2, -1.0f  * side} }),
                new Matrix(new float[1, 3] { { centerPoint[0] + side / 2, centerPoint[1] - side / 2, -1.0f  * side} }),
                new Matrix(new float[1, 3] { { centerPoint[0] + side / 2, centerPoint[1] + side / 2, -1.0f  * side} }),
                new Matrix(new float[1, 3] { { centerPoint[0] - side / 2, centerPoint[1] + side / 2, -1.0f  * side} })
            };

        }

        public void Draw(Graphics graphics, Pen pen)
        {
            for (int i = 0; i < 7; i += 2)
            {
                graphics.DrawLine(pen, vertecies[indicies[0, i]][0, 0], vertecies[indicies[0, i]][0, 1],
                    vertecies[indicies[0, i + 1]][0, 0], vertecies[indicies[0, i + 1]][0, 1]);
            }
        }

        private int[,] indicies =
        {
            //Front face
            {0, 1, 1, 2, 2, 3, 3, 0 },
            //Back face
            //{4, 5, 6, 7 },
            ////Top face
            //{3, 2, 6, 7 },
            ////Bottom face
            //{0, 1, 5, 4 },
            ////Right face
            //{1, 5, 6, 2 },
            ////Left face
            //{0, 4, 7, 3 }
        };
        private float side;
        public float Side
        {
            get { return side; }
            set { this.side = value; }
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
