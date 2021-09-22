using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGr_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "3", "5" });
            comboBox1.Text = "3";
            button2.Enabled = false;
            button3.Enabled = false;
        }
        private Bitmap bmp;
        private Bitmap tmp;
        string RB_text;

        private void Form1_Load(object sender, EventArgs e)
        {

        }       
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG,*.PNG, *.ICO)|*.bmp; *.jpg; *.png; *.ico";

            Image image;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(dialog.FileName);            
                bmp = new Bitmap(image, Convert.ToInt32(image.Width), Convert.ToInt32(image.Height));
                tmp = bmp;
                //----tmp будет уменьшаться в размере или увеличиваться для пикчербокса
                //----bmp будет иметь изначальный размер, все действия будут производиться с оригиналом
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = tmp;
            }
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как ...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
            "BMP-file(*.bmp)|*.bmp|" +
            "GIF-file(*.gif)|*.gif|" +
            "JPEG-file(*.jpg)|*.jpg|" +
            "PNG-file(*.png)|*.png";
            savedialog.ShowHelp = true;

            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = savedialog.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                switch (strFilExtn)
                {
                    case "bmp":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "png":
                        bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            double[,] kernal_blur_3 = {{1.0/9.0, 1.0/9.0, 1.0/9.0},
                                       {1.0/9.0, 1.0/9.0, 1.0/9.0},
                                       {1.0/9.0, 1.0/9.0, 1.0/9.0}};
            double[,] kernal_blur_5 = {{1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                                       {1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                                       {1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                                       {1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                                       {1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},};
            double[,] kernal_Gauss_3 = {{1.0/16.0, 2.0/16.0, 1.0/16.0},
                                        {2.0/16.0, 4.0/16.0, 2.0/16.0},
                                        {1.0/16.0, 2.0/16.0, 1.0/16.0}};
            double[,] kernal_Gauss_5 = {{1.0/273.0, 4.0/273.0, 7.0/273.0, 4.0/273.0, 2.0/273.0},
                                    {4.0/273.0, 16.0/273.0, 26.0/273.0, 16.0/273.0, 4.0/273.0},
                                    {7.0/273.0, 26.0/273.0, 41.0/273.0, 26.0/273.0, 7.0/273.0},
                                    {4.0/273.0, 16.0/273.0, 26.0/273.0, 16.0/273.0, 4.0/273.0},
                                    {1.0/273.0, 4.0/273.0, 7.0/273.0, 4.0/273.0, 2.0/273.0}};
            double[,] kernal_bound = {{0, -1, 0},
                                  {-1, 4, -1},
                                  {0, -1, 0}};
            double[,] kernal_sharp = {{0, -1, 0},
                                  {-1, 5, -1},
                                  {0, -1, 0}};
            switch (RB_text)
            {
                case "Размытие":
                    if (comboBox1.Text == "3") Image_Editor(kernal_blur_3);
                    else if (comboBox1.Text == "5") Image_Editor(kernal_blur_5);
                    break;
                case "Размытие по Гауссу":
                    if (comboBox1.Text == "3") Image_Editor(kernal_Gauss_3);
                    else if (comboBox1.Text == "5") Image_Editor(kernal_Gauss_5);
                    break;
                case "Выделение границ":
                    Image_Editor(kernal_bound);
                    break;
                case "Повышение резкости":
                    Image_Editor(kernal_sharp);
                    break;
                default:
                    break;
            }
        }
        private void Image_Editor(double[,] kernal)
        {
            int kernal_size = Convert.ToInt32(Math.Sqrt(kernal.Length));
            int AddPix = kernal_size / 2;

            int tmpH = bmp.Height + 2 * AddPix;
            int tmpW = bmp.Width + 2 * AddPix;
            Color[,] tmpIm = new Color[tmpW, tmpH];
            //заполнение расширенной картинки
            //------выбрала метод расширенной картинки, тк по тз нужно использовать ядра разных размеров
            //------таким образом картинка не будет уменьшаться
            //углы
            for (int i = 0; i < AddPix; i++)
                for (int j = 0; j < AddPix; j++)
                {
                    tmpIm[i, j] = bmp.GetPixel(0, 0);
                    tmpIm[i, tmpH - 1 - j] = bmp.GetPixel(0, bmp.Height - 1);
                    tmpIm[tmpW - 1 - i, j] = bmp.GetPixel(bmp.Width - 1, 0);
                    tmpIm[tmpW - 1 - i, tmpH - 1 - j] = bmp.GetPixel(bmp.Width - 1, bmp.Height - 1);
                }
            //право/лево
            for (int i = 0; i < AddPix; i++)
                for (int j = AddPix; j < tmpH - AddPix; j++)
                {
                    tmpIm[i, j] = bmp.GetPixel(0, j - AddPix);
                    tmpIm[tmpW - 1 - i, j] = bmp.GetPixel(bmp.Width - 1, j - AddPix);
                }
            //верх/низ
            for (int i = AddPix; i < tmpW - AddPix; i++)
                for (int j = 0; j < AddPix; j++)
                {
                    tmpIm[i, j] = bmp.GetPixel(i - AddPix, 0);
                    tmpIm[i, tmpH - 1 - j] = bmp.GetPixel(i - AddPix, bmp.Height - 1);
                }
            //центр
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    tmpIm[i + AddPix, j + AddPix] = bmp.GetPixel(i, j);
                }
            for (int i = AddPix; i < tmpW - AddPix; i++)
                for (int j = AddPix; j < tmpH - AddPix; j++)
                {
                    int r = 0, g = 0, b = 0;

                    for (int k = 0; k < kernal_size; k++)
                        for (int m = 0; m < kernal_size; m++)
                        {
                            Color col = tmpIm[i - AddPix + k, j - AddPix + m];
                            r += Convert.ToInt32(kernal[k, m] * col.R);
                            g += Convert.ToInt32(kernal[k, m] * col.G);
                            b += Convert.ToInt32(kernal[k, m] * col.B);
                        }
                    if (r > 255) r = 255;
                    if (r < 0) r = 0;

                    if (g > 255) g = 255;
                    if (g < 0) g = 0;

                    if (b > 255) b = 255;
                    if (b < 0) b = 0;
                    Color rgb = Color.FromArgb(r, g, b);
                    bmp.SetPixel(i - AddPix, j - AddPix, rgb);
                }
            tmp = bmp;
            Refresh();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                RB_text = (sender as RadioButton).Text;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
