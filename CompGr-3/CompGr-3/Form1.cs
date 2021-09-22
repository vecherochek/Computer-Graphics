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
            Graphics g = e.Graphics;
            Pen skyBluePen = new Pen(Color.Black);

            int x = ClientSize.Width / 2;
            int y = ClientSize.Height / 2;

            g.DrawLine(skyBluePen, x, 0, x, ClientSize.Height);
            g.DrawLine(skyBluePen, 0, y, ClientSize.Width, y);
        }
    }
}
