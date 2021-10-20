using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace CompGr_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton3.Checked = true;
        }
        double angle = 0.0f;
        string RB_text;
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = this.openGLControl1.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.Translate(0.0, 0.0, -6.0);
            switch (RB_text)
            {
                case "Поворот по OX":
                    gl.Rotate(angle, 1.0, 0.0, 0.0);
                    break;
                case "Поворот по OY":
                    gl.Rotate(angle, 0.0, 1.0, 0.0);
                    break;
                case "Поворот по OZ":
                    gl.Rotate(angle, 0.0, 0.0, 1.0);
                    break;
                default:
                    break;
            }

            gl.Begin(OpenGL.GL_TRIANGLES);

            gl.Color(1.0, 0.0, 0.0);
            gl.Vertex(0.0, 2.0, 0.0);
            gl.Vertex(0.0, 0.0, 1.0);
            gl.Vertex(1.5, 0.0, 0.0);

            gl.Color(1.0, 0.0, 1.0);
            gl.Vertex(0.0, 2.0, 0.0);
            gl.Vertex(1.5, 0.0, 0.0);
            gl.Vertex(0.0, 0.0, -1.0);

            gl.Color(1.0, 1.0, 0.0);
            gl.Vertex(0.0, 2.0, 0.0);
            gl.Vertex(0.0, 0.0, -1.0);
            gl.Vertex(-1.5, 0.0, 0.0);

            gl.Color(1.0, 1.0, 1.0);
            gl.Vertex(0.0, 2.0, 0.0);
            gl.Vertex(-1.5, 0.0, 0.0);
            gl.Vertex(0.0, 0.0, 1.0);

            gl.Color(0.3, 0.5, 0.0);
            gl.Vertex(0.0, -2.0, 0.0);
            gl.Vertex(1.5, 0.0, 0.0);
            gl.Vertex(0.0, 0.0, 1.0);

            gl.Color(0.0, 0.0, 1.0);
            gl.Vertex(0.0, -2.0, 0.0);
            gl.Vertex(0.0, 0.0, -1.0);
            gl.Vertex(1.5, 0.0, 0.0);

            gl.Color(0.0, 1.0, 0.0);
            gl.Vertex(0.0, -2.0, 0.0);
            gl.Vertex(-1.5, 0.0, 0.0);
            gl.Vertex(0.0, 0.0, -1.0);

            gl.Color(0.0, 1.0, 1.0);
            gl.Vertex(0.0, -2.0, 0.0);
            gl.Vertex(0.0, 0.0, 1.0);
            gl.Vertex(-1.5, 0.0, 0.0);

            gl.End();
            gl.Flush();

            angle += 2.0;

        }
        private void radioButton_EnabledChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                RB_text = (sender as RadioButton).Text;
        }
    }
}
