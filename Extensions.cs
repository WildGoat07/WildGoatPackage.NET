using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// Set of extension methods.
    /// </summary>
    static public partial class Extensions
    {
        #region Public Methods

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="nb">Base number.</param>
        /// <returns>Absolute value.</returns>
        static public Double Abs(this Double nb) => Math.Abs(nb);

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="nb">Base number.</param>
        /// <returns>Absolute value.</returns>
        static public SByte Abs(this SByte nb) => Math.Abs(nb);

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="nb">Base number.</param>
        /// <returns>Absolute value.</returns>
        static public Int16 Abs(this Int16 nb) => Math.Abs(nb);

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="nb">Base number.</param>
        /// <returns>Absolute value.</returns>
        static public Int32 Abs(this Int32 nb) => Math.Abs(nb);

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="nb">Base number.</param>
        /// <returns>Absolute value.</returns>
        static public Int64 Abs(this Int64 nb) => Math.Abs(nb);

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="nb">Base number.</param>
        /// <returns>Absolute value.</returns>
        static public Single Abs(this Single nb) => Math.Abs(nb);

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

        /// <summary>
        /// Test the collision between two lines. Test also the collision if one or both of the lines
        /// are segments.
        /// </summary>
        /// <param name="line1">First line.</param>
        /// <param name="line2">econd line.</param>
        /// <returns>True if there is a collision.</returns>
        static public bool Collision(this Line line1, Line line2)
        {
            if ((line1.Direction % Angle.Loop / 2) == (line2.Direction % Angle.Loop / 2) &&
                Vector.CrossProduct((line2.Position - line1.Position), line2.Direction.GenerateVector()) == 0)
                return false;
            double t1 = Vector.CrossProduct((line2.Position - line1.Position), line2.Direction.GenerateVector()) /
        Vector.CrossProduct(line1.Direction.GenerateVector(), line2.Direction.GenerateVector());
            double t2 = Vector.CrossProduct((line2.Position - line1.Position), line1.Direction.GenerateVector()) /
                Vector.CrossProduct(line1.Direction.GenerateVector(), line2.Direction.GenerateVector());
            if (line1 is Segment && line2 is Segment)
            {
                if (t1.IsInRange(0f, ((Segment)line1).Length) && t2.IsInRange(0, ((Segment)line2).Length))
                    return true;
                else
                    return false;
            }
            else if (line1 is Segment)
            {
                if (t1.IsInRange(0f, ((Segment)line1).Length))
                    return true;
                else
                    return false;
            }
            else if (line2 is Segment)
            {
                if (t2.IsInRange(0f, ((Segment)line2).Length))
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Applies a function to each element of an IEnumerable.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="array">IEnumerable to apply the function.</param>
        /// <param name="func">Function.</param>
        static public void ForEach<T>(this IEnumerable<T> array, Action<T> func)
        {
            foreach (var item in array)
                func(item);
        }

        /// <summary>
        /// Returns the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>Angle.</returns>
        static public Angle GetAngle(this Vector vector)
        {
            return Angle.FromRadians(Math.Atan2(vector.Y, vector.X));
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
        /// Returns the intersection between two lines.
        /// </summary>
        /// <param name="line1">First line.</param>
        /// <param name="line2">Second line.</param>
        /// <returns>Intersection of the lines.</returns>
        /// <remarks>The returned value is set to default if there is no collision.</remarks>
        static public Point Intersection(this Line line1, Line line2)
        {
            if ((line1.Direction % Angle.Loop / 2) == (line2.Direction % Angle.Loop / 2))
                return default;
            double t = Vector.CrossProduct((line2.Position - line1.Position), line2.Direction.GenerateVector()) /
                Vector.CrossProduct(line1.Direction.GenerateVector(), line2.Direction.GenerateVector());
            return line1.GetPoint(t);
        }

        /// <summary>
        /// Tests if <paramref name="value"/> is in the [ <paramref name="min"/> , <paramref
        /// name="max"/>] range. <paramref name="min"/> and <paramref name="max"/> are included in
        /// the range.
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
        /// Tests if <paramref name="value"/> is in the <paramref name="interval"/>.
        /// </summary>
        /// <typeparam name="T">Type of the variable. Must be comparable and equatable.</typeparam>
        /// <param name="value">Value to compare.</param>
        /// <param name="interval">Interval to be compared with.</param>
        /// <returns>True if <paramref name="value"/> is in the range.</returns>
        static public bool IsInRange<T>(this T value, Interval<T> interval) where T : IComparable, IEquatable<T> => interval.IsInRange(value);

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

        /// <summary>
        /// Returns a vector with the Y axis set to 0.
        /// </summary>
        /// <param name="vec">Vector to extract the X axis.</param>
        /// <returns>The vector without the Y axis.</returns>
        static public Vector OnlyX(this Vector vec) => new Vector(vec.X, 0);

        /// <summary>
        /// Returns a vector with the X axis set to 0.
        /// </summary>
        /// <param name="vec">Vector to extract the Y axis.</param>
        /// <returns>The vector without the X axis.</returns>
        static public Vector OnlyY(this Vector vec) => new Vector(0, vec.Y);

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
        static public Single ReadSingle(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Single)];
            stream.Read(bytes, 0, bytes.Length);
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// Reads a string (Unicode) and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <param name="nbChars">Number of characters to read.</param>
        /// <returns>String read.</returns>
        static public String ReadString(this System.IO.Stream stream, uint nbChars) => ReadString(stream, nbChars, Encoding.Unicode);

        /// <summary>
        /// Reads a string (Unicode) and returns it.
        /// </summary>
        /// <param name="stream">Input stream.</param>
        /// <param name="nbChars">Number of characters to read.</param>
        /// <param name="encoding">Encoding of the string.</param>
        /// <returns>String read.</returns>
        static public String ReadString(this System.IO.Stream stream, uint nbChars, Encoding encoding)
        {
            var bytes = new byte[encoding.GetByteCount("a") * nbChars];
            stream.Read(bytes, 0, bytes.Length);
            return encoding.GetString(bytes);
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
        static public Byte ReadUInt8(this System.IO.Stream stream)
        {
            var bytes = new byte[sizeof(Byte)];
            stream.Read(bytes, 0, bytes.Length);
            return bytes[0];
        }

        /// <summary>
        /// Sets the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="angle">Angle of the vector to add.</param>
        static public void Rotate(ref this Vector vector, Angle angle)
        {
            var tmp = angle.RotateVector(vector);
        }

        /// <summary>
        /// Sets the angle of the vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="angle">Angle of the vector.</param>
        static public void SetAngle(ref this Vector vector, Angle angle)
        {
            var tmp = angle.GenerateVector(vector.Length);
        }

        /// <summary>
        /// Returns the length of a vector.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="length">Length of the vector.</param>
        public static void SetLength(ref this Vector vector, float length)
        {
            var tmp = vector.GetAngle().GenerateVector(length);
        }

        /// <summary>
        /// Returns the angle in a [0;2PI[ space.
        /// </summary>
        /// <param name="angle"></param>
        static public void To0_2PI(ref this Angle angle) => angle.Radian %= Math.PI * 2;

        /// <summary>
        /// Returns the angle in a [-PI;+PI[ space.
        /// </summary>
        /// <param name="angle"></param>
        static public void ToMinusPI_PI(ref this Angle angle)
        {
            var rad = angle.Radian %= Math.PI * 2;
            if (rad >= Math.PI)
                rad -= Math.PI * 2;
            angle.Radian = rad;
        }

        /// <summary>
        /// Tries to find the value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="info"></param>
        /// <param name="name">Name of the value.</param>
        /// <returns>The value or default if none was found.</returns>
        static public T TryGetValue<T>(this System.Runtime.Serialization.SerializationInfo info, string name)
        {
            try
            {
                return (T)info.GetValue(name, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Tries to find the value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="info"></param>
        /// <param name="name">Name of the value.</param>
        /// <param name="result">The value or default if none was found.</param>
        /// <returns>True if successful, false otherwise.</returns>
        static public bool TryGetValue<T>(this System.Runtime.Serialization.SerializationInfo info, string name, out T result)
        {
            try
            {
                result = (T)info.GetValue(name, typeof(T));
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
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
        static public void WriteString(this System.IO.Stream stream, String value) => WriteString(stream, value, Encoding.Unicode);

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
        static public void WriteUInt8(this System.IO.Stream stream, Byte value)
        {
            var bytes = new byte[sizeof(Byte)];
            bytes[0] = value;
            stream.Write(bytes, 0, bytes.Length);
        }

        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods
    }
}