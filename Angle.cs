using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// Angle class. Used for angle conversions.
    /// </summary>
    [Serializable]
    public struct Angle : IComparable, IComparable<Angle>, IEquatable<Angle>, IFormattable, ISerializable, ICloneable
    {
        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Angle(Angle other) => Radian = other.Radian;

        public Angle(SerializationInfo info, StreamingContext context)
        {
            Radian = info.GetSingle("Radians");
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Angle corresponing to 360 degrees or 2PI radians.
        /// </summary>
        static public Angle Loop => FromRadians(2 * Math.PI);

        /// <summary>
        /// Angle corresponing to 0 degrees or 0 radians.
        /// </summary>
        static public Angle Zero => FromRadians(0);

        /// <summary>
        /// Angle in degrees.
        /// </summary>
        public double Degree
        {
            get => Radian * 180 / Math.PI;
            set => Radian = value * Math.PI / 180;
        }

        /// <summary>
        /// Angle in radians.
        /// </summary>
        public double Radian { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates a new instance from a value in degrees.
        /// </summary>
        /// <param name="degree">Value in degrees.</param>
        /// <returns>New Angle instance.</returns>
        static public Angle FromDegrees(double degree) => new Angle() { Degree = degree };

        /// <summary>
        /// Creates a new instance from a value in radians.
        /// </summary>
        /// <param name="radian">Value in radians.</param>
        /// <returns>New Angle instance.</returns>
        static public Angle FromRadians(double radian) => new Angle() { Radian = radian };

        static public Angle operator -(Angle left, Angle right)
        {
            return FromRadians(left.Radian - right.Radian);
        }

        static public Angle operator -(Angle angle)
        {
            return FromRadians(-angle.Radian);
        }

        static public bool operator !=(Angle left, Angle right)
        {
            return !left.Equals(right);
        }

        static public Angle operator %(Angle left, Angle right)
        {
            return FromRadians(left.Radian % right.Radian);
        }

        static public Angle operator *(Angle angle, double value)
        {
            return FromRadians(angle.Radian * value);
        }

        static public Angle operator *(double value, Angle angle)
        {
            return FromRadians(angle.Radian * value);
        }

        static public Angle operator /(Angle angle, double value)
        {
            return FromRadians(angle.Radian / value);
        }

        static public Angle operator /(double value, Angle angle)
        {
            return FromRadians(value / angle.Radian);
        }

        static public double operator /(Angle left, Angle right)
        {
            return left.Radian / right.Radian;
        }

        static public Angle operator +(Angle left, Angle right)
        {
            return FromRadians(left.Radian + right.Radian);
        }

        static public bool operator <(Angle left, Angle right)
        {
            return left.CompareTo(right) < 0;
        }

        static public bool operator <=(Angle left, Angle right)
        {
            return left.CompareTo(right) <= 0;
        }

        static public bool operator ==(Angle left, Angle right)
        {
            return left.Equals(right);
        }

        static public bool operator >(Angle left, Angle right)
        {
            return left.CompareTo(right) > 0;
        }

        static public bool operator >=(Angle left, Angle right)
        {
            return left.CompareTo(right) >= 0;
        }

        public object Clone() => new Angle(this);

        public int CompareTo(object obj) => CompareTo((Angle)obj);

        public int CompareTo(Angle other) => Radian.CompareTo(other.Radian);

        /// <summary>
        /// Returns the cosine of the angle.
        /// </summary>
        /// <returns>Cosine.</returns>
        public double Cos() => Math.Cos(Radian);

        /// <summary>
        /// Returns the hyperbolic cosine of the angle.
        /// </summary>
        /// <returns>Hyperbolic cosine.</returns>
        public double Cosh() => Math.Cosh(Radian);

        public bool Equals(Angle other) => Radian.Equals(other.Radian);

        public override bool Equals(object obj) => Radian.Equals(((Angle)obj).Radian);

        /// <summary>
        /// Generates a vector based on the angle.
        /// </summary>
        /// <param name="length">The length of the vector. (Optional)</param>
        /// <returns>Generated vector.</returns>
        public Vector GenerateVector(double length = 1) => RotateVector(new Vector(length, 0));

        public override int GetHashCode() => Radian.GetHashCode();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Radians", Radian);
        }

        /// <summary>
        /// Returns the rotated vector corresponding to the angle.
        /// </summary>
        /// <param name="vector">Vector to rotate.</param>
        public Vector RotateVector(Vector vector)
        {
            var a = vector.GetAngle();
            a += this;
            var l = vector.Length;
            return new Vector(a.Cos() * l, a.Sin() * l);
        }

        /// <summary>
        /// Returns the rotated vector corresponding to the angle.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="y">Y component of the vector.</param>
        public Vector RotateVector(double x, double y) => RotateVector(new Vector(x, y));

        /// <summary>
        /// Returns the sinus of the angle.
        /// </summary>
        /// <returns>Sinus.</returns>
        public double Sin() => Math.Sin(Radian);

        /// <summary>
        /// Returns the hyperbolic sinus of the angle.
        /// </summary>
        /// <returns>Hyperbolic sinus.</returns>
        public double Sinh() => Math.Sinh(Radian);

        /// <summary>
        /// Returns the tangent of the angle.
        /// </summary>
        /// <returns>Tangent.</returns>
        public double Tan() => Math.Tan(Radian);

        /// <summary>
        /// Returns the hyperbolic tangent of the angle.
        /// </summary>
        /// <returns>Hyperbolic tangent.</returns>
        public double Tanh() => Math.Tanh(Radian);

        public override string ToString() => ToString(null, System.Globalization.CultureInfo.CurrentCulture);

        /// <summary>
        /// Format the string : "D" for the degrees, "R" for the radians.
        /// </summary>
        /// <param name="format">Format of the string.</param>
        /// <param name="formatProvider"></param>
        /// <returns>Formated string.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null || format == "" || format == "G")
                return "{ [Radians:" + Radian + "] , [Degrees:" + Degree + "] }";
            else if (format == "D")
                return Degree.ToString("G", formatProvider);
            else if (format == "R")
                return Radian.ToString("G", formatProvider);
            else
                return "{ [Radians:" + Radian + "] , [Degrees:" + Degree + "] }";
        }

        #endregion Public Methods
    }
}