using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGr_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int[,] points = { {-2, 2, 1},
                              {0, 2, 1},
                              {0, 1, 1},
                              {2, 1, 1},};
        int[,] points_0 = { {-2, 2, 1},
                              {0, 2, 1},
                              {0, 1, 1},
                              {2, 1, 1}};
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;
            int scale = 70;

            int rec1_x = points[0, 0];
            int rec1_y = points[0, 1];
            int rec1_w = Math.Abs(points[0, 0] - points[1, 0]);

            int rec2_x = points[2, 0];
            int rec2_y = points[2, 1];
            int rec2_w = Math.Abs(points[2, 0] - points[3, 0]);

            g.Clear(Color.SeaShell);
            g.FillRectangle(brush, rec1_x * scale + x, y - rec1_y * scale, rec1_w  * scale, rec1_w * scale);
            g.FillRectangle(brush, rec2_x * scale + x, y - rec2_y * scale, rec2_w * scale, rec2_w * scale);
            g.DrawLine(pen, x, 0, x, ClientSize.Height);
            g.DrawLine(pen, 0, y, ClientSize.Width, y);
        }   
        private int[,] MatrixMult(int[,] matrixA, int[,] matrixB)
        {
            int A_Rows = matrixA.GetUpperBound(0) + 1;
            int A_Columns = matrixA.GetUpperBound(1) + 1;
            int B_Columns = matrixB.GetUpperBound(1) + 1;

            var matrix = new int[A_Rows, B_Columns];

            for (int i = 0; i < A_Rows; i++)
            {
                for (int j = 0; j < B_Columns; j++)
                {
                    matrix[i, j] = 0;

                    for (int k = 0; k < A_Columns; k++)
                    {
                        matrix[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }
            return matrix;
        }
        private int[,] MatrixDiv(int[,] matrixA, int s)
        {
            int A_Rows = matrixA.GetUpperBound(0) + 1;
            int A_Columns = matrixA.GetUpperBound(1) + 1;

            var matrix = new int[A_Rows, A_Columns];

            for (int i = 0; i < A_Rows; i++)
            {
                for (int j = 0; j < A_Columns; j++)
                {
                    matrix[i, j] = 0;

                    for (int k = 0; k < A_Columns; k++)
                    {
                        matrix[i, j] += matrixA[i, k] * s;
                    }
                }
            }
            return matrix;
        }
        //смещение впрво
        private void button1_Click(object sender, EventArgs e)
        {
            int[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {1, 0, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //смещение влево
        private void button2_Click(object sender, EventArgs e)
        {
            int[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {-1, 0, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //смещение вверх
        private void button3_Click(object sender, EventArgs e)
        {
            int[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {0, 1, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //смещение вниз        
        private void button4_Click_1(object sender, EventArgs e)
        {
            int[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {0, -1, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //увеличить-----переделать
        private void button5_Click(object sender, EventArgs e)
        {
            int[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {0, 0, 2}};
            points = MatrixMult(points, matr);
            points = MatrixDiv(points, 2);
            this.Invalidate();
        }
        //восстановить
        private void button9_Click(object sender, EventArgs e)
        {
            points = points_0;
            this.Invalidate();
        }

        
    }
}
