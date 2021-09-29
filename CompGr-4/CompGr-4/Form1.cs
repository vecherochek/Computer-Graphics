using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGr_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double[,] points_3D = { {0, 0, 3, 1},
                              {1, 0, 0, 1},
                              {0, 0, -3, 1},
                              {0, 2, 0, 1},
                              {1, 0, 0, 1},
                              {0, -2, 0, 1},
                              {0, 0, -3, 1},
                              {1, 0, 0, 1},
                              {-1, 0, 0, 1},
                              {0, -2, 0, 1},
                              {0, 0, 3, 1},
                              {-1, 0, 0, 1},
                              {0, 2, 0, 1},
                              {0, 0, 3, 1}};
        double[,] points_2D;
        double scale = 70;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            Pen pen1 = new Pen(Color.Black, 10);
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;
            double l = 0.5;
            double B = Math.Atan(2);
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {l * Math.Cos(B), l * Math.Sin(B), 0, 0},
                              {0, 0, 0, 1}};

            Point[] points0_2D = new Point[points_3D.GetUpperBound(0) + 1];

            points_2D = MatrixMult(points_3D, matrix);
            points_2D = MatrixNorm(points_2D);

            for (int i = 0; i < points_2D.GetUpperBound(0) + 1; i++)
            {
                points0_2D[i] = new Point(Convert.ToInt32(points_2D[i, 0] * scale + x), Convert.ToInt32(y - points_2D[i, 1] * scale));
                g.DrawEllipse(pen1, Convert.ToInt32(points_2D[i, 0] * scale + x), Convert.ToInt32(y - points_2D[i, 1] * scale), 1, 1);
            }


            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(points0_2D);

            g.DrawPath(pen, path);
            g.DrawLine(pen, x, 0, x, ClientSize.Height / 2);
            g.DrawLine(pen, ClientSize.Width / 2, y, ClientSize.Width, y);
            g.DrawLine(pen, ClientSize.Width / 2, y, ClientSize.Width - ClientSize.Height, ClientSize.Height);
        }
        private double[,] MatrixMult(double[,] matrixA, double[,] matrixB)
        {
            int A_Rows = matrixA.GetUpperBound(0) + 1;
            int A_Columns = matrixA.GetUpperBound(1) + 1;
            int B_Columns = matrixB.GetUpperBound(1) + 1;

            var matrix = new double[A_Rows, B_Columns];

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
        private double[,] MatrixNorm(double[,] matrixA)
        {
            int A_Rows = matrixA.GetUpperBound(0) + 1;
            int A_Columns = matrixA.GetUpperBound(1) + 1;

            var matrix = new double[A_Rows, A_Columns];

            for (int i = 0; i < A_Rows; i++)
            {
                for (int j = 0; j < A_Columns; j++)
                {
                    matrix[i, j] = matrixA[i, j] / matrixA[i, A_Columns - 1];
                }
            }
            return matrix;
        }
    }
}
