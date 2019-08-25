using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PictureProg
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
            button5.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            parts = false;
        }
        private Form2 img_frst, img_scnd;
        private Bmp bmp_first;
        private Bmp bmp_second;
        private Bmp bmp_final;
        private double alpha;
        private double beta;
        private bool parts;
        private string filepathfirst,filepathsecond;

        #region Пустые слушатели

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mg1.Enabled = false;
            mg2.Enabled = false;
        }
        #endregion

        private void EnableButtons()
        {
            if (Functions.SizeImage(bmp_first.bmp,bmp_second.bmp))
            {
                textBox3.Text = "Размеры изображений одинаковы";
                button3.Enabled = true;
                button4.Enabled = true;
                button6.Enabled = true;
            }
            else
            {
                textBox3.Text = "Размеры изображений различны";
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
            }
                
        }

        /// <summary>
        ///Загрузка первого изображения и вывод данных
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            bmp_first = new Bmp();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            openFileDialog1.InitialDirectory = "C:\\";

            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                AlphaBeta();
                filepathfirst = openFileDialog1.FileName;
                bmp_first.bmp = new Bitmap(filepathfirst);
                bmp_first.original_bmp = new Bitmap(filepathfirst);
                Functions.ShowImage(filepathfirst, mg1, textBox1);
                Functions.DataImage(textBox4, bmp_first.bmp, alpha, beta);
                try
                {
                    EnableButtons();
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        ///Загрузка второго изображения и вывод данных
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            bmp_second = new Bmp();
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            openFileDialog2.InitialDirectory = "C:\\";

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                AlphaBeta(); 
                filepathsecond = openFileDialog2.FileName;
                bmp_second.bmp = new Bitmap(filepathsecond);
                bmp_second.original_bmp = new Bitmap(filepathsecond);
                Functions.ShowImage(filepathsecond, mg2, textBox2);
                Functions.DataImage(textBox5, bmp_second.bmp, alpha, beta);
                try
                {
                    EnableButtons();
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        ///Отнять изображение 2 из изображения 1
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bmp_final = new Bmp();
                AlphaBeta();
                bmp_final.bmp = new Bitmap(bmp_first.bmp.Width, bmp_first.bmp.Height);
                if (parts==false)
                    Functions.MinusImage(pictureBox3, bmp_first.bmp, bmp_second.bmp, bmp_final.bmp);
                else
                    Functions.MinusParts(pictureBox3, Color.Red, bmp_first, bmp_second, bmp_final);
                Functions.DataImage(textbox6, bmp_final.bmp, alpha, beta);
                button5.Enabled = true;
            }
            catch (Exception ex) { }
        }

        /// <summary>
        ///Отнять изображение 1 из изображения 2
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                AlphaBeta();
                bmp_final = new Bmp();
                bmp_final.bmp = new Bitmap(bmp_first.bmp.Width, bmp_first.bmp.Height);
                if (parts==false)
                    Functions.MinusImage(pictureBox3, bmp_second.bmp, bmp_first.bmp, bmp_final.bmp);
                else
                    Functions.MinusParts(pictureBox3,Color.Red,bmp_second,bmp_first,bmp_final);
                Functions.DataImage(textbox6, bmp_final.bmp, alpha, beta);
                button5.Enabled = true;
            }
            catch (Exception ex) { }
        }

        /// <summary>
        ///Кнопка сохранения конечного изображения
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "jpeg image|*.jpg";
            save.Title = "Сохранить изображение";
            if (save.ShowDialog() == DialogResult.OK)
            {
                bmp_final.bmp.Save(save.FileName);
            } 
        }

        /// <summary>
        /// Обновить
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            AlphaBeta();
            Functions.DataImage(textBox4, bmp_first.bmp,alpha, beta);
            Functions.DataImage(textBox5, bmp_second.bmp, alpha, beta);
            if (bmp_final != null) Functions.DataImage(textbox6, bmp_final.bmp, alpha, beta);
        }

        private void mg1_Click(object sender, EventArgs e)
        {
            img_frst = new Form2(bmp_first);
            img_frst.Show();
        }


        private void mg2_Click(object sender, EventArgs e)
        {
            img_scnd = new Form2(bmp_second);
            img_scnd.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked && bmp_first != null && bmp_second != null)
            {
                parts = true;
                mg1.Enabled = true;
                mg2.Enabled = true;
            }
            else
            {
                parts = false;
                mg1.Enabled = false;
                mg2.Enabled = false;
            }
        }

        private void AlphaBeta()
        {
            try
            {
                alpha = Double.Parse(textBox7.Text);
                beta = Double.Parse(textBox8.Text);
            }
            catch (Exception ex) { }
        }
    }
}
