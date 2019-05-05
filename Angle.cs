using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Angle class. Used for angle conversions.
    /// </summary>
    [Serializable]
    public struct Angle : IComparable, IComparable<Angle>, IEquatable<Angle>, IFormattable, ISerializable, ICloneable
    {
        #region Private Fields

        private float _degree;

        private float _radian;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Angle(Angle other)
        {
            _radian = other._radian;
            _degree = other._degree;
        }

        public Angle(SerializationInfo info, StreamingContext context)
        {
            _radian = info.GetSingle("Radian");
            _degree = info.GetSingle("Degree");
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Angle corresponing to 360 degrees or 2PI radians.
        /// </summary>
        static public Angle Loop => FromDegrees(360);

        /// <summary>
        /// Angle corresponing to 0 degrees or 0 radians.
        /// </summary>
        static public Angle Zero => FromRadians(0);

        /// <summary>
        /// Angle in degrees.
        /// </summary>
        public float Degree
        {
            get => _degree;
            set
            {
                _degree = value;
                LinkToRadians();
            }
        }

        /// <summary>
        /// Angle in radians.
        /// </summary>
        public float Radian
        {
            get => _radian;
            set
            {
                _radian = value;
                LinkToDegrees();
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates a new instance from a value in degrees.
        /// </summary>
        /// <param name="degree">Value in degrees.</param>
        /// <returns>New Angle instance.</returns>
        static public Angle FromDegrees(float degree) => new Angle() { Degree = degree };

        /// <summary>
        /// Creates a new instance from a value in radians.
        /// </summary>
        /// <param name="radian">Value in radians.</param>
        /// <returns>New Angle instance.</returns>
        static public Angle FromRadians(float radian) => new Angle() { Radian = radian };

        static public Angle operator -(Angle left, Angle right) => new Angle
        {
            _radian = left._radian - right._radian,
            _degree = left._degree - right._degree
        };

        static public Angle operator -(Angle angle) => new Angle
        {
            _radian = -angle._radian,
            _degree = -angle._degree
        };

        static public bool operator !=(Angle left, Angle right)
        {
            return !left.Equals(right);
        }

        static public Angle operator %(Angle left, Angle right) => new Angle
        {
            _radian = left._radian % right._radian,
            _degree = left._degree % right._degree
        };

        static public Angle operator *(Angle angle, float value) => new Angle
        {
            _radian = angle._radian * value,
            _degree = angle._degree * value
        };

        static public Angle operator *(float value, Angle angle) => new Angle
        {
            _radian = angle._radian * value,
            _degree = angle._degree * value
        };

        static public Angle operator /(Angle angle, float value) => new Angle
        {
            _radian = angle._radian / value,
            _degree = angle._degree / value
        };

        static public float operator /(Angle left, Angle right)
        {
            var perc1 = left._radian / right._radian;
            var perc2 = left._degree / right._degree;
            if ((perc1 - Math.Truncate(perc1)).ToString().Length > (perc2 - Math.Truncate(perc2)).ToString().Length)
                return perc2;
            else
                return perc1;
        }

        static public Angle operator +(Angle left, Angle right) => new Angle
        {
            _radian = left._radian + right._radian,
            _degree = left._degree + right._degree
        };

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

        public int CompareTo(Angle other)
        {
            var res1 = _radian.CompareTo(other._radian);
            var res2 = _degree.CompareTo(other._degree);
            if (res2 == 0)
                return res2;
            return res1;
        }

        /// <summary>
        /// Returns the cosine of the angle.
        /// </summary>
        /// <returns>Cosine.</returns>
        public float Cos() => (float)Math.Cos(Radian);

        /// <summary>
        /// Returns the hyperbolic cosine of the angle.
        /// </summary>
        /// <returns>Hyperbolic cosine.</returns>
        public float Cosh() => (float)Math.Cosh(Radian);

        public bool Equals(Angle other) => CompareTo(other) == 0;

        public override bool Equals(object obj) => Equals((Angle)obj);

        /// <summary>
        /// Generates a vector based on the angle.
        /// </summary>
        /// <param name="length">The length of the vector. (Optional)</param>
        /// <returns>Generated vector.</returns>
        public SFML.System.Vector2f GenerateVector(float length = 1) => RotateVector(new SFML.System.Vector2f(length, 0));

        public override int GetHashCode() => Radian.GetHashCode();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Radian", _radian);
            info.AddValue("Degree", _degree);
        }

        /// <summary>
        /// Returns the rotated vector corresponding to the angle.
        /// </summary>
        /// <param name="vector">Vector to rotate.</param>
        public SFML.System.Vector2f RotateVector(SFML.System.Vector2f vector)
        {
            SFML.Graphics.Transform tr = SFML.Graphics.Transform.Identity;
            tr.Rotate(Degree);
            return tr.TransformPoint(vector);
        }

        /// <summary>
        /// Returns the rotated vector corresponding to the angle.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="y">Y component of the vector.</param>
        public SFML.System.Vector2f RotateVector(float x, float y) => RotateVector(new SFML.System.Vector2f(x, y));

        /// <summary>
        /// Returns the sinus of the angle.
        /// </summary>
        /// <returns>Sinus.</returns>
        public float Sin() => (float)Math.Sin(Radian);

        /// <summary>
        /// Returns the hyperbolic sinus of the angle.
        /// </summary>
        /// <returns>Hyperbolic sinus.</returns>
        public float Sinh() => (float)Math.Sinh(Radian);

        /// <summary>
        /// Returns the tangent of the angle.
        /// </summary>
        /// <returns>Tangent.</returns>
        public float Tan() => (float)Math.Tan(Radian);

        /// <summary>
        /// Returns the hyperbolic tangent of the angle.
        /// </summary>
        /// <returns>Hyperbolic tangent.</returns>
        public float Tanh() => (float)Math.Tanh(Radian);

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

        #region Private Methods

        private void LinkToDegrees() => _degree = _radian / (float)Math.PI * 180;

        private void LinkToRadians() => _radian = _degree * (float)Math.PI / 180;

        #endregion Private Methods
    }
}