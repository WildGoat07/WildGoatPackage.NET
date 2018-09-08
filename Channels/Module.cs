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
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Multiplication.</returns>
        public static float Multiplying(float left, float right) => left * right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Division.</returns>
        public static float Dividing(float left, float right) => left / right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Addition.</returns>
        public static float Adding(float left, float right) => left + right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Substraction.</returns>
        public static float Substracting(float left, float right) => left - right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Multiplication.</returns>
        public static double Multiplying(double left, double right) => left * right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Division.</returns>
        public static double Dividing(double left, double right) => left / right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Addition.</returns>
        public static double Adding(double left, double right) => left + right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Substraction.</returns>
        public static double Substracting(double left, double right) => left - right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Multiplication.</returns>
        public static int Multiplying(int left, int right) => left * right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Division.</returns>
        public static int Dividing(int left, int right) => left / right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Addition.</returns>
        public static int Adding(int left, int right) => left + right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Substraction.</returns>
        public static int Substracting(int left, int right) => left - right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Multiplication.</returns>
        public static long Multiplying(long left, long right) => left * right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Division.</returns>
        public static long Dividing(long left, long right) => left / right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Addition.</returns>
        public static long Adding(long left, long right) => left + right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Substraction.</returns>
        public static long Substracting(long left, long right) => left - right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Multiplication.</returns>
        public static ulong Multiplying(ulong left, ulong right) => left * right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Division.</returns>
        public static ulong Dividing(ulong left, ulong right) => left / right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Addition.</returns>
        public static ulong Adding(ulong left, ulong right) => left + right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Substraction.</returns>
        public static ulong Substracting(ulong left, ulong right) => left - right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Multiplication.</returns>
        public static uint Multiplying(uint left, uint right) => left * right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Division.</returns>
        public static uint Dividing(uint left, uint right) => left / right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Addition.</returns>
        public static uint Adding(uint left, uint right) => left + right;
        /// <summary>
        /// Use the multiplication between the two given values.
        /// </summary>
        /// <param name="left">Left value.</param>
        /// <param name="right">Right value.</param>
        /// <returns>Substraction.</returns>
        public static uint Substracting(uint left, uint right) => left - right;
        /// <summary>
        /// The direct value of the module.
        /// </summary>
        public T Value { get; set; }
        internal MasterChannel<T> master;
        internal Module<T> Superior;
        internal List<Channel<T>> childs;
        /// <summary>
        /// Constructor.
        /// </summary>
        public Module()
        {
            Value = default;
            childs = new List<Channel<T>>();
        }
        /// <summary>
        /// Returns the final result of the value, from the master to the last channel.
        /// </summary>
        public abstract T FinalValue { get; }
        public static implicit operator T(Module<T> m) => m.FinalValue;
        /// <summary>
        /// Adds a channel to its childs.
        /// </summary>
        /// <param name="chan">Channel to add.</param>
        public abstract void AddChild(Channel<T> chan);
        /// <summary>
        /// Remove a channel to its childs.
        /// </summary>
        /// <param name="chan">Channel to add.</param>
        public abstract void RemoveChild(Channel<T> chan);

        public IEnumerator<Channel<T>> GetEnumerator() => childs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
