using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoFactory
{
    class Function
    {
        /*
         *  图像左旋90度
         */
        public Bitmap LRotate(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            Color color;
            Bitmap newbmp = new Bitmap(h, w);  //初始化一个新图片对象,长宽与原图交换
            for (int y = h - 1; y >= 0; y--)  //遍历每一个像素点
            {
                for (int x = w - 1; x >= 0; x--)
                {
                    color = bmp.GetPixel(x, y);  //获取当前像素的值
                    newbmp.SetPixel(y, w - 1 - x, color);  //绘图
                }
            }
            return newbmp;
        }


        /*
         *  图像右旋90度
         */
        public Bitmap RRotate(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            Color color;
            Bitmap newbmp = new Bitmap(h, w);  //初始化一个新图片对象,长宽与原图交换
            for (int y = h - 1; y >= 0; y--)  //遍历每一个像素点
            {
                for (int x = w - 1; x >= 0; x--)
                {
                    color = bmp.GetPixel(x, y);  //获取当前像素的值
                    newbmp.SetPixel(h - 1 - y, x, color);  //绘图
                }
            }
            return newbmp;
        }


        /*
         * 左右翻转图像
         */
        public Bitmap LRReverse(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            int z;
            Bitmap newbmp = new Bitmap(width, height);  //初始化一个新图片对象
            Color newcolor;
            for (int y = height - 1; y >= 0; y--)  //遍历每一个像素点
            {
                z = 0;
                for (int x = width - 1; x >= 0; x--)
                {
                    newcolor = bmp.GetPixel(x, y);  //获取当前像素的值
                    newbmp.SetPixel(z++, y, newcolor);  //绘图
                }
            }
            return newbmp;  //返回经过翻转后的图片
        }


        /*
       * 上下翻转图像
       */
        public Bitmap UDReverse(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            int z;
            Bitmap newbmp = new Bitmap(width, height);  //初始化一个新图片对象
            Color newcolor;

            for (int x = width - 1; x >= 0; x--)  //遍历每一个像素点
            {
                z = 0;
                for (int y = height - 1; y >= 0; y--)
                {
                    newcolor = bmp.GetPixel(x, y);  //获取当前像素的值
                    newbmp.SetPixel(x, z++, newcolor);  //绘图
                }
            }
            return newbmp;  //返回经过翻转后的图片
        }


        /*
       * 灰度化图像
       */
        public Bitmap ToGray(Bitmap bmp)
        {
            Color color;
            Color newColor;
            for (int i = 0; i < bmp.Width; i++)  //遍历每一个像素点
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    color = bmp.GetPixel(i, j);   //获取该点的像素的RGB的颜色 
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);    //利用公式计算灰度值
                    newColor = Color.FromArgb(gray, gray, gray);
                    bmp.SetPixel(i, j, newColor);  //为该点设置新颜色
                }
            }
            return bmp;
        }


        /*
       * 雾化图像
       */
        public Bitmap Atomization(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;
            Bitmap newbmp = new Bitmap(width, height);   //初始化一个新图片对象
            Random MyRandom = new Random();  //随机数生成器
            Color newColor;
            for (int x = 1; x < width - 1; x++)  //遍历每一个像素点
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int k = MyRandom.Next(123456);  //生成一个随机数

                    int dx = x + k % 19;   //像素块大小
                    int dy = y + k % 19;
                    if (dx >= width)
                        dx = width - 1;
                    if (dy >= height)
                        dy = height - 1;
                    newColor = bmp.GetPixel(dx, dy);
                    newbmp.SetPixel(x, y, newColor);   //为该点设置新颜色
                }
            }
            return newbmp;
        }


        /*
       *改变图像颜色
       */
        public Bitmap ColorChange(Bitmap bmp, int rVal, int gVal, int bVal)
        {
            //判断图像是否存在
            if (bmp == null)
            {
                return null;
            }

            //判断像素改变量是否越界
            if (rVal > 255 || rVal < -255 || gVal > 255 || gVal < -255 || bVal > 255 || bVal < -255)
            {
                return null;
            }

            int height = bmp.Height;
            int width = bmp.Width;
            Color color;
            Color newColor;
            int r, b, g;
            for (int i = 0; i < width; i++)  //遍历每一个像素点
            {
                for (int j = 0; j < height; j++)
                {
                    color = bmp.GetPixel(i, j);   //获取该点的像素的RGB的颜色 
                    r = (int)color.R + rVal;
                    if (r > 255) r = 255;
                    if (r < 0) r = 0;

                    g = (int)color.G + gVal;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;

                    b = (int)color.B + bVal;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;

                    newColor = Color.FromArgb(r, g, b);
                    bmp.SetPixel(i, j, newColor);    //为该点设置新颜色
                }
            }
            return bmp;
        }

    }
}
