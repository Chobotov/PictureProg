﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PictureProg
{
    class Fill
    {
        public void FloodFill(int step, Bitmap bitmap, int x, int y, Color color)
        {
            BitmapData data = bitmap.LockBits(
         new Rectangle(0, 0, bitmap.Width, bitmap.Height),
         ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int[] bits = new int[data.Stride / 4 * data.Height];
            Marshal.Copy(data.Scan0, bits, 0, bits.Length);

            LinkedList<Point> check = new LinkedList<Point>();
            int floodTo = color.ToArgb();
            int floodFrom = bits[x + y * data.Stride / 4];
            bits[x + y * data.Stride / 4] = floodTo;

            if (floodFrom != floodTo)
            {
                check.AddLast(new Point(x, y));
                while (check.Count > 0)
                {
                    Point cur = check.First.Value;
                    check.RemoveFirst();

                    foreach (Point off in new Point[] {
                new Point(0, -1), new Point(0, 1),
                new Point(-1, 0), new Point(1, 0)})
                    {
                        Point next = new Point(cur.X + off.X, cur.Y + off.Y);
                        if (next.X >= 0 && next.Y >= 0 &&
                            next.X < data.Width &&
                            next.Y < data.Height)
                        {
                            if (bits[next.X + next.Y * data.Stride / 4] == floodFrom || 
                                areColorsSimmilar(step,Color.FromArgb(bits[next.X + next.Y * data.Stride / 4]),Color.FromArgb(floodFrom)))
                            {
                                check.AddLast(next);
                                bits[next.X + next.Y * data.Stride / 4] = floodTo;
                            }
                        }
                    }
                }
            }

            Marshal.Copy(bits, 0, data.Scan0, bits.Length);
            bitmap.UnlockBits(data);
        }  
        private bool areColorsSimmilar(int step, Color bmb, Color clr)
        {
            int differenceR = Math.Abs(bmb.R-clr.R);
            int differenceG = Math.Abs(bmb.G-clr.G);
            int differenceB = Math.Abs(bmb.B-clr.B);
            int maxDifference = Math.Max(Math.Max(differenceR,differenceG),differenceB);
            return maxDifference < step ? true : false;
        }
        
    }
}
