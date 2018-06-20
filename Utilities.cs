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
            return result;
        }
    }
    static public class Extensions
    {
        private static dynamic InterpolationD(IFunction fct, float percent, dynamic min, dynamic max)
        {
            if (fct == null)
                return default;
            try
            {
                return fct.Image(percent) * (max - min) + min;
            }
            catch (Exception e)
            {
                throw new Exception("Can't compute an interpolation from the given types.");
            }
        }
        /// <summary>
        /// Returns an interpolation.
        /// </summary>
        /// <param name="fct">Referential function.</param>
        /// <param name="percent">Percent. Must be between [0,1].</param>
        /// <param name="min">Minimal value.</param>
        /// <param name="max">Maximal value.</param>
        /// <returns>Interpolation.</returns>
        public static T Interpolation<T>(this IFunction fct, float percent, T min, T max)
        {
            return InterpolationD(fct, percent, min, max);
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
        /// Returns the length squared of a vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Length squared.</returns>
        public static float LengthSquared(this Vector2f vector)
        {
            return vector.X * vector.X + vector.Y * vector.Y;
        }
        /// <summary>
        /// Returns the length of a vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Length.</returns>
        [Obsolete("Use GetLength() instead.")]
        public static float Length(this Vector2f vector)
        {
            return (float)Math.Sqrt(LengthSquared(vector));
        }
        /// <summary>
        /// Returns the length of a vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Length.</returns>
        public static float GetLength(this Vector2f vector)
        {
            return (float)Math.Sqrt(LengthSquared(vector));
        }
        /// <summary>
        /// Returns the length of a vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="length">Length of the vector.</param>
        /// <returns>Length.</returns>
        public static void SetLength(ref this Vector2f vector, float length)
        {
            vector = vector.GetAngle().GenerateVector(length);
        }
        /// <summary>
        /// Normalize a vector.
        /// </summary>
        /// <param name="vector">Vector to normalize.</param>
        /// <returns>Normalized vector.</returns>
        public static Vector2f Normalize(this Vector2f vector)
        {
            float l = vector.GetLength();
            if (l == 0)
                return default;
            return vector / l;
        }
        /// <summary>
        /// Returns the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Angle.</returns>
        [Obsolete("Use GetAngle() instead.")]
        static public Angle Angle(this Vector2f vector)
        {
            return WGP.Angle.FromRadians((float)Math.Atan2(vector.Y, vector.X));
        }
        /// <summary>
        /// Returns the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Angle.</returns>
        static public Angle GetAngle(this Vector2f vector)
        {
            return WGP.Angle.FromRadians((float)Math.Atan2(vector.Y, vector.X));
        }
        /// <summary>
        /// Sets the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="angle">Angle of the vector.</param>
        /// <returns>Angle.</returns>
        static public void SetAngle(ref this Vector2f vector, Angle angle)
        {
            vector = angle.GenerateVector(vector.GetLength());
        }
        /// <summary>
        /// Sets the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="angle">Angle of the vector to add.</param>
        /// <returns>Angle.</returns>
        static public void Rotate(ref this Vector2f vector, Angle angle)
        {
            vector = angle.RotateVector(vector);
        }
        /// <summary>
        /// Returns the intersection between two lines.
        /// </summary>
        /// <param name="line1">First line.</param>
        /// <param name="line2">Second line.</param>
        /// <returns>Intersection of the lines.</returns>
        /// <remarks>The returned value is set to default if there is no collision.</remarks>
        static public Vector2f Intersection(this Line line1, Line line2)
        {
            if (!line1.Collision(line2))
                return default;
            float t = (line2.Position - line1.Position).CrossProduct(line2.Direction) / line1.Direction.CrossProduct(line2.Direction);
            return line1.GetPoint(t);
        }
        /// <summary>
        /// Test the collision between two lines. Test also the collision if one or both of the lines are segments.
        /// </summary>
        /// <param name="line1">First line.</param>
        /// <param name="line2">econd line.</param>
        /// <returns>True if there is a collision.</returns>
        static public bool Collision(this Line line1, Line line2)
        {
            if (line1.Direction.CrossProduct(line2.Direction) == 0 &&
                (line2.Position - line1.Position).CrossProduct(line2.Direction) == 0)
                return false;
            if (line1 is Segment && line2 is Segment)
            {
                float t1 = (line2.Position - line1.Position).CrossProduct(line2.Direction) / line1.Direction.CrossProduct(line2.Direction);
                float t2 = (line2.Position - line1.Position).CrossProduct(line1.Direction) / line1.Direction.CrossProduct(line2.Direction);
                if (t1.IsInRange(0f, ((Segment)line1).Length) && t2.IsInRange(0, ((Segment)line2).Length))
                    return true;
                else
                    return false;
            }
            else if (line1 is Segment)
            {
                float t1 = (line2.Position - line1.Position).CrossProduct(line2.Direction) / line1.Direction.CrossProduct(line2.Direction);
                if (t1.IsInRange(0f, ((Segment)line1).Length))
                    return true;
                else
                    return false;
            }
            else if (line2 is Segment)
            {
                float t2 = (line2.Position - line1.Position).CrossProduct(line1.Direction) / line1.Direction.CrossProduct(line2.Direction);
                if (t2.IsInRange(0f, ((Segment)line2).Length))
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
        /// <summary>
        /// Returns the cross product of two vectors.
        /// </summary>
        /// <param name="vec1">First vector.</param>
        /// <param name="vec2">Second vector.</param>
        /// <returns>Cross product.</returns>
        static public float CrossProduct(this Vector2f vec1, Vector2f vec2)
        {
            return vec1.X * vec2.Y - vec1.Y * vec2.X;
        }
        /// <summary>
        /// Tests if <paramref name="value"/> is in the [<paramref name="min"/> , <paramref name="max"/>] range. <paramref name="min"/> and <paramref name="max"/> are included in the range.
        /// </summary>
        /// <typeparam name="T">Type of the variable. Must be comparable.</typeparam>
        /// <param name="value">Value to compare.</param>
        /// <param name="min">Minimum value of the range.</param>
        /// <param name="max">Maximum value of the range.</param>
        /// <returns>True if <paramref name="value"/> is in the range.</returns>
        static public bool IsInRange<T>(this T value, T min, T max) where T : IComparable
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }
        /// <summary>
        /// Cap the value between a maximum and a minimum value.
        /// </summary>
        /// <typeparam name="T">Type of the variable. Must be comparable.</typeparam>
        /// <param name="value">Value to cap.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>Capped value.</returns>
        static public T Capped<T>(this T value, T min, T max) where T : IComparable
        {
            return Utilities.Max(min, Utilities.Min(max, value));
        }
        #region Stream . Write / Read base type
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Int16 ReadInt16(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Int16)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToInt16(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Int32 ReadInt32(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Int32)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Int64 ReadInt64(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Int64)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToInt64(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public UInt16 ReadUInt16(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(UInt16)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToUInt16(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public UInt32 ReadUInt32(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(UInt32)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToUInt32(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public UInt64 ReadUInt64(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(UInt64)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToUInt64(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Char ReadChar(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Char)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToChar(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Single ReadSingle(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Single)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToSingle(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Double ReadDouble(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Double)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToDouble(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Boolean ReadBoolean(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Boolean)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToBoolean(bytes, 0);
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public SByte ReadInt8(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(SByte)];
            stream.Read(bytes, 0, bytes.Length);
            return (SByte)bytes[0];
        }
        /// <summary>
        /// Reads a base type and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <returns>Base type read.</returns>
        static public Byte ReadUInt8(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Byte)];
            stream.Read(bytes, 0, bytes.Length);
            return bytes[0];
        }
        /// <summary>
        /// Reads a string (Unicode) and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <param name="nbChars">Number of characters to read.</param>
        /// <returns>String read.</returns>
        static public String ReadString(this System.IO.Stream stream, uint nbChars)
        {
            var bytes = new byte[sizeof(Char) * nbChars];
            stream.Read(bytes, 0, bytes.Length);
            return Encoding.Unicode.GetString(bytes);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteInt8(this System.IO.Stream stream, SByte value)
        {
            var bytes = new byte[sizeof(SByte)];
            bytes[0] = (byte)value;
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteInt16(this System.IO.Stream stream, Int16 value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteInt32(this System.IO.Stream stream, Int32 value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteInt64(this System.IO.Stream stream, Int64 value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteUInt8(this System.IO.Stream stream, Byte value)
        {
            var bytes = new byte[sizeof(Byte)];
            bytes[0] = value;
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteUInt16(this System.IO.Stream stream, UInt16 value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteUInt32(this System.IO.Stream stream, UInt32 value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteUInt64(this System.IO.Stream stream, UInt64 value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteBoolean(this System.IO.Stream stream, Boolean value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteSingle(this System.IO.Stream stream, Single value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteDouble(this System.IO.Stream stream, Double value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteChar(this System.IO.Stream stream, Char value)
        {
            var bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        static public void WriteString(this System.IO.Stream stream, String value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        #endregion
    }
}