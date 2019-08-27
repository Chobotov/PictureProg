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
        public void FloodFill(int step, Bitmap bitmap, int x, int y, Color color)
        {
            LinkedList<Point> check = new LinkedList<Point>();
            Color floodFrom = bitmap.GetPixel(x, y); // Основной цвет,который ищется
            int percent = 2; // Один процент от 255
            if (floodFrom != color)
            {
                check.AddLast(new Point(x, y));
                while (check.Count>0)//Поставил ограничение в 5000 значений на клик,потому что при while(check.Count > 0) часто возникают утечки памяти
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
                            bool Amax = bitmap.GetPixel(next.X, next.Y).A >= floodFrom.A - percent * step;
                            bool Rmax = bitmap.GetPixel(next.X, next.Y).R >= floodFrom.R - percent * step;
                            bool Gmax = bitmap.GetPixel(next.X, next.Y).G >= floodFrom.G - percent * step;
                            bool Bmax = bitmap.GetPixel(next.X, next.Y).B >= floodFrom.B - percent * step;
                            bool Amin = bitmap.GetPixel(next.X, next.Y).A <= floodFrom.A + percent * step;
                            bool Rmin = bitmap.GetPixel(next.X, next.Y).R <= floodFrom.R + percent * step;
                            bool Gmin = bitmap.GetPixel(next.X, next.Y).G <= floodFrom.G + percent * step;
                            bool Bmin = bitmap.GetPixel(next.X, next.Y).B <= floodFrom.B + percent * step;
                            if ( bitmap.GetPixel(next.X, next.Y) == floodFrom ||                           
                            Amax &&
                            Amin &&
                            Rmax &&
                            Rmin &&
                            Gmax &&
                            Gmin &&
                            Bmax &&
                            Bmin)
                            {
                                check.AddLast(next);
                                bitmap.SetPixel(next.X, next.Y, color);
                                Debug.WriteLine((bitmap.GetPixel(next.X, next.Y) == floodFrom).ToString,
                            Amax.ToString,
                            Amin.ToString,
                            Rmax.ToString,
                            Rmin.ToString,
                            Gmax.ToString,
                            Gmin.ToString,
                            Bmax &&
                            Bmin);
                            }
                        }
                    }
                }
            }
        }
    }
}
