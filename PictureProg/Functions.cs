using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureProg
{
    class Functions
    {
        /// <summary>
        /// Вывод изображения в нужную кнопку
        /// </summary>
        public static void ShowImage(string filepath,Button bt, TextBox text)
        {
            Image img = Image.FromFile(filepath);
            int bmp_height = img.Height;
            int bmp_width = img.Width;

            Bitmap mimg = new Bitmap(img, bt.Width, bt.Height);

            bt.BackgroundImage = img;
            bt.BackgroundImageLayout = ImageLayout.Stretch;

            text.Text = ($"{ bmp_height } x { bmp_width }");
        }
        
        ///<summary>
        ///Сравнивание изображений по размерам и вывод итогов в нужный текстбокс
        ///</summary>
        public static bool SizeImage(Bitmap bmp_first,Bitmap bmp_second)
        {
            if (bmp_first.Height == bmp_second.Height && bmp_first.Width == bmp_second.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Вычитание одного изображения из другого,
        ///filepathMain-путь изображения из которого вычитать
        /// </summary> 
        public static void MinusImage(PictureBox box, Bitmap bmp_first, Bitmap bmp_second,Bitmap bmp_final)
        {
            //Заполнение изображения пикселями 
            for (int y = 0; y < bmp_first.Height; y++)
            {
                for (int x = 0; x < bmp_first.Width; x++)
                {
                    int pixelMainR = Convert.ToInt32(bmp_first.GetPixel(x, y).R);
                    int pixelLastR = Convert.ToInt32(bmp_second.GetPixel(x, y).R);

                    int pixelMainG = Convert.ToInt32(bmp_first.GetPixel(x, y).G);
                    int pixelLastG = Convert.ToInt32(bmp_second.GetPixel(x, y).G);

                    int pixelMainB = Convert.ToInt32(bmp_first.GetPixel(x, y).B);
                    int pixelLastB = Convert.ToInt32(bmp_second.GetPixel(x, y).B);

                    bmp_final.SetPixel(x, y, Color.FromArgb(Math.Max((pixelMainR - pixelLastR), 0), Math.Max((pixelMainG - pixelLastG), 0), Math.Max((pixelMainB - pixelLastB), 0)));
                }
            }
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.Image = (Image)bmp_final;
        }

        public static void MinusParts(PictureBox box, Color clr, Bmp bmp_first, Bmp bmp_second,Bmp bmp_final)
        {
            //Заполнение изображения пикселями 
            for (int y = 0; y < bmp_first.bmp.Height; y++)
            {
                for (int x = 0; x < bmp_first.bmp.Width; x++)
                {
                    int pixelMainR = Convert.ToInt32(bmp_first.bmp.GetPixel(x, y).R);
                    int pixelLastR = Convert.ToInt32(bmp_second.bmp.GetPixel(x, y).R);

                    int pixelMainG = Convert.ToInt32(bmp_first.bmp.GetPixel(x, y).G);
                    int pixelLastG = Convert.ToInt32(bmp_second.bmp.GetPixel(x, y).G);

                    int pixelMainB = Convert.ToInt32(bmp_first.bmp.GetPixel(x, y).B);
                    int pixelLastB = Convert.ToInt32(bmp_second.bmp.GetPixel(x, y).B);
                    if (bmp_first.bmp.GetPixel(x, y).R == clr.R &&
                        bmp_first.bmp.GetPixel(x, y).G == clr.G &&
                        bmp_first.bmp.GetPixel(x, y).B == clr.B &&
                        (bmp_second.bmp.GetPixel(x, y).R == clr.R &&
                        bmp_second.bmp.GetPixel(x, y).G == clr.G &&
                        bmp_second.bmp.GetPixel(x, y).B == clr.B))
                    {
                        //Пропускаем
                    }
                    else
                        bmp_final.bmp.SetPixel(x, y, Color.FromArgb(Math.Max((pixelMainR - pixelLastR), 0), Math.Max((pixelMainG - pixelLastG), 0), Math.Max((pixelMainB - pixelLastB), 0)));
                }
            }
            bmp_final.bmp.MakeTransparent(Color.Red);
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.Image = (Image)bmp_final.bmp;
        }

        ///<summary>
        ///Высчитывание данных из всего изображения
        ///text-нужный текстбокс для вывода текста,image-нужное Bitmap изображение 
        ///</summary>
        public static void DataImage(RichTextBox text, Bitmap bmp,double alpha,double beta)
        {
            int blackpixel = 0;
            int whitepixel = 0;
            int graypixel = 0;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    int pixelR = Convert.ToInt32(bmp.GetPixel(x, y).R);
                    int pixelG = Convert.ToInt32(bmp.GetPixel(x, y).G);
                    int pixelB = Convert.ToInt32(bmp.GetPixel(x, y).B);

                    if (pixelR >= 0 && pixelR <= 255 * (1 - beta) && 
                        pixelG >= 0 && pixelG <= 255 * (1 - beta) && 
                        pixelB >= 0 && pixelB <= 255 * (1 - beta)) blackpixel++;
                    if (pixelR > (1 - alpha) * 255 && pixelR <= 255 && 
                        pixelG > (1 - alpha) * 255 && pixelG <= 255 && 
                        pixelB > (1 - alpha) * 255 && pixelB <= 255) whitepixel++;
                    if (pixelR > 255 * (1 - beta) && pixelR < 255 * (1 - alpha) && 
                        pixelG > 255 * (1 - beta) && pixelG < 255 * (1 - alpha) && 
                        pixelB > 255 * (1 - beta) && pixelB < 255 * (1 - alpha)) graypixel++;
                }
            }
        text.Text = "Материал = "
            + blackpixel + ", "
            + (100 * (double) blackpixel / (bmp.Height * bmp.Width)).ToString("0.000")
            + "% "
            + "\n"
            + "Переходный материал = "
            + graypixel
            + ", " + (100 * (double) graypixel / (bmp.Height * bmp.Width)).ToString("0.000")
            + "% " + "\n" + "Пустота = "
            + whitepixel + ", "
            + (100 * (double) whitepixel / (bmp.Height * bmp.Width)).ToString("0.000")
            + "% ";
        }


    }
}
