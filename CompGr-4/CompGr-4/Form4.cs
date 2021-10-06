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
        double[,] points2_2D;
        string RB_text;
        bool flag = true;
        double[,] matrix ={ {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {0, 0, 0, 0},
                              {0, 0, 0, 1}};
        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);
            Point[] points0_2D = new Point[Form1.points_3D.GetUpperBound(0) + 1];

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;

            points2_2D = Form1.MatrixMult(Form1.points_3D, matrix);
            points2_2D = Form1.MatrixNorm(points2_2D);
            if (flag)
            {
                for (int i = 0; i < points2_2D.GetUpperBound(0) + 1; i++)
                {
                    points0_2D[i] = new Point(Convert.ToInt32(points2_2D[i, 0] * Form1.scale + x), Convert.ToInt32(y - points2_2D[i, 1] * Form1.scale));
                }
            }
            else
            {
                for (int i = 0; i < points2_2D.GetUpperBound(0) + 1; i++)
                {
                    points0_2D[i] = new Point(Convert.ToInt32(points2_2D[i, 2] * Form1.scale + x), Convert.ToInt32(y - points2_2D[i, 1] * Form1.scale));
                }
            }
            
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(points0_2D);

            g.DrawPath(pen, path);
            g.DrawLine(pen, x, 0, x, ClientSize.Height);
            g.DrawLine(pen, 0, y, ClientSize.Width, y);
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
                    flag = true;
                    this.Invalidate();
                    break;
                case "сбоку":
                    matrix = matrix_2;
                    flag = false;
                    this.Invalidate();
                    break;
                case "диметрическая":
                    matrix = matrix_3;
                    flag = true;
                    this.Invalidate();
                    break;
                case "изометрическая":
                    matrix = matrix_4;
                    flag = true;
                    this.Invalidate();
                    break;
                default:
                    break;
            }
        }
    }
}
