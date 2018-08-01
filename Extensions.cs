using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace WGP
{
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
            catch (Exception)
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
        public static bool Contains(this FloatRect rect, Vector2f vec)
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
        static public Angle GetAngle(this Vector2f vector)
        {
            return Angle.FromRadians((float)Math.Atan2(vector.Y, vector.X));
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
        /// Reads a string (Unicode) and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <param name="nbChars">Number of characters to read.</param>
        /// <param name="encoding">Encoding of the string.</param>
        /// <returns>String read.</returns>
        static public String ReadString(this System.IO.Stream stream, uint nbChars, Encoding encoding)
        {
            var bytes = new byte[sizeof(Char) * nbChars];
            stream.Read(bytes, 0, bytes.Length);
            return encoding.GetString(bytes);
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
        /// <summary>
        /// Write a base type.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="value">Base type to write.</param>
        /// <param name="encoding">Encoding of the string.</param>
        static public void WriteString(this System.IO.Stream stream, String value, Encoding encoding)
        {
            var bytes = encoding.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        #endregion
        /// <summary>
        /// Returns the top left point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Top left point.</returns>
        static public Vector2f TopLeft(this FloatRect rect) => new Vector2f(rect.Left, rect.Top);
        /// <summary>
        /// Returns the top right point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Top right point.</returns>
        static public Vector2f TopRight(this FloatRect rect) => new Vector2f(rect.Left + rect.Width, rect.Top);
        /// <summary>
        /// Returns the bottom left point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Bottom left point.</returns>
        static public Vector2f BotLeft(this FloatRect rect) => new Vector2f(rect.Left, rect.Top + rect.Height);
        /// <summary>
        /// Returns the bottom right point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Bottom right point.</returns>
        static public Vector2f BotRight(this FloatRect rect) => new Vector2f(rect.Left + rect.Width, rect.Top + rect.Height);
        /// <summary>
        /// Returns the size of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>size of the rect.</returns>
        static public Vector2f Size(this FloatRect rect) => new Vector2f(rect.Width, rect.Height);
        /// <summary>
        /// Returns the top left point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Top left point.</returns>
        static public Vector2i TopLeft(this IntRect rect) => new Vector2i(rect.Left, rect.Top);
        /// <summary>
        /// Returns the top right point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Top right point.</returns>
        static public Vector2i TopRight(this IntRect rect) => new Vector2i(rect.Left + rect.Width, rect.Top);
        /// <summary>
        /// Returns the bottom left point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Bottom left point.</returns>
        static public Vector2i BotLeft(this IntRect rect) => new Vector2i(rect.Left, rect.Top + rect.Height);
        /// <summary>
        /// Returns the bottom right point of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>Bottom right point.</returns>
        static public Vector2i BotRight(this IntRect rect) => new Vector2i(rect.Left + rect.Width, rect.Top + rect.Height);
        /// <summary>
        /// Returns the size of the rect.
        /// </summary>
        /// <param name="rect">AABB.</param>
        /// <returns>size of the rect.</returns>
        static public Vector2i Size(this IntRect rect) => new Vector2i(rect.Width, rect.Height);
        /// <summary>
        /// Returns a random float between two values.
        /// </summary>
        /// <param name="randGen">Generator.</param>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <returns>Random value.</returns>
        static public float NextFloat(this Random randGen, float min, float max)
        {
            return (float)(randGen.NextDouble() * (max - min) + min);
        }
    }
}
