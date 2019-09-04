using System;
using System.Diagnostics;
using System.Drawing;
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
            this.bmp.StepBackBmp = new Bitmap(bmp.EditBmp);
            SetImage();
            chart1.Titles.Add("Время работы(мс)");
            chart2.Titles.Add("Кол-во тактов");
        }
        //Запуск волнового алгоритма для выделения участков изображения при нажатии ЛКМ и алгоритма полного прохода ПКМ
        private void mouse_button(object sender, MouseEventArgs ms)
        {
            switch (ms.Button)
            {
                case MouseButtons.Left:
                    bmp.StepBackBmp = new Bitmap(bmp.EditBmp);
                    step = trackBar1.Value;
                    Fill fl = new Fill();
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    fl.FloodFill(step, bmp.EditBmp, ms.X, ms.Y, Color.Red);
                    st.Stop();
                    SetImage();
                    SetStatistic(label6, label2, label3, st, step);
                    chart1.Series["Волновой алгоритм"].Points.AddXY("1",st.ElapsedMilliseconds);
                    chart2.Series["Волновой алгоритм"].Points.AddXY("1",st.ElapsedTicks);
                    break;

                case MouseButtons.Right:
                    bmp.StepBackBmp = new Bitmap(bmp.EditBmp);
                    step = trackBar1.Value;
                    st = new Stopwatch();
                    st.Start();
                    Functions.AllImage(step, bmp.EditBmp, bmp.EditBmp.GetPixel(ms.X, ms.Y));
                    st.Stop();
                    SetImage();
                    SetStatistic(label7, label4, label5, st, step);
                    chart1.Series["Проход по всему изображению"].Points.AddXY("2", st.ElapsedMilliseconds);
                    chart2.Series["Проход по всему изображению"].Points.AddXY("2", st.ElapsedTicks);
                    break;
            }
        }
        //Текстовый ввод чувствительности
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
            bmp.EditBmp = new Bitmap(bmp.original_bmp);
            pictureBox1.Image = bmp.EditBmp;
            pictureBox1.Refresh();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            chart1.Series["Волновой алгоритм"].Points.Clear();
            chart2.Series["Волновой алгоритм"].Points.Clear();

            chart1.Series["Проход по всему изображению"].Points.Clear();
            chart2.Series["Проход по всему изображению"].Points.Clear();

            label6.Text = ($"Чувствительность : ");
            label2.Text = ($"Время : ");
            label3.Text = ($"Тик : ");

            label7.Text = ($"Чувствительность : ");
            label4.Text = ($"Время : ");
            label5.Text = ($"Тик : ");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bmp.EditBmp = new Bitmap(bmp.StepBackBmp);
            SetImage();
        }

        private void SetImage()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.Image = this.bmp.EditBmp;
            pictureBox1.Refresh();
        }

        private void SetStatistic(Label lstep,Label time, Label tik, Stopwatch st , int step)
        {
            lstep.Text = ($"Чувствительность : {step}");
            time.Text = ($"Время : {st.Elapsed}");
            tik.Text = ($"Тик : {st.ElapsedTicks}");
        }
    }
}
