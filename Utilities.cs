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
                return Max(Min((value - min) / (max - min), 1), 0);
            }
            catch (Exception)
            {
                throw new Exception("Can't compute a percent from the given types.");
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
        }
        /// <summary>
        /// Converts a 4 bytes number to a string with 4 characters.
        /// </summary>
        /// <param name="nb">Number to convert.</param>
        /// <returns>The FOURCC corresponding to the number.</returns>
        static public string Int32ToFOURCC(System.Int32 nb)
        {
            char[] str = new char[4];
            str[3] = (char)(0x000000ff & nb);
            str[2] = (char)((0x0000ff00 & nb) / 0x0000100);
            str[1] = (char)((0x00ff0000 & nb) / 0x0010000);
            str[0] = (char)((0xff000000 & nb) / 0x1000000);
            return new string(str);
        }
        /// <summary>
        /// Converts a 4 characters string to a 4 bytes number.
        /// </summary>
        /// <param name="FOURCC">FOURCC to convert.</param>
        /// <returns>The number corresponding to the FOURCC.</returns>
        static public Int32 FOURCCToInt32(string FOURCC)
        {
            if (FOURCC.Count() != 4)
                throw new Exception("The FOURCC id doesn't have 4 characters.");
            char[] str = FOURCC.ToArray();
            Int32 result = 0;
            result += str[3];
            result += str[2] * 0x100;
            result += str[1] * 0x10000;
            result += str[0] * 0x1000000;
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