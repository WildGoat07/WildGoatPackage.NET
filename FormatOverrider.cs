using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Allow the possibility to override the ToString() method of any object.
    /// </summary>
    public struct FormatOverrider : IFormattable
    {
        #region Public Fields

        /// <summary>
        /// Overrided object.
        /// </summary>
        public readonly object Object;

        /// <summary>
        /// Overriding function.
        /// </summary>
        public readonly Func<string, IFormatProvider, string> Overrider;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="obj">Object to override.</param>
        /// <param name="overrider">Overriding function.</param>
        public FormatOverrider(object obj, Func<string, IFormatProvider, string> overrider)
        {
            Object = obj;
            Overrider = overrider;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="obj">Object to override.</param>
        /// <param name="overrider">Overriding function.</param>
        public FormatOverrider(object obj, Func<string> overrider)
        {
            Object = obj;
            Overrider = (s, fp) => overrider();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="obj">Object to override.</param>
        /// <param name="display">Result of the ToString() method.</param>
        public FormatOverrider(object obj, string display)
        {
            Object = obj;
            Overrider = (s, fp) => (string)display.Clone();
        }

        #endregion Public Constructors

        #region Public Methods

        public override bool Equals(object obj) => Object.Equals(obj);

        public override int GetHashCode() => Object.GetHashCode();

        public override string ToString() => ToString(null, System.Globalization.CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider) => Overrider == null ? throw new InvalidOperationException("Overrider is not defined") : Overrider(format, formatProvider);

        #endregion Public Methods
    }
}