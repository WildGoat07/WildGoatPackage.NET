using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Emulate a chronometer by bumping it using the Bump() method. Bumping the tickmeter adds one
    /// tick to the elapsed time, relative to its speed.
    /// </summary>
    public class Tickmeter : Chronometer
    {
        #region Private Fields

        private int _ticksPerSeconds;
        private int ticksToEmulate;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ticksPerSeconds">How many ticks in one second ?</param>
        public Tickmeter(int ticksPerSeconds)
        {
            TicksPerSeconds = ticksPerSeconds;
            elapsed = TimeSpan.Zero;
            ticksToEmulate = 0;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Number of ticks per seconds.
        /// </summary>
        public int TicksPerSeconds
        {
            get => _ticksPerSeconds;
            set => _ticksPerSeconds = Utilities.Max(0, value);
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Bumps to the next tick, resulting to add 1 / TicksPerSeconds * Speed to ElapsedTime.
        /// </summary>
        public void Bump() => ticksToEmulate++;

        #endregion Public Methods

        #region Internal Methods

        internal override void Update()
        {
            if (!Paused)
                elapsed += TimeSpan.FromMilliseconds(ticksToEmulate / _ticksPerSeconds * Speed);
            ticksToEmulate = 0;
        }

        #endregion Internal Methods
    }
}