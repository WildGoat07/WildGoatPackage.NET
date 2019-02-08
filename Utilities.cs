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
        #region Public Methods

        /// <summary>
        /// Creates a hitbox from a Floatrect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Hitbox.</returns>
        static public RectangleHitbox CreateHitbox(FloatRect rect)
        {
            RectangleHitbox result = new RectangleHitbox();
            result.Position = rect.TopLeft() + rect.Size() / 2;
            result.HalfExtend = rect.Size() / 2;
            return result;
        }

        /// <summary>
        /// Creates a FloatRect from multiple coords. The resulting box will be able to contain all
        /// given coords.
        /// </summary>
        /// <param name="pts">Points.</param>
        /// <returns>Resulting FloatRect.</returns>
        static public FloatRect CreateRect(params Vector2f[] pts) => CreateRect(pts.AsEnumerable());

        /// <summary>
        /// Creates a FloatRect from multiple coords. The resulting box will be able to contain all
        /// given coords.
        /// </summary>
        /// <param name="pts">Points.</param>
        /// <returns>Resulting FloatRect.</returns>
        static public FloatRect CreateRect(IEnumerable<Vector2f> pts)
        {
            if (pts == null)
                throw new ArgumentNullException("pts");
            if (pts.Count() == 0)
                throw new Exception("Too few vectors in the list.");
            FloatRect result = new FloatRect();
            Vector2f min = pts.First(), max = pts.First();
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
        /// Generate a number from one or two seeds. The generation is as chaotic as the Random class is.
        /// </summary>
        /// <param name="seed">First seed.</param>
        /// <param name="seed2">Second seed (Optional).</param>
        /// <returns></returns>
        static public Int32 Hash(Int32 seed, Int32 seed2 = 0)
        {
            Int32 r1, r2;
            r1 = new Random(seed).Next();
            r2 = new Random(seed2).Next();
            return r1 + r2;
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
        /// Returns an interpolation.
        /// </summary>
        /// <param name="percent">Percent. Must be between [0,1].</param>
        /// <param name="min">Minimal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Interpolation.</returns>
        public static Time Interpolation(float percent, Time min, Time max)
        {
            return percent * (max - min) + min;
        }

        /// <summary>
        /// Returns the biggest value.
        /// </summary>
        /// <param name="param">values.</param>
        /// <returns>Maximum value.</returns>
        static public Time Max(params Time[] param) => Max(param.AsEnumerable());

        /// <summary>
        /// Returns the biggest value.
        /// </summary>
        /// <param name="param">values.</param>
        /// <returns>Maximum value.</returns>
        static public Time Max(IEnumerable<Time> param)
        {
            if (param.Count() == 0)
                return default;
            Time maxValue = param.First();
            foreach (var item in param)
            {
                if (maxValue < item)
                    maxValue = item;
            }
            return maxValue;
        }

        /// <summary>
        /// Returns the biggest value.
        /// </summary>
        /// <typeparam name="T">Type of the variables. Must be comparable.</typeparam>
        /// <param name="param">values.</param>
        /// <returns>Maximum value.</returns>
        static public T Max<T>(params T[] param) where T : IComparable => Max(param.AsEnumerable());

        /// <summary>
        /// Returns the biggest value.
        /// </summary>
        /// <typeparam name="T">Type of the variables. Must be comparable.</typeparam>
        /// <param name="param">values.</param>
        /// <returns>Maximum value.</returns>
        static public T Max<T>(IEnumerable<T> param) where T : IComparable
        {
            if (param.Count() == 0)
                return default;
            T maxValue = param.First();
            foreach (var item in param)
            {
                if (maxValue.CompareTo(item) <= 0)
                    maxValue = item;
            }
            return maxValue;
        }

        /// <summary>
        /// Returns the smallest value.
        /// </summary>
        /// <typeparam name="T">Type of the variables. Must be comparable.</typeparam>
        /// <param name="param">values.</param>
        /// <returns>Minimum value.</returns>
        static public T Min<T>(params T[] param) where T : IComparable => Min(param.AsEnumerable());

        /// <summary>
        /// Returns the smallest value.
        /// </summary>
        /// <typeparam name="T">Type of the variables. Must be comparable.</typeparam>
        /// <param name="param">values.</param>
        /// <returns>Minimum value.</returns>
        static public T Min<T>(IEnumerable<T> param) where T : IComparable
        {
            if (param.Count() == 0)
                return default;
            T minValue = param.First();
            foreach (var item in param)
            {
                if (minValue.CompareTo(item) >= 0)
                    minValue = item;
            }
            return minValue;
        }

        /// <summary>
        /// Returns the smallest value.
        /// </summary>
        /// <param name="param">values.</param>
        /// <returns>Minimum value.</returns>
        static public Time Min(params Time[] param) => Min(param.AsEnumerable());

        /// <summary>
        /// Returns the smallest value.
        /// </summary>
        /// <param name="param">values.</param>
        /// <returns>Minimum value.</returns>
        static public Time Min(IEnumerable<Time> param)
        {
            if (param.Count() == 0)
                return default;
            Time minValue = param.First();
            foreach (var item in param)
            {
                if (minValue > item)
                    minValue = item;
            }
            return minValue;
        }

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

        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods
    }
}