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
            this.KeyPreview = true;
        }
        double scale = 70;
        double mouse_x = 0;
        double mouse_y = 0;
        double[,] points = { {-2, 2, 1},
                              {0, 2, 1},
                              {0, 1, 1},
                              {0, 1, 1},
                              {2, 1, 1},
                              {2, -1, 1},
                              {0, -1, 1},
                              {0, 0, 1},
                              {-2, 0, 1}};       
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;

            Point[] points_0 = new Point[points.GetUpperBound(0) + 1];
            for (int i = 0; i < points.GetUpperBound(0) + 1; i++)
            {
                points_0[i] = new Point(Convert.ToInt32(points[i, 0] * scale + x), Convert.ToInt32(y - points[i, 1] * scale));
            }
         
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(points_0);

            g.FillPath(brush, path);
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
        private double[,] MatrixNorm(double[,] matrixA)
        {
            int A_Rows = matrixA.GetUpperBound(0) + 1;
            int A_Columns = matrixA.GetUpperBound(1) + 1;

            var matrix = new double[A_Rows, A_Columns];

            for (int i = 0; i < A_Rows; i++)
            {
                for (int j = 0; j < A_Columns; j++)
                {                               
                    matrix[i, j] = matrixA[i, j] / matrixA[i, 2];                    
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
            double[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {0, 0, 0.835}};
            points = MatrixMult(points, matr);
            points = MatrixNorm(points);
            this.Invalidate();
        }
        //уменьшить
        private void button6_Click(object sender, EventArgs e)
        {
            double[,] matr = { {1, 0, 0},
                            {0, 1, 0},
                            {0, 0, 1.2}};
            points = MatrixMult(points, matr);
            points = MatrixNorm(points);
            this.Invalidate();
        }
        //OX
        private void button7_Click(object sender, EventArgs e)
        {
            double[,] matr = { {1, 0, 0},
                            {0, -1, 0},
                            {0, 0, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //OY
        private void button8_Click(object sender, EventArgs e)
        {
            double[,] matr = { {-1, 0, 0},
                            {0, 1, 0},
                            {0, 0, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //восстановить
        private void button9_Click(object sender, EventArgs e)
        {
            double[,] points_1 = { {-2, 2, 1},
                              {0, 2, 1},
                              {0, 1, 1},
                              {0, 1, 1},
                              {2, 1, 1},
                              {2, -1, 1},
                              {0, -1, 1},
                              {0, 0, 1},
                              {-2, 0, 1}};
            points = points_1;
            this.Invalidate();
        }
        //поворот на угол относительно начала координат
        private void button10_Click(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox1.Text, out a))
            {
                MessageBox.Show(
                "Введите угол в градусах,а дробные числа через запятую.",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            a = a * Math.PI / 180;
            double[,] matr = { {Math.Cos(a), -Math.Sin(a), 0},
                            {Math.Sin(a), Math.Cos(a), 0},
                            {0, 0, 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //поворот на угол относительно точки
        private void button11_Click(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox1.Text, out a))
            {
                MessageBox.Show(
                "Введите угол в градусах,а дробные числа через запятую.",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            a = a * Math.PI / 180;
            double[,] matr = { {Math.Cos(a), -Math.Sin(a), 0},
                            {Math.Sin(a), Math.Cos(a), 0},
                            {mouse_x*(-Math.Cos(a) + 1) - mouse_y * Math.Sin(a), mouse_y*(-Math.Cos(a) + 1) + mouse_x * Math.Sin(a), 1}};
            points = MatrixMult(points, matr);
            this.Invalidate();
        }
        //X = Y
        private void button12_Click(object sender, EventArgs e)
        {
            double[,] matr = { {0, 1, 0},
                               {1, 0, 0},
                               {0, 0, 1}};
            points = MatrixMult(points, matr);
            points = MatrixNorm(points);
            this.Invalidate();
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            double x = ClientSize.Width / 2;
            double y = ClientSize.Height / 2;
            if (e.Button == MouseButtons.Left)
            {
                mouse_x = (Convert.ToDouble(e.X) - x) / scale;
                mouse_y = (y - Convert.ToDouble(e.Y)) / scale;
                textBox2.Text = "(" + Convert.ToString(Math.Round(mouse_x, 2)) + ";" + Convert.ToString(Math.Round(mouse_y, 2)) + ")";
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    button1.PerformClick();
                    break;
                case Keys.Left:
                    button2.PerformClick();
                    break;
                case Keys.Up:
                    button3.PerformClick();
                    break;
                case Keys.Down:
                    button4.PerformClick();
                    break;
                case Keys.Add:
                    button5.PerformClick();
                    break;
                case Keys.Subtract:
                    button6.PerformClick();
                    break;
                default:
                    break;
            }
        }
    }
}
