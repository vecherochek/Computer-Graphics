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
    public partial class Form2 : Form
    {
        public Form2()
        {
            Form1 main = this.Owner as Form1;
            InitializeComponent();
        }
        double[,] points2_2D;
        double scale2 = Form1.scale / 1.25; 
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.DarkSeaGreen);
            Point[] points0_2D = new Point[Form1.points_3D.GetUpperBound(0) + 1];

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;

            double l = 0.5;
            double B = Math.Atan(2);
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {l * Math.Cos(B), l * Math.Sin(B), 0, 0},
                              {0, 0, 0, 1}};
            points2_2D = Form1.MatrixMult(Form1.points_3D, matrix);
            points2_2D = Form1.MatrixNorm(points2_2D);
            for (int i = 0; i < points2_2D.GetUpperBound(0) + 1; i++)
            {
                points0_2D[i] = new Point(Convert.ToInt32(points2_2D[i, 0] * scale2 + x), Convert.ToInt32(y - points2_2D[i, 1] * scale2));
            }

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLines(points0_2D);

            g.DrawPath(pen, path);
            g.DrawLine(pen, x, 0, x, ClientSize.Height);
            g.DrawLine(pen, 0, y, ClientSize.Width, y);
        }
    }
}
