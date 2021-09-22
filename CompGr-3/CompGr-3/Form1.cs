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
        double [,] points = { {-2, 2, 1},
                              {0, 2, 1},
                              {-2, 0, 1},
                              {0, 1, 1},
                              {2, 1, 1},
                              {0, -1, 1}};       
        Graphics g;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;
            int scale = 70;

            double rec1_x = points[0, 0];
            double rec1_y = points[0, 1];
            double rec1_w = Math.Abs(points[0, 0] - points[1, 0]);

            double rec2_x = points[3, 0];
            double rec2_y = points[3, 1];
            double rec2_w = Math.Abs(points[3, 0] - points[4, 0]);

            g.Clear(Color.SeaShell);
            g.FillRectangle(brush, Convert.ToInt32(rec1_x * scale + x), Convert.ToInt32(y - rec1_y * scale), Convert.ToInt32(rec1_w  * scale), Convert.ToInt32(rec1_w * scale));
            g.FillRectangle(brush, Convert.ToInt32(rec2_x * scale + x), Convert.ToInt32(y - rec2_y * scale), Convert.ToInt32(rec2_w * scale), Convert.ToInt32(rec2_w * scale));
            g.DrawLine(pen, x, 0, x, ClientSize.Height);
            g.DrawLine(pen, 0, y, ClientSize.Width, y);
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
        private double[,] MatrixDiv(double[,] matrixA, double s)
        {
            int A_Rows = matrixA.GetUpperBound(0) + 1;
            int A_Columns = matrixA.GetUpperBound(1) + 1;

            var matrix = new double[A_Rows, A_Columns];

            for (int i = 0; i < A_Rows; i++)
            {
                for (int j = 0; j < A_Columns; j++)
                {                               
                    matrix[i, j] = matrixA[i, j] / s;                    
                }
            }
            return matrix;
        }
        //смещение впрво
        private void button1_Click(object sender, EventArgs e)
        {
            double[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {1, 0, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //смещение влево
        private void button2_Click(object sender, EventArgs e)
        {
            double[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {-1, 0, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //смещение вверх
        private void button3_Click(object sender, EventArgs e)
        {
            double[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {0, 1, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //смещение вниз        
        private void button4_Click_1(object sender, EventArgs e)
        {
            double[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {0, -1, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //увеличить
        private void button5_Click(object sender, EventArgs e)
        {
            points = MatrixDiv(points, 0.835);
            this.Invalidate();
        }
        //уменьшить
        private void button6_Click(object sender, EventArgs e)
        {
            points = MatrixDiv(points, 1.2);
            this.Invalidate();
        }
        //OX
        private void button7_Click(object sender, EventArgs e)
        {
            double[,] matr = { {1, 0, 0},
                            {0, -1, 0},
                            {0, 0, 1}};
            points = MatrixMult(points, matr);

            double tmp = points[0, 1];
            points[0, 1] = points[2, 1];
            points[2, 1] = tmp;

            tmp = points[3, 1];
            points[3, 1] = points[5, 1];
            points[5, 1] = tmp;

            this.Invalidate();
        }
        //OY
        private void button8_Click(object sender, EventArgs e)
        {
            double[,] matr = { {-1, 0, 0},
                            {0, 1, 0},
                            {0, 0, 1}};
            points = MatrixMult(points, matr);

            double tmp = points[0, 0];
            points[0, 0] = points[1, 0];
            points[1, 0] = tmp;

            tmp = points[3, 0];
            points[3, 0] = points[4, 0];
            points[4, 0] = tmp;

            this.Invalidate();
        }
        //восстановить
        private void button9_Click(object sender, EventArgs e)
        {
            double[,] points_0 = { {-2, 2, 1},
                              {0, 2, 1},
                              {-2, 0, 1},
                              {0, 1, 1},
                              {2, 1, 1},
                              {0, -1, 1}};
            points = points_0;
            this.Invalidate();
        }
        //нужно написать отрисовку фигуры после поворота
        private void button10_Click(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox1.Text, out a))
            {
                MessageBox.Show(
                "Введите угол в градусах",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            a = a * Math.PI / 180;
            double[,] matr = { {Math.Cos(a), Math.Sin(a), 0},
                            {-Math.Sin(a), Math.Cos(a), 0},
                            {0, 0, 1}};
            points = MatrixMult(points, matr);
        }
    }
}
