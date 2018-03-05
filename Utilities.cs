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
        /// <summary>
        /// Returns a percentage.
        /// </summary>
        /// <param name="value">Reference value.</param>
        /// <param name="min">Minmal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Percentage.</returns>
        public static float Percent(float value, float min, float max)
        {
            return Math.Max(Math.Min((value - min) / (max - min), 1), 0);
        }
        /// <summary>
        /// Returns an interpolation.
        /// </summary>
        /// <param name="fct">Referential function.</param>
        /// <param name="percent">Percent. Must be between [0,1].</param>
        /// <param name="min">Minimal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Interpolation.</returns>
        public static float Interpolation(this IFunction fct, float percent, float min, float max)
        {
            return fct.Image(percent) * (max - min) + min;
        }
        /// <summary>
        /// Returns an interpolation.
        /// </summary>
        /// <param name="percent">Percent. Must be between [0,1].</param>
        /// <param name="min">Minimal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Interpolation.</returns>
        public static float Interpolation(float percent, float min, float max)
        {
            return percent * (max - min) + min;
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
        /// Test if a FloatRect contains a Vector2f.
        /// </summary>
        /// <param name="rect">Box.</param>
        /// <param name="vec">Vector.</param>
        /// <returns>True if the vector is inside the box, false if not.</returns>
        public static bool Contains(this FloatRect rect, SFML.System.Vector2f vec)
        {
            return rect.Contains(vec.X, vec.Y);
        }
        /// <summary>
        /// Crypt a string.
        /// </summary>
        /// <param name="str">String to crypt.</param>
        /// <param name="key">Key to crypt. (Optional)</param>
        /// <returns>Crypted string.</returns>
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
        /// <summary>
        /// Uncrypt a string.
        /// </summary>
        /// <param name="str">String to uncrypt.</param>
        /// <param name="key">Key to uncrypt. (Optional)</param>
        /// <returns>Uncrypted string.</returns>
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
        /// Returns the length squared of a vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Length squared.</returns>
        public static float LengthSquared(this SFML.System.Vector2f vector)
        {
            return vector.X * vector.X + vector.Y * vector.Y;
        }
        /// <summary>
        /// Returns the length of a vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Length.</returns>
        public static float Length(this SFML.System.Vector2f vector)
        {
            return (float)Math.Sqrt(LengthSquared(vector));
        }
        /// <summary>
        /// Normalize a vector.
        /// </summary>
        /// <param name="vector">Vector to normalize.</param>
        /// <returns>Normalized vector.</returns>
        public static SFML.System.Vector2f Normalize(this SFML.System.Vector2f vector)
        {
            float l = vector.Length();
            if (l == 0)
                return vector;
            return vector / l;
        }
        /// <summary>
        /// Returns the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Angle.</returns>
        static public Angle Angle(this SFML.System.Vector2f vector)
        {
            return WGP.Angle.FromRadians((float)System.Math.Atan2(vector.Y, vector.X));
        }
        /// <summary>
        /// Returns the angle of the vector.
        /// </summary>
        /// <param name="x">X component of a vector.</param>
        /// <param name="y">Y component of a vector.</param>
        /// <returns>Angle.</returns>
        static public Angle Angle(float x, float y)
        {
            return Angle(new SFML.System.Vector2f(x, y));
        }
    }
}