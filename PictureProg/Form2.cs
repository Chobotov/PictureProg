using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureProg
{
    public partial class Form2 : Form
    {
        private Bmp bmp;
        private int step;

        public Form2(Bmp bmp)
        {
            InitializeComponent();
            this.bmp = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = this.bmp.bmp;
            pictureBox1.Refresh();
        }
        //Запуск волнового алгоритма для выделения участков изображения при нажатии ЛКМ
        private void mouse_button(object sender, MouseEventArgs ms)
        {
            try
            {
                if (ms.Button == MouseButtons.Left)
                {
                    step = trackBar1.Value;
                    Color color = bmp.bmp.GetPixel(ms.X, ms.Y);
                    Fill fl = new Fill();
                    fl.FloodFill(step, bmp.bmp, ms.X, ms.Y, Color.Red);
                    pictureBox1.Refresh();
                }
                if(ms.Button == MouseButtons.Right)
                {
                    step = trackBar1.Value;
                    Functions.AllImage(step,bmp.bmp, bmp.bmp.GetPixel(ms.X, ms.Y));
                    pictureBox1.Refresh();
                }
            }
            catch (Exception e) { Debug.WriteLine(e); };
        }
        //Текстовый вврд чувствительности
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                step = trackBar1.Value;
            }
            catch (Exception ex) { }
        }

        //Ползунок чувтсвительности
        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                trackBar1.Value = int.Parse(textBox1.Text);
            }
            catch (Exception ex) { }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseClick += mouse_button;
        }

        //Очищает изображение от выделенных участков
        private void Button1_Click(object sender, EventArgs e)
        {
            bmp.bmp = new Bitmap(bmp.original_bmp);
            pictureBox1.Image = bmp.bmp;
            pictureBox1.Refresh();
        }
    }
}
