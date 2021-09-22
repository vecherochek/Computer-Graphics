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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            Pen skyBluePen = new Pen(Brushes.Black);
            int cx = ClientSize.Width / 2;
            int cy = ClientSize.Height / 2;
            gfx.DrawLine(skyBluePen, cx, 0, cx, ClientSize.Height);
            gfx.DrawLine(skyBluePen, 0, cy, ClientSize.Width, cy);

            Pen SqPen = new Pen(Brushes.BlueViolet);


        }
    }
}
