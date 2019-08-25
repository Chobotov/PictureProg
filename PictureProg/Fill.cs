using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureProg
{
    class Fill
    {
        public void FloodFill(int step, Bmp bmp, Bitmap bitmap, int x, int y, Color color)
        {
            LinkedList<Point> check = new LinkedList<Point>();
            Color floodFrom = bitmap.GetPixel(x, y); // Основной цвет,который ищется
            int percent = 3; // Один процент от 255
            if (floodFrom != color)
            {
                check.AddLast(new Point(x, y));
                while (check.Count < 5000)//Поставил ограничение в 1000 значений на клик,потому что при while(check.Count > 0) часто возникают утечки памяти
                {
                    Point cur = check.First.Value;
                    check.RemoveFirst();
                    foreach (Point off in new Point[]
                    {
                        new Point(0, -1), new Point(0, 1),
                        new Point(-1, 0), new Point(1, 0)
                    })
                    {
                        Point next = new Point(cur.X + off.X, cur.Y + off.Y);
                        if (next.X >= 0 && next.Y >= 0 &&
                            next.X < bitmap.Width &&
                            next.Y < bitmap.Height)
                        {
                            bool A = bitmap.GetPixel(next.X, next.Y).A >= floodFrom.A - percent * step;
                            bool R = bitmap.GetPixel(next.X, next.Y).R >= floodFrom.R - percent * step;
                            bool G = bitmap.GetPixel(next.X, next.Y).G >= floodFrom.G - percent * step;
                            bool B = bitmap.GetPixel(next.X, next.Y).B >= floodFrom.B - percent * step;
                            bool a = bitmap.GetPixel(next.X, next.Y).A <= floodFrom.A + percent * step;
                            bool r = bitmap.GetPixel(next.X, next.Y).R <= floodFrom.R + percent * step;
                            bool g = bitmap.GetPixel(next.X, next.Y).G <= floodFrom.G + percent * step;
                            bool b = bitmap.GetPixel(next.X, next.Y).B <= floodFrom.B + percent * step;
                            if (percent * step >=0 && percent * step <=255 &&
                            bitmap.GetPixel(next.X, next.Y) == floodFrom ||
                            A &&
                            a &&
                            R &&
                            r &&
                            G &&
                            g &&
                            B &&
                            b)
                            {
                                check.AddLast(next);
                                bitmap.SetPixel(next.X, next.Y, color);
                            }
                        }
                    }
                }
            }
        }
    }
}
