using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Angle class. Used for angle conversions.
    /// </summary>
    public struct Angle : IComparable, IComparable<Angle>, IEquatable<Angle>
    {
        private float radian;
        /// <summary>
        /// Angle in degrees.
        /// </summary>
        public float Degree
        {
            get => radian * 180 / (float)Math.PI;
            set => radian = value * (float)Math.PI / 180;
        }
        /// <summary>
        /// Angle in radians.
        /// </summary>
        public float Radian
        {
            get => radian;
            set => radian = value;
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
        public SFML.System.Vector2f RotateVector(float x, float y)
        {
            return RotateVector(new SFML.System.Vector2f(x, y));
        }
        /// <summary>
        /// Generates a vector based on the angle.
        /// </summary>
        /// <param name="length">The length of the vector. (Optional)</param>
        /// <returns>Generated vector.</returns>
        public SFML.System.Vector2f GenerateVector(float length = 1)
        {
            return RotateVector(new SFML.System.Vector2f(length, 0));
        }
        /// <summary>
        /// Creates a new instance from a value in degrees.
        /// </summary>
        /// <param name="degree">Value in degrees.</param>
        /// <returns>New Angle instance.</returns>
        static public Angle FromDegrees(float degree)
        {
            return new Angle() { Degree = degree };
        }
        /// <summary>
        /// Creates a new instance from a value in radians.
        /// </summary>
        /// <param name="radian">Value in radians.</param>
        /// <returns>New Angle instance.</returns>
        static public Angle FromRadians(float radian)
        {
            return new Angle() { Radian = radian };
        }
        /// <summary>
        /// Returns the cosine of the angle.
        /// </summary>
        /// <returns>Cosinus.</returns>
        public float Cos()
        {
            return (float)Math.Cos(radian);
        }
        /// <summary>
        /// Returns the sinus of the angle.
        /// </summary>
        /// <returns>Sinus.</returns>
        public float Sin()
        {
            return (float)Math.Sin(radian);
        }
        /// <summary>
        /// Returns the tangent of the angle.
        /// </summary>
        /// <returns>Tangent.</returns>
        public float Tan()
        {
            return (float)Math.Tan(radian);
        }
        /// <summary>
        /// Returns the hyperbolic cosine of the angle.
        /// </summary>
        /// <returns>Cosinus.</returns>
        public float Cosh()
        {
            return (float)Math.Cosh(radian);
        }
        /// <summary>
        /// Returns the hyperbolic sinus of the angle.
        /// </summary>
        /// <returns>Sinus.</returns>
        public float Sinh()
        {
            return (float)Math.Sinh(radian);
        }
        /// <summary>
        /// Returns the hyperbolic tangent of the angle.
        /// </summary>
        /// <returns>Tangent.</returns>
        public float Tanh()
        {
            return (float)Math.Tanh(radian);
        }
        public int CompareTo(object obj)
        {
            return CompareTo((Angle)obj);
        }
        public int CompareTo(Angle other)
        {
            return radian.CompareTo(other.radian);
        }
        public bool Equals(Angle other)
        {
            return radian.Equals(other.radian);
        }
        public override bool Equals(object obj)
        {
            return radian.Equals(((Angle)obj).radian);
        }
        public override int GetHashCode()
        {
            return (int)(radian * 10000);
        }
        public override string ToString()
        {
            return "{ [Radians:" + Radian + "] , [Degrees:" + Degree + "] }";
        }
    }
}
