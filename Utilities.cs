using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace WGP
{
    public static partial class Utilities
    {
        /// <summary>
        /// Returns a percentage.
        /// </summary>
        /// <param name="value">Reference value.</param>
        /// <param name="min">Minmal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Percentage.</returns>
        public static float Percent<T>(T value, T min, T max)
        {
                return PercentD(value, min, max);
        }
        private static float PercentD(dynamic value, dynamic min, dynamic max)
        {
            try
            {
                return ((float)(value - min) / (float)(max - min)).Capped(0, 1);
            }
            catch (Exception)
            {
                try
                {
                    return ((value - min) / (max - min)).Capped(0, 1);
                }
                catch (Exception)
                {
                    throw new Exception("Can't compute a percent from the given types.");
                }
            }
        }
        /// <summary>
        /// Returns a percentage.
        /// </summary>
        /// <param name="value">Reference value.</param>
        /// <param name="min">Minmal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Percentage.</returns>
        public static float Percent(Time value, Time min, Time max)
        {
            return ((value.AsSeconds() - min.AsSeconds()) / (max.AsSeconds() - min.AsSeconds())).Capped(0, 1);
        }
        private static dynamic InterpolationD(float percent, dynamic min, dynamic max)
        {
            try
            {
                return percent * (max - min) + min;
            }
            catch (Exception)
            {
                throw new Exception("Can't compute an interpolation from the given types.");
            }
        }
        /// <summary>
        /// Returns an interpolation.
        /// </summary>
        /// <param name="percent">Percent. Must be between [0,1].</param>
        /// <param name="min">Minimal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Interpolation.</returns>
        public static T Interpolation<T>(float percent, T min, T max)
        {
            return InterpolationD(percent, min, max);
        }
        /// <summary>
        /// Returns an SFML image from a system bitmap image.
        /// </summary>
        /// <param name="img">Bitmap image.</param>
        /// <returns>SFML image.</returns>
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
        /// <summary>
        /// Returns a system bitmap image from a system image.
        /// </summary>
        /// <param name="img">SFML image.</param>
        /// <returns>Bitmap image.</returns>
        public static System.Drawing.Bitmap SFMLImageAsSystemBitmap(Image img)
        {
            System.Drawing.Bitmap result = new System.Drawing.Bitmap((int)img.Size.X, (int)img.Size.Y);
            for (int i = 0; i < img.Size.X; i++)
            {
                for (int j = 0; j < img.Size.Y; j++)
                {
                    var pixel = img.GetPixel((uint)i, (uint)j);
                    result.SetPixel(i, j, System.Drawing.Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B));
                }
            }
            return result;
        }
        /// <summary>
        /// Returns the GCD of two numbers.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns>GCD of the two numbers.</returns>
        public static float GCD(float a, float b)
        {
            float temp = a % b;
            if (temp == 0)
                return b;
            return GCD(b, temp);
        }
        /// <summary>
        /// Returns the smallest value.
        /// </summary>
        /// <typeparam name="T">Type of the variables. Must be comparable.</typeparam>
        /// <param name="param">values.</param>
        /// <returns>Minimum value.</returns>
        static public T Min<T>(params T[] param) where T : IComparable
        {
            int minIndex = 0;
            if (param.Count() == 0)
                return default;
            for (int i = 0; i < param.Count(); i++)
            {
                if (param[minIndex].CompareTo(param[i]) >= 0)
                    minIndex = i;
            }
            return param[minIndex];
        }
        /// <summary>
        /// Returns the smallest value.
        /// </summary>
        /// <param name="param">values.</param>
        /// <returns>Minimum value.</returns>
        static public Time Min(params Time[] param)
        {
            int minIndex = 0;
            if (param.Count() == 0)
                return default;
            for (int i = 0; i < param.Count(); i++)
            {
                if (param[minIndex] > param[i])
                    minIndex = i;
            }
            return param[minIndex];
        }
        /// <summary>
        /// Returns the biggest value.
        /// </summary>
        /// <typeparam name="T">Type of the variables. Must be comparable.</typeparam>
        /// <param name="param">values.</param>
        /// <returns>Maximum value.</returns>
        static public T Max<T>(params T[] param) where T : IComparable
        {
            int maxIndex = 0;
            if (param.Count() == 0)
                return default;
            for (int i = 0; i < param.Count(); i++)
            {
                if (param[maxIndex].CompareTo(param[i]) <= 0)
                    maxIndex = i;
            }
            return param[maxIndex];
        }
        /// <summary>
        /// Returns the biggest value.
        /// </summary>
        /// <param name="param">values.</param>
        /// <returns>Maximum value.</returns>
        static public Time Max(params Time[] param)
        {
            int maxIndex = 0;
            if (param.Count() == 0)
                return default;
            for (int i = 0; i < param.Count(); i++)
            {
                if (param[maxIndex] < param[i])
                    maxIndex = i;
            }
            return param[maxIndex];
        }
        /// <summary>
         /// Creates a FloatRect from multiple coords. The resulting box will be able to contain all given coords.
         /// </summary>
         /// <param name="pts">Points.</param>
         /// <returns>Resulting FloatRect.</returns>
        static public FloatRect CreateRect(params Vector2f[] pts)
        {
            if (pts == null)
                throw new ArgumentNullException("pts");
            if (pts.Length == 0)
                throw new Exception("Too few vectors in the list.");
            FloatRect result = new FloatRect();
            Vector2f min = pts[0], max = pts[0];
            foreach (var item in pts)
            {
                min.X = Min(min.X, item.X);
                min.Y = Min(min.Y, item.Y);
                max.X = Max(max.X, item.X);
                max.Y = Max(max.Y, item.Y);
            }
            result.Left = min.X;
            result.Top = min.Y;
            result.Width = max.X - min.X;
            result.Height = max.Y - min.Y;

            return result;
        }
    }
}