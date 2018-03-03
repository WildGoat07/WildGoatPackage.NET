using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace WGP
{
    public static class Utilities
    {
        public static float Percent(float value, float min, float max)
        {
            return Math.Max(Math.Min((value - min) / (max - min), 1), 0);
        }
        public static float Interpolation(this IFunction fct, float percent, float min, float max)
        {
            return fct.Image(percent) * (max - min) + min;
        }
        public static float Interpolation(float percent, float min, float max)
        {
            return percent * (max - min) + min;
        }
        public static Image SystemBitmapAsSFML(System.Drawing.Bitmap img)
        {
            Image result = new Image((uint)img.Width, (uint)img.Height);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    var pixel = img.GetPixel(i, j);
                    result.SetPixel((uint)i, (uint)j, new Color(pixel.R, pixel.G, pixel.B, pixel.A));
                }
            }
            return result;
        }
        public static bool Contains(this FloatRect rect, SFML.System.Vector2f vec)
        {
            return rect.Contains(vec.X, vec.Y);
        }
        public static string Crypt(string str, string key = "")
        {
            string result = "";
            for (int i = 0; i < str.Count(); i++)
            {
                byte car = (byte)str[i];
                car = (byte)(255 - car);
                if (i > 0)
                    car += (byte)str[i - 1];
                car += (byte)(str.Count() * 10 + 50 + i * 5);
                if (key.Count() > 0)
                    car += (byte)(key[i % key.Count()]);
                result += (char)car;
            }
            return result;
        }
        public static string Uncrypt(string str, string key = "")
        {
            string result = "";
            for (int i = 0;i<str.Count();i++)
            {
                byte car = (byte)str[i];
                if (key.Count() > 0)
                    car -= (byte)(key[i % key.Count()]);
                car -= (byte)(str.Count() * 10 + 50 + i * 5);
                if (i > 0)
                    car -= (byte)result[i - 1];
                car = (byte)(255 - car);
                result += (char)car;
            }
            return result;
        }
        public static float Modulo(float nb, float mod)
        {
            int frac = (int)(nb / mod);
            nb -= frac * mod;
            if (nb < 0)
                nb += mod;
            return nb;
        }
     }
}
