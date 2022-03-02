using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    class Cube
    {
        public Cube(float side, int[] centerPoint)
        {
            this.side = side;
            this.centerPoint = new Matrix(new float[1, 4] { { centerPoint[0], centerPoint[1], centerPoint[2], 1 } });
            vertecies = new Matrix[]
            {
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] - side / 2, this.centerPoint[0, 1] + side / 2, this.centerPoint[0, 2] + side / 2, this.centerPoint[0, 3] } }),
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] + side / 2, this.centerPoint[0, 1] + side / 2, this.centerPoint[0, 2] + side / 2, this.centerPoint[0, 3] } }),
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] + side / 2, this.centerPoint[0, 1] - side / 2, this.centerPoint[0, 2] + side / 2, this.centerPoint[0, 3] } }),
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] - side / 2, this.centerPoint[0, 1] - side / 2, this.centerPoint[0, 2] + side / 2, this.centerPoint[0, 3] } }),
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] - side / 2, this.centerPoint[0, 1] + side / 2, this.centerPoint[0, 2] - side / 2, this.centerPoint[0, 3] } }),
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] + side / 2, this.centerPoint[0, 1] + side / 2, this.centerPoint[0, 2] - side / 2, this.centerPoint[0, 3] } }),
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] + side / 2, this.centerPoint[0, 1] - side / 2, this.centerPoint[0, 2] - side / 2, this.centerPoint[0, 3] } }),
                new Matrix(new float[1, 4] { { this.centerPoint[0, 0] - side / 2, this.centerPoint[0, 1] - side / 2, this.centerPoint[0, 2] - side / 2, this.centerPoint[0, 3] } })
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

        public void ortho()
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
        private Matrix centerPoint;
    }
}
