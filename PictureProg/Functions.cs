﻿using System;
using System.Drawing;
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

            bt.BackgroundImage = img;
            bt.BackgroundImageLayout = ImageLayout.Stretch;

            text.Text = ($"{ bmp_width } x { bmp_height }");
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
            for (int y = 0; y < bmp_first.EditBmp.Height; y++)
            {
                for (int x = 0; x < bmp_first.EditBmp.Width; x++)
                {
                    int pixelMainR = Convert.ToInt32(bmp_first.EditBmp.GetPixel(x, y).R);
                    int pixelLastR = Convert.ToInt32(bmp_second.EditBmp.GetPixel(x, y).R);

                    int pixelMainG = Convert.ToInt32(bmp_first.EditBmp.GetPixel(x, y).G);
                    int pixelLastG = Convert.ToInt32(bmp_second.EditBmp.GetPixel(x, y).G);

                    int pixelMainB = Convert.ToInt32(bmp_first.EditBmp.GetPixel(x, y).B);
                    int pixelLastB = Convert.ToInt32(bmp_second.EditBmp.GetPixel(x, y).B);
                    if (bmp_first.EditBmp.GetPixel(x, y).R == clr.R &&
                        bmp_first.EditBmp.GetPixel(x, y).G == clr.G &&
                        bmp_first.EditBmp.GetPixel(x, y).B == clr.B &&
                        (bmp_second.EditBmp.GetPixel(x, y).R == clr.R &&
                        bmp_second.EditBmp.GetPixel(x, y).G == clr.G &&
                        bmp_second.EditBmp.GetPixel(x, y).B == clr.B))
                    {
                        
                    }
                    else
                        bmp_final.EditBmp.SetPixel(x, y, Color.FromArgb(Math.Max((pixelMainR - pixelLastR), 0), Math.Max((pixelMainG - pixelLastG), 0), Math.Max((pixelMainB - pixelLastB), 0)));
                }
            }
            bmp_final.EditBmp.MakeTransparent(Color.Red);
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.Image = (Image)bmp_final.EditBmp;
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
            int RedPixel = 0;
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
                    if (pixelR == Color.Red.R && pixelG == Color.Red.G && pixelB == Color.Red.B)
                        RedPixel += 1;
                }
            }
            text.Text = "Материал = "
                + blackpixel + ", "
                + (100 * (double)blackpixel / ((bmp.Height * bmp.Width) - RedPixel)).ToString("0.000")
                + "% "
                + "\n"
                + "Переходный материал = "
                + graypixel
                + ", " + (100 * (double)graypixel / ((bmp.Height * bmp.Width) - RedPixel)).ToString("0.000")
            + "% " + "\n" + "Пустота = "
            + whitepixel + ", "
            + (100 * (double)whitepixel / ((bmp.Height * bmp.Width) - RedPixel)).ToString("0.000")
            + "% ";
        }

        public static void AllImage(int step,Bitmap bmb, Color clr)
        {
            for (int i = 0; i < bmb.Height; i++)
            {
                for(int j = 0; j < bmb.Width; j++)
                {
                    Color clr1 = bmb.GetPixel(j, i);
                    if (bmb.GetPixel(j,i) == clr || areColorsSimmilar(step,clr1,clr))   
                    {
                        bmb.SetPixel(j,i,Color.Red);
                    }
                }
            }
        }

        private static bool areColorsSimmilar(int step, Color bmb, Color clr)
        {
            int differenceR = Math.Abs(bmb.R - clr.R);
            int differenceG = Math.Abs(bmb.G - clr.G);
            int differenceB = Math.Abs(bmb.B - clr.B);
            int maxDifference = Math.Max(Math.Max(differenceR, differenceG), differenceB);
            return maxDifference < step ? true : false;
        }

    }
}
