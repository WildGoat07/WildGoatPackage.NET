using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Represent an interval of values.
    /// </summary>
    /// <typeparam name="T">Type of the values.</typeparam>
    [Serializable]
    public struct Interval<T> : IEquatable<Interval<T>>, ISerializable, ICloneable where T : IComparable, IEquatable<T>
    {
        #region Public Fields

        /// <summary>
        /// Maximum value.
        /// </summary>
        public T Maximum;

        /// <summary>
        /// Minimum value.
        /// </summary>
        public T Minimum;

        /// <summary>
        /// The way the borders of the interval are managed if they are inclusive or not.
        /// INCLUDE_BOTH by default.
        /// </summary>
        public IncludingOptions Options;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy">Interval to copy.</param>
        public Interval(Interval<T> copy)
        {
            Options = copy.Options;
            Maximum = copy.Maximum;
            Minimum = copy.Minimum;
        }

        /// <summary>
        /// Constructor. Generate an interval from the given values.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="values">Optionnal values.</param>
        public Interval(T value1, params T[] values)
        {
            Options = IncludingOptions.INCLUDE_BOTH;
            var mi = Utilities.Min(values);
            var ma = Utilities.Max(values);
            Minimum = Utilities.Min(value1, mi);
            Maximum = Utilities.Max(value1, ma);
        }

        /// <summary>
        /// Constructor. Generate an interval from the given values and the given including option.
        /// </summary>
        /// <param name="options">How the borders are managed.</param>
        /// <param name="value1">Value 1.</param>
        /// <param name="values">Optionnal values.</param>
        public Interval(IncludingOptions options, T value1, params T[] values)
        {
            Options = options;
            var mi = Utilities.Min(values);
            var ma = Utilities.Max(values);
            Minimum = Utilities.Min(value1, mi);
            Maximum = Utilities.Max(value1, ma);
        }

        public Interval(SerializationInfo info, StreamingContext context)
        {
            Minimum = (T)info.GetValue("Minimum", typeof(T));
            Maximum = (T)info.GetValue("Maximum", typeof(T));
            Options = (IncludingOptions)info.GetValue("Options", typeof(IncludingOptions));
        }

        #endregion Public Constructors

        #region Public Enums

        /// <summary>
        /// Provide the interval a way to manage borders.
        /// </summary>
        [Flags]
        public enum IncludingOptions
        {
            /// <summary>
            /// No border will be inclusive.
            /// </summary>
            NO_INCLUSIVE_BORDER = 0,

            /// <summary>
            /// The lower border will be inclusive.
            /// </summary>
            INCLUDE_MINIMUM = 1,

            /// <summary>
            /// The higher border will be inclusive.
            /// </summary>
            INCLUDE_MAXIMUM = 2,

            /// <summary>
            /// Both borders will be inclusive.
            /// </summary>
            INCLUDE_BOTH = 3
        }

        #endregion Public Enums

        #region Public Methods

        public static bool operator !=(Interval<T> inter1, Interval<T> inter2) => !inter1.Equals(inter2);

        public static bool operator ==(Interval<T> inter1, Interval<T> inter2) => inter1.Equals(inter2);

        public object Clone() => new Interval<T>(this);

        public bool Equals(Interval<T> other) => Minimum.Equals(other.Minimum) && Maximum.Equals(other.Maximum);

        public override bool Equals(object obj) => Equals((Interval<T>)obj);

        public override int GetHashCode() => Minimum.GetHashCode() * Maximum.GetHashCode() + Maximum.GetHashCode();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Minimum", Minimum);
            info.AddValue("Maximum", Minimum);
            info.AddValue("Options", Options);
        }

        /// <summary>
        /// Returns true if the value is within the interval.
        /// </summary>
        /// <param name="value">Value to test.</param>
        /// <returns>True if the value is in the interval.</returns>
        public bool IsInRange(T value)
        {
            bool matchMin, matchMax;
            if ((Options & IncludingOptions.INCLUDE_MINIMUM) == IncludingOptions.INCLUDE_MINIMUM)
                matchMin = Minimum.CompareTo(value) <= 0;
            else
                matchMin = Minimum.CompareTo(value) < 0;
            if ((Options & IncludingOptions.INCLUDE_MAXIMUM) == IncludingOptions.INCLUDE_MAXIMUM)
                matchMax = Maximum.CompareTo(value) >= 0;
            else
                matchMax = Maximum.CompareTo(value) > 0;
            return matchMin && matchMax;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if ((Options & IncludingOptions.INCLUDE_MINIMUM) == IncludingOptions.INCLUDE_MINIMUM)
                sb.Append("[");
            else
                sb.Append("]");
            sb.Append(Minimum.ToString());
            sb.Append(";");
            sb.Append(Minimum.ToString());
            if ((Options & IncludingOptions.INCLUDE_MAXIMUM) == IncludingOptions.INCLUDE_MAXIMUM)
                sb.Append("]");
            else
                sb.Append("[");
            return sb.ToString();
        }

        #endregion Public Methods
    }
}