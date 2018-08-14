using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace WGP
{
    public static class Utilities
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
        }/// <summary>
        /// Creates a FloatRect from 2 coords.
        /// </summary>
        /// <param name="pt1">First point.</param>
        /// <param name="pt2">Second point.</param>
        /// <returns>Resulting FloatRect.</returns>
        static public FloatRect CreateRect(Vector2f pt1, Vector2f pt2)
        {
            FloatRect result = new FloatRect();
            result.Left = Min(pt1.X, pt2.X);
            result.Top = Min(pt1.Y, pt2.Y);
            result.Width = Max(pt1.X, pt2.X) - result.Left;
            result.Height = Max(pt1.Y, pt2.Y) - result.Top;

            return result;
        }
    }
}