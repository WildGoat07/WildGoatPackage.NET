using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP.CHANNELS
{
    /// <summary>
    /// The base module of a channel.
    /// </summary>
    /// <typeparam name="T">Type to share between channels.</typeparam>
    public abstract class Module<T> : IEnumerable<Channel<T>>
    {
        #region Internal Fields

        internal List<Channel<T>> childs;

        internal MasterChannel<T> master;

        internal Module<T> Superior;

        #endregion Internal Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Module()
        {
            Value = default;
            childs = new List<Channel<T>>();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the final result of the value, from the master to the last channel.
        /// </summary>
        public abstract T FinalValue { get; }

        /// <summary>
        /// The direct value of the module.
        /// </summary>
        public T Value { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Addition.</returns>
        public static T Adding(T left, T right) => (dynamic)left + right;

        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Division.</returns>
        public static T Dividing(T left, T right) => (dynamic)left / right;

        public static implicit operator T(Module<T> m) => m.FinalValue;

        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Multiplication.</returns>
        public static T Multiplying(T left, T right) => (dynamic)left * right;

        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Substraction.</returns>
        public static T Substracting(T left, T right) => (dynamic)left - right;

        /// <summary>
        /// Adds a channel to its childs.
        /// </summary>
        /// <param name="chan">Channel to add.</param>
        public abstract void AddChild(Channel<T> chan);

        public IEnumerator<Channel<T>> GetEnumerator() => childs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Remove a channel to its childs.
        /// </summary>
        /// <param name="chan">Channel to add.</param>
        public abstract void RemoveChild(Channel<T> chan);

        #endregion Public Methods
    }
}