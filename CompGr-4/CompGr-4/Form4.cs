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
    public partial class Form4 : Form
    {
        public Form4()
        {        
            Form1 main = this.Owner as Form1;                     
            InitializeComponent();
            radioButton1.Checked = true;
        }
        string RB_text;
        double[,] points_2D;
        double[,] matrix ={ {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 0, 0},
                              {0, 0, 0, 1}};
        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;

            //нарисуем оси
            Pen pen = new Pen(Color.Black);
            g.DrawLine(pen, x, 0, x, y * 2);
            g.DrawLine(pen, 0, y, x * 2, y);

            //нарисуем проекцию
            DrawWireframe(g, matrix, ClientSize.Width, ClientSize.Height);
        }
        private void DrawWireframe(Graphics g, double[,] matrix, int Form_x, int Form_y)
        {
            Pen pen = new Pen(Color.Black);

            int x = Form_x / 2;
            int y = Form_y / 2;

            points_2D = Form1.MatrixMult(Form1.points_3D, matrix);
            points_2D = Form1.MatrixNorm(points_2D);

            Point[] points0_2D = new Point[Form1.points_3D.GetUpperBound(0) + 1];
            if (RB_text == "сбоку")
            {
                for (int i = 0; i < points_2D.GetUpperBound(0) + 1; i++)
                {
                    points0_2D[i] = new Point(Convert.ToInt32(points_2D[i, 2] * Form1.scale + x), Convert.ToInt32(y - points_2D[i, 1] * Form1.scale));
                }
            }
            else
            {
                for (int i = 0; i < points_2D.GetUpperBound(0) + 1; i++)
                {
                    points0_2D[i] = new Point(Convert.ToInt32(points_2D[i, 0] * Form1.scale + x), Convert.ToInt32(y - points_2D[i, 1] * Form1.scale));
                }
            }
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(points0_2D);

            g.DrawPath(pen, path);
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                RB_text = (sender as RadioButton).Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double[,] matrix_1 ={ {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 0, 0},
                              {0, 0, 0, 1}};
            double[,] matrix_2 ={ {0, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 1, 0},
                              {0, 0, 0, 1}};
            double[,] matrix_3 ={ {0.93, 0.13, -0.35, 0},
                              {0, 0.93, 0.35, 0},
                              {0.38, -0.33, 0.36, 0},
                              {0, 0, 0, 1}};
            double[,] matrix_4 ={ {0.71, 0.41, -0.58, 0},
                              {0, 0.82, 0.58, 0},
                              {0.71, -0.41, 0.58, 0},
                              {0, 0, 0, 1}};
            switch (RB_text)
            {
                case "спереди":
                    matrix = matrix_1;
                    this.Invalidate();
                    break;
                case "сбоку":
                    matrix = matrix_2;
                    this.Invalidate();
                    break;
                case "диметрическая":
                    matrix = matrix_3;
                    this.Invalidate();
                    break;
                case "изометрическая":
                    matrix = matrix_4;
                    this.Invalidate();
                    break;
                default:
                    break;
            }
        }
    }
}
