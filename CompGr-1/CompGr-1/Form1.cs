using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGr_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new string[] { "2,1", "3", "4", "5,5", "6"});
            comboBox1.Text = "2,1";           
        }
        private double  x1, y1, x2, y2;
        private double  fi, t, k, r, R, width, hight;
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private PointF[] PointXY = new PointF[(int)10000];
        private Pen pen = new Pen(Color.AntiqueWhite, 2);
        private SolidBrush brush = new SolidBrush(Color.AntiqueWhite);
        private void button1_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(comboBox1.Text, out k) || k <= 0)
            {
                MessageBox.Show(
                "Только положительные числа, дробные числа вводить через запятую.",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            width = pictureBox1.Width - 20;
            hight = pictureBox1.Height - 20;
            R = width / 2;
            r = R / k;
            x1 = width / 2 + 10;
            y1 = hight / 2 + 10;

            for (int i = 0; i < 10000; i++)
            {
                t = i * Math.PI / 180;
                x2 = x1 - r * (k - 1) * (Math.Cos(t) + Math.Cos((k - 1) * t) / (k - 1));
                y2 = y1 + r * (k - 1) * (Math.Sin(t) - Math.Sin((k - 1) * t) / (k - 1));
                PointXY[i] = new PointF((float)x2, (float)y2);
            }
            timer1.Enabled = true;
        } 
        private void timer1_Tick_1(object sender, EventArgs e)
        { 
            fi += 10;
            t = fi * Math.PI / 180;

            x2 = x1 - r * (k - 1) * (Math.Cos(t) + Math.Cos((k - 1) * t) / (k - 1));
            y2 = y1 + r * (k - 1) * (Math.Sin(t) - Math.Sin((k - 1) * t) / (k - 1));

            pictureBox1.Invalidate();
        }       
        private void Form1_Load(object sender, EventArgs e)
        {
            k = 2.1;
            width = pictureBox1.Width - 20;
            hight = pictureBox1.Height - 20;
            R = width / 2;
            r = R / k;
            x1 = width / 2 + 10;
            y1 = hight / 2 + 10;

            fi += 1;
            t = fi * Math.PI / 180;

            x2 = x1 - r * (k - 1) * (Math.Cos(t) + Math.Cos((k - 1) * t) / (k - 1));
            y2 = y1 + r * (k - 1) * (Math.Sin(t) - Math.Sin((k - 1) * t) / (k - 1));
        }          
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLines(pen, PointXY);
            g.DrawEllipse(pen, 10, 10, (float)width, (float)hight);
            g.FillEllipse(brush, (float)x2 - 10, (float)y2 - 10, 20, 20);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
