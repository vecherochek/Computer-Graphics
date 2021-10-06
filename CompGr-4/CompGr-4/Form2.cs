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
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;

            //нарисуем оси
            Pen pen = new Pen(Color.Black);
            g.DrawLine(pen, x, 0, x, y * 2);
            g.DrawLine(pen, 0, y, x * 2, y);

            //нарисуем проекцию
            double l = 0.5;
            double B = Math.Atan(2);
            double[,] matrix = { {1, 0, 0, 0},
                              {0, 1, 0, 0},
                              {l * Math.Cos(B), l * Math.Sin(B), 0, 0},
                              {0, 0, 0, 1}};
            Form1.DrawWireframe(g, matrix, ClientSize.Width, ClientSize.Height);           
        }
    }
}
