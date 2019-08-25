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
        private double alpha, beta;
        public List<int> xlist;
        public List<int> ylist;

        public Form2(Bmp bmp,double alpha,double beta)
        {
            InitializeComponent();
            this.bmp = bmp;
            this.alpha = alpha;
            this.beta = beta;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = (Image)this.bmp.bmp;
            pictureBox1.Refresh();
        }
        private void mouse_button(object sender, MouseEventArgs ms)
        {
            try
            {
                if (ms.Button == MouseButtons.Left)
                {
                    step = trackBar1.Value;
                    Color color = bmp.bmp.GetPixel(ms.X, ms.Y);
                    Color clr = Color.FromArgb(
                        int.Parse(textBox2.Text),//Alpha
                        int.Parse(textBox3.Text),//Red
                        int.Parse(textBox4.Text),//Blue
                        int.Parse(textBox5.Text));//Green
                    Debug.WriteLine($"Альфа:{int.Parse(textBox2.Text)}\n " +
                        $"Red:{int.Parse(textBox3.Text)}\n" +
                        $"Blue{int.Parse(textBox4.Text)}\n" +
                        $"Green{int.Parse(textBox5.Text)}");
                    Fill fl = new Fill();
                    Debug.WriteLine($"{BackColor.A}, {BackColor.R} , {BackColor.G}, {BackColor.B}");
                    fl.FloodFill(step,bmp, bmp.bmp, ms.X, ms.Y, Color.Red);
                    pictureBox1.Refresh();
                }
            }
            catch (Exception e) { Debug.WriteLine(e); };
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                step = trackBar1.Value;
            }
            catch (Exception ex) { }
        }

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
    }
}
