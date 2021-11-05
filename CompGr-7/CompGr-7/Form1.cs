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
            checkBox1.Checked = true;
            radioButton3.Checked = true;
            radioButton5.Checked = true;
            radioButton8.Checked = true;
            colorDialog1.Color = Color.Red;
            colorDialog2.Color = Color.DarkBlue;
        }
        double angle = 0.0;
        string RB_text;
        #region points
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
        #endregion
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = openGLControl1.OpenGL;
            gl.LoadIdentity();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            Lighting(gl);

            gl.Translate(0.0, 0.0, -6.0);
            Rotate_(gl);
            if (radioButton7.Checked == true)
            {
                checkBox1.Enabled = false;
                button2.Enabled = false;
                gl.Disable(OpenGL.GL_LIGHT0);
                gl.Disable(OpenGL.GL_LIGHTING);
                if (radioButton6.Checked == true)
                {
                    DrawOctahedron_Texture(gl);
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
            else if (radioButton8.Checked == true)
            {
                checkBox1.Enabled = true;
                button2.Enabled = true;
                if (checkBox1.Checked == true)
                {
                    gl.Enable(OpenGL.GL_LIGHTING);
                    gl.Enable(OpenGL.GL_LIGHT0);
                }
                else
                {
                    gl.Disable(OpenGL.GL_LIGHT0);
                    gl.Disable(OpenGL.GL_LIGHTING);
                }              
                if (radioButton5.Checked == true)
                {
                    DrawTeapot(gl, colorDialog1.Color, OpenGL.GL_FILL);
                }
                else if (radioButton4.Checked == true)
                {
                    DrawTeapot(gl, colorDialog1.Color, OpenGL.GL_LINE);
                }
                else if (radioButton6.Checked == true)
                {
                    DrawTeapot_Texture(gl);
                }
                else radioButton5.Checked = true;
            }
        }
        private void Lighting(OpenGL gl)
        {
            float[] mat_specular = { (float)(colorDialog2.Color.R / 255.0), (float)(colorDialog2.Color.G / 255.0), (float)(colorDialog2.Color.B / 255.0), 1.0f };
            float[] mat_shininess = { 50.0f };
            float[] light_position = { 1.0f, 1.0f, 1.0f, 0.0f };
            float[] white_light = { (float)(colorDialog2.Color.R / 255.0), (float)(colorDialog2.Color.G / 255.0), (float)(colorDialog2.Color.B / 255.0), 1.0f };
            float[] Light_Model_Ambient = { (float)(colorDialog1.Color.R / 255.0), (float)(colorDialog1.Color.G / 255.0), (float)(colorDialog1.Color.B / 255.0), 1.0f };

            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ShadeModel(OpenGL.GL_SMOOTH);

            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, mat_specular);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, mat_shininess);

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light_position);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, white_light);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, white_light);
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, Light_Model_Ambient);
        }
        private void DrawOctahedron_Texture(OpenGL gl)
        {
            Texture texture = new Texture();
            texture.Create(gl, CompGr_7.Properties.Resources._1);

            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE,(float)OpenGL.GL_REPLACE);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
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
        }
        private void DrawOctahedron_Color(OpenGL gl, Color color)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < points.GetUpperBound(0); i += 3)
            {
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(points[i, 0], points[i, 1], points[i, 2]);
                gl.Color(0, 0, 0);
                gl.Vertex(points[i + 1, 0], points[i + 1, 1], points[i + 1, 2]);
                gl.Color(color.R, color.G, color.B);
                gl.Vertex(points[i + 2, 0], points[i + 2, 1], points[i + 2, 2]);
            }
            gl.End();
            gl.Flush();
        }
        private void DrawOctahedron_Frame(OpenGL gl, Color color)
        {
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
        }
        private void DrawTeapot(OpenGL gl, Color color, uint type)
        {
            gl.Color(color.R, color.G, color.B);
            SharpGL.SceneGraph.Primitives.Teapot f = new SharpGL.SceneGraph.Primitives.Teapot();
            f.Draw(gl, 4, 1, type);
        }
        private void DrawTeapot_Texture(OpenGL gl)
        {
            Texture texture = new Texture();
            texture.Create(gl, CompGr_7.Properties.Resources._1);

            gl.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, (float)OpenGL.GL_REPLACE);
            gl.Enable(OpenGL.GL_TEXTURE_2D);

            SharpGL.SceneGraph.Primitives.Teapot f = new SharpGL.SceneGraph.Primitives.Teapot();
            f.Draw(gl, 4, 1, OpenGL.GL_FILL);

            gl.Disable(OpenGL.GL_TEXTURE_2D);
        }
        private void radioButton_EnabledChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                RB_text = (sender as RadioButton).Text;
            openGLControl1.Invalidate();
        }
        private void Rotate_(OpenGL gl)
        {
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
                case "XY":
                    gl.Rotate(angle, 1.0, 1.0, 0.0);
                    break;
                case "XZ":
                    gl.Rotate(angle, 1.0, 0.0, 1.0);
                    break;
                case "YZ":
                    gl.Rotate(angle, 0.0, 1.0, 1.0);
                    break;
                case "XYZ":
                    gl.Rotate(angle, 1.0, 1.0, 1.0);
                    break;
                case "стоп":
                    break;
                default:
                    break;
            }
            angle += 2.0;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
        }
    }
}
