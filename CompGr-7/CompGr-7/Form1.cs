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
using SharpGL.Enumerations;

namespace CompGr_7
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            radioButton3.Checked = true;
            radioButton6.Checked = true;
            colorDialog1.Color = Color.Orange;
        }
        double angle = 0.0;
        string RB_text;

        double[,] points = { {0.0, 2.0, 0.0  },
                            {0.0, 0.0, 1.0  },
                            {1.5, 0.0, 0.0  },

                            {0.0, 2.0, 0.0  },
                            {1.5, 0.0, 0.0  },
                            {0.0, 0.0, -1.0 },

                            {0.0, 2.0, 0.0  },
                            {0.0, 0.0, -1.0 },
                            {-1.5, 0.0, 0.0 },

                            {0.0, 2.0, 0.0  },
                            {-1.5, 0.0, 0.0 },
                            {0.0, 0.0, 1.0  },

                            {0.0, -2.0, 0.0 },
                            {1.5, 0.0, 0.0  },
                            {0.0, 0.0, 1.0  },

                            {0.0, -2.0, 0.0 },
                            {0.0, 0.0, -1.0 },
                            {1.5, 0.0, 0.0  },

                            {0.0, -2.0, 0.0 },
                            {-1.5, 0.0, 0.0 },
                            {0.0, 0.0, -1.0 },

                            {0.0, -2.0, 0.0 },
                            {0.0, 0.0, 1.0  }, 
                            {-1.5, 0.0, 0.0}};
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = this.openGLControl1.OpenGL;
            
            if (radioButton6.Checked == true)
            {
                Texture texture = new Texture();
                texture.Create(gl, CompGr_7.Properties.Resources._1);

                DrawOctahedron_Texture(gl, texture);
            }
            else if (radioButton5.Checked == true)
            {
                DrawOctahedron_Color(gl, colorDialog1.Color);
            }
            else if (radioButton4.Checked == true)
            {
                DrawOctahedron_Frame(gl, colorDialog1.Color);
            }
        }
        private void radioButton_EnabledChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                RB_text = (sender as RadioButton).Text;
        }

        private void DrawOctahedron_Texture(OpenGL gl, Texture texture)
        {           
            gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.Translate(0.0, 0.0, -6.0);
            switch (RB_text)
            {
                case "OX":
                    gl.Rotate(angle, 1.0, 0.0, 0.0);
                    break;
                case "OY":
                    gl.Rotate(angle, 0.0, 1.0, 0.0);
                    break;
                case "OZ":
                    gl.Rotate(angle, 0.0, 0.0, 1.0);
                    break;
                default:
                    break;
            }
            texture.Bind(gl);
            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < points.GetUpperBound(0); i += 3)
            {
                gl.TexCoord(0, 0); gl.Vertex(points[i,0], points[i, 1], points[i, 2]);
                gl.TexCoord(0, 1); gl.Vertex(points[i + 1, 0], points[i + 1, 1], points[i + 1, 2]);
                gl.TexCoord(1, 0); gl.Vertex(points[i + 2, 0], points[i + 2, 1], points[i + 2, 2]);
            }
            
            gl.End();
            gl.Flush();

            gl.Disable(OpenGL.GL_TEXTURE_2D);

            angle += 2.0;
        }
        private void DrawOctahedron_Color(OpenGL gl, Color color)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.Translate(0.0, 0.0, -6.0);
            switch (RB_text)
            {
                case "OX":
                    gl.Rotate(angle, 1.0, 0.0, 0.0);
                    break;
                case "OY":
                    gl.Rotate(angle, 0.0, 1.0, 0.0);
                    break;
                case "OZ":
                    gl.Rotate(angle, 0.0, 0.0, 1.0);
                    break;
                default:
                    break;
            }

            gl.Begin(OpenGL.GL_TRIANGLES);

            gl.Color(color.R, color.G, color.B);
            for (int i = 0; i < points.GetUpperBound(0); i += 3)
            {
                gl.Vertex(points[i, 0], points[i, 1], points[i, 2]);
                gl.Vertex(points[i + 1, 0], points[i + 1, 1], points[i + 1, 2]);
                gl.Vertex(points[i + 2, 0], points[i + 2, 1], points[i + 2, 2]);
            }            
            gl.End();
            gl.Flush();

            angle += 2.0;
        }
        private void DrawOctahedron_Frame(OpenGL gl, Color color)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.Translate(0.0, 0.0, -6.0);
            switch (RB_text)
            {
                case "OX":
                    gl.Rotate(angle, 1.0, 0.0, 0.0);
                    break;
                case "OY":
                    gl.Rotate(angle, 0.0, 1.0, 0.0);
                    break;
                case "OZ":
                    gl.Rotate(angle, 0.0, 0.0, 1.0);
                    break;
                default:
                    break;
            }

            gl.Begin(OpenGL.GL_LINE_LOOP);

            gl.Color(color.R, color.G, color.B);
            for (int i = 0; i < points.GetUpperBound(0); i += 3)
            {
                gl.Vertex(points[i, 0], points[i, 1], points[i, 2]);
                gl.Vertex(points[i + 1, 0], points[i + 1, 1], points[i + 1, 2]);
                gl.Vertex(points[i + 2, 0], points[i + 2, 1], points[i + 2, 2]);
            }
            gl.End();
            gl.Flush();

            angle += 2.0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
    }
}
