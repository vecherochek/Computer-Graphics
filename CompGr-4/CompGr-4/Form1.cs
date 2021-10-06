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
        public static double[,] points_3D = { {0, 0, 2, 1},
                              {3, 0, 0, 1},
                              {0, 0, -2, 1},
                              {0, 4, 0, 1},
                              {3, 0, 0, 1},
                              {0, -4, 0, 1},
                              {0, 0, -2, 1},
                              {-3, 0, 0, 1},
                              {0, -4, 0, 1},
                              {0, 0, 2, 1},
                              {-3, 0, 0, 1},
                              {0, 4, 0, 1},
                              {0, 0, 2, 1}};
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
            g.DrawLine(pen, x, 0, x, ClientSize.Height / 2);
            g.DrawLine(pen2, x, ClientSize.Height / 2, x, ClientSize.Height);
            //OY
            g.DrawLine(pen, ClientSize.Width / 2, y, ClientSize.Width, y);
            g.DrawLine(pen2, ClientSize.Width / 2, y, 0, y);
            //OZ
            g.DrawLine(pen, ClientSize.Width / 2, y, (ClientSize.Width - ClientSize.Height) / 2, ClientSize.Height);
            g.DrawLine(pen2, ClientSize.Width / 2, y, (ClientSize.Width + ClientSize.Height) / 2, 0);

            //нарисуем фигуру
            if (checkBox1.Checked == true)
            {
                DrawWireframe(g, ClientSize.Width, ClientSize.Height);
            }
            else
            {
                DrawFill(g, ClientSize.Width, ClientSize.Height);
            }
            
        }
        private void DrawFill(Graphics g, int Form_x, int Form_y)
        {
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);

            int x = Form_x / 2;
            int y = Form_y / 2;


        }
        public static void DrawWireframe(Graphics g, int Form_x, int Form_y) 
        {
            Pen pen = new Pen(Color.Black);
            //Pen pen1 = new Pen(Color.Black, 10);

            int x = Form_x / 2;
            int y = Form_y / 2;

            double l = 1;
            double B = 45 * Math.PI / 180;
            //double B = Math.Atan(2);
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {l * Math.Cos(B), l * Math.Sin(B), 0, 0},
                              {0, 0, 0, 1}};
            points_2D = MatrixMult(points_3D, matrix);
            points_2D = MatrixNorm(points_2D);

            Point[] points0_2D = new Point[points_3D.GetUpperBound(0) + 1];
            for (int i = 0; i < points_2D.GetUpperBound(0) + 1; i++)
            {
                points0_2D[i] = new Point(Convert.ToInt32(points_2D[i, 0] * scale + x), Convert.ToInt32(y - points_2D[i, 1] * scale));
                //g.DrawEllipse(pen1, Convert.ToInt32(points_2D[i, 0] * scale + x), Convert.ToInt32(y - points_2D[i, 1] * scale), 1, 1);
            }

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(points0_2D);

            g.DrawPath(pen, path);           
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
        //отразить по OY
        private void button10_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, -1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //отразить по OX
        private void button9_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {-1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 1}};
            points_3D = MatrixMult(points_3D, matrix);
            this.Invalidate();
        }
        //отразить по OZ
        private void button11_Click(object sender, EventArgs e)
        {
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, -1, 0},
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
            double[,] points_1 = { {0, 0, 2, 1},
                              {3, 0, 0, 1},
                              {0, 0, -2, 1},
                              {0, 4, 0, 1},
                              {3, 0, 0, 1},
                              {0, -4, 0, 1},
                              {0, 0, -2, 1},
                              {-3, 0, 0, 1},
                              {0, -4, 0, 1},
                              {0, 0, 2, 1},
                              {-3, 0, 0, 1},
                              {0, 4, 0, 1},
                              {0, 0, 2, 1}};
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
    }
}
