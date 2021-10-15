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
            textBox1.Text = "45";
        }
        public static double[,] points_3D = {
                                             //1   
                                             { 0,  4,  0, 1},
                                             { 0,  0,  2, 1},
                                             { 3,  0,  0, 1},
                                             //2
                                             { 0,  4,  0, 1},
                                             { 3,  0,  0, 1},
                                             { 0,  0, -2, 1},
                                             //3
                                             { 0,  4,  0, 1},
                                             { 0,  0, -2, 1},
                                             {-3,  0,  0, 1},
                                             //4
                                             { 0,  4,  0, 1},
                                             {-3,  0,  0, 1},
                                             { 0,  0,  2, 1},                                            
                                             //5
                                             { 0, -4,  0, 1},
                                             { 3,  0,  0, 1},
                                             { 0,  0,  2, 1},
                                             //6
                                             { 0, -4,  0, 1},
                                             { 0,  0, -2, 1},
                                             { 3,  0,  0, 1},
                                             //7
                                             { 0, -4,  0, 1},
                                             {-3,  0,  0, 1},
                                             { 0,  0, -2, 1},
                                             //8
                                             { 0, -4,  0, 1},
                                             { 0,  0,  2, 1},
                                             {-3,  0,  0, 1}};
        private double[,] points_1 = points_3D;
        public static double[,] points_2D;
        public static double scale = 50;
        string RB_text;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //нарисуем оси
            Pen pen = new Pen(Color.Black);
            Pen pen2 = new Pen(Color.Silver);
            pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;

            //0X
            g.DrawLine(pen, x, 0, x, y);
            g.DrawLine(pen2, x, y, x, ClientSize.Height);
            //OY
            g.DrawLine(pen, x, y, ClientSize.Width, y);
            g.DrawLine(pen2, x, y, 0, y);
            //OZ
            g.DrawLine(pen, x, y, x - y, ClientSize.Height);
            g.DrawLine(pen2, x, y, x + y, 0);

            //нарисуем фигуру
            double l = 1;
            double B = 45 * Math.PI / 180;
            //double B = Math.Atan(2);
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {l * Math.Cos(B), l * Math.Sin(B), 0, 0},
                              {0, 0, 0, 1}};
            if (checkBox1.Checked == true)
            {
                DrawWireframe(g, matrix, ClientSize.Width, ClientSize.Height);
            }
            else
            {
                DrawFill(g, matrix, ClientSize.Width, ClientSize.Height);
            }
        }

        private void DrawFill(Graphics g, double[,] matrix, int Form_x, int Form_y)
        {
            int x = Form_x / 2;
            int y = Form_y / 2;

            Point[] points0_2D = new Point[3];
            double[,] points_2D = MatrixMult(points_3D, matrix);
            double[] v = { 0.7, 0.7, -1, 0 };
            //double[,] v = MatrixMult(r, matrix);

            for (int i = 0, j = 0; i < points_3D.GetUpperBound(0); i += 3, j++)
            {
                double x1 = points_3D[i, 0];
                double y1 = points_3D[i, 1];
                double z1 = points_3D[i, 2];

                double x2 = points_3D[i + 1, 0];
                double y2 = points_3D[i + 1, 1];
                double z2 = points_3D[i + 1, 2];

                double x3 = points_3D[i + 2, 0];
                double y3 = points_3D[i + 2, 1];
                double z3 = points_3D[i + 2, 2];

                double nx = (y2 - y1) * (z3 - z2) - (z2 - z1) * (y3 - y2);
                double ny = (z2 - z1) * (x3 - x2) - (x2 - x1) * (z3 - z2);
                double nz = (x2 - x1) * (y3 - y2) - (y2 - y1) * (x3 - x2);

                double check = nx * v[0] + ny * v[1] + nz * v[2];

                if (check > 0)
                {
                    points0_2D[0] = new Point(Convert.ToInt32(points_2D[i, 0] * scale + x), Convert.ToInt32(y - points_2D[i, 1] * scale));
                    points0_2D[1] = new Point(Convert.ToInt32(points_2D[i + 1, 0] * scale + x), Convert.ToInt32(y - points_2D[i + 1, 1] * scale));
                    points0_2D[2] = new Point(Convert.ToInt32(points_2D[i + 2, 0] * scale + x), Convert.ToInt32(y - points_2D[i + 2, 1] * scale));

                    double a_l = Math.Sqrt(nx * nx + ny * ny + nz * nz);
                    double b_l = Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);

                    double cos_ = check / (a_l * b_l);

                    SolidBrush brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(246 * cos_), Convert.ToInt32(146 * cos_), Convert.ToInt32(114 * cos_)));

                    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                    path.AddLines(points0_2D);

                    g.FillPath(brush, path);
                    path.Dispose();
                }
            }
        }
        public static void DrawWireframe(Graphics g, double[,] matrix, int Form_x, int Form_y)
        {
            Pen pen = new Pen(Color.Black);

            int x = Form_x / 2;
            int y = Form_y / 2;

            double[,] points_2D = MatrixMult(points_3D, matrix);
            Point[] points0_2D = new Point[points_3D.GetUpperBound(0) + 1];
            for (int i = 0; i < points_2D.GetUpperBound(0) + 1; i++)
            {
                points0_2D[i] = new Point(Convert.ToInt32(points_2D[i, 0] * scale + x), Convert.ToInt32(y - points_2D[i, 1] * scale));
            }

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(points0_2D);

            g.DrawPath(pen, path);
            path.Dispose();
        }
        public static double[,] MatrixMult(double[,] matrixA, double[,] matrixB)
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
        public static double[,] MatrixNorm(double[,] matrixA)
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
        //перемещение по OX в +
        private void button1_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {1, 0, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //перемещение по OX в -
        private void button2_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {-1, 0, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //перемещение по OY в +
        private void button3_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 1, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //перемещение по OY в -
        private void button4_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, -1, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //перемещение по OZ в +
        private void button5_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 1, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //перемещение по OZ в -
        private void button6_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, -1, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //уменьшить
        private void button7_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 1.2}};
            points_3D = MatrixMult(points_3D, matrix);
            points_3D = MatrixNorm(points_3D);
            this.Invalidate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 0.835}};
            points_3D = MatrixMult(points_3D, matrix);
            points_3D = MatrixNorm(points_3D);
            this.Invalidate();
        }
        //отразить по OX
        private void button10_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, -1, 0, 0},
                              {0, 0, -1, 0},
                              {0, 0, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //отразить по OY
        private void button9_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {-1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, -1, 0},
                              {0, 0, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //отразить по OZ
        private void button11_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {-1, 0, 0, 0},
                              {0, -1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                RB_text = (sender as RadioButton).Text;
        }
        //поворот вокруг осей на угол
        private void button12_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double a))
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
            double[,] matrix_OX = new double[,] { {1, 0, 0, 0},
                              {0, Math.Cos(a), Math.Sin(a), 0},
                              {0, -Math.Sin(a), Math.Cos(a), 0},
                              {0, 0, 0, 1}};
            double[,] matrix_OY = new double[,] { {Math.Cos(a), 0, -Math.Sin(a), 0},
                              {0, 1, 0, 0},
                              {Math.Sin(a), 0, Math.Cos(a), 0},
                              {0, 0, 0, 1}};
            double[,] matrix_OZ = new double[,] { {Math.Cos(a), Math.Sin(a), 0, 0},
                              {-Math.Sin(a), Math.Cos(a), 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 1}};
            switch (RB_text)
            {
                case "поворот по OX":
                    points_3D = MatrixMult(points_3D, matrix_OX);
                    break;
                case "поворот по OY":
                    points_3D = MatrixMult(points_3D, matrix_OY);
                    break;
                case "поворот по OZ":
                    points_3D = MatrixMult(points_3D, matrix_OZ);
                    break;
                default:
                    break;
            }
            points_3D = MatrixNorm(points_3D);
            this.Invalidate();
        }
        //восстановить
        private void button13_Click(object sender, EventArgs e)
        {
            points_3D = points_1;
            this.Invalidate();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            switch (RB_text)
            {
                case "косоугольная":
                    Form2 form2 = new Form2();
                    form2.Owner = this;
                    form2.ShowDialog();
                    break;
                case "центральная oдноточечная":
                    Form3 form3 = new Form3();
                    form3.Owner = this;
                    form3.ShowDialog();
                    break;
                case "паралленьная ортогональной":
                    Form4 form4 = new Form4();
                    form4.Owner = this;
                    form4.ShowDialog();
                    break;
                default:
                    break;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                RB_text = (sender as RadioButton).Text;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double a = 2 * Math.PI / 180;
            double[,] matrix_OX = new double[,] { {1, 0, 0, 0},
                              {0, Math.Cos(a), Math.Sin(a), 0},
                              {0, -Math.Sin(a), Math.Cos(a), 0},
                              {0, 0, 0, 1}};
            double[,] matrix_OY = new double[,] { {Math.Cos(a), 0, -Math.Sin(a), 0},
                              {0, 1, 0, 0},
                              {Math.Sin(a), 0, Math.Cos(a), 0},
                              {0, 0, 0, 1}};
            double[,] matrix_OZ = new double[,] { {Math.Cos(a), Math.Sin(a), 0, 0},
                              {-Math.Sin(a), Math.Cos(a), 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 1}};
            switch (RB_text)
            {
                case "поворот по OX":
                    points_3D = MatrixMult(points_3D, matrix_OX);
                    break;
                case "поворот по OY":
                    points_3D = MatrixMult(points_3D, matrix_OY);
                    break;
                case "поворот по OZ":
                    points_3D = MatrixMult(points_3D, matrix_OZ);
                    break;
                default:
                    break;
            }
            points_3D = MatrixNorm(points_3D);
            this.Invalidate();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                RB_text = (sender as RadioButton).Text;
                if (RB_text != "остановить")
                    timer1.Enabled = true;
            }
            else timer1.Enabled = false;
        }
    }
}
