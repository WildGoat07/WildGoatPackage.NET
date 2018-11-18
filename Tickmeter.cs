using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace WGP
{
    /// <summary>
    /// Emulate a chronometer by bumping it using the Bump() method. Bumping the tickmeter adds one tick to the elapsed time, relative to its speed.
    /// </summary>
    public class Tickmeter : Chronometer
    {
        private int _ticksPerSeconds;
        private int ticksToEmulate;
        /// <summary>
        /// Number of ticks per seconds. Has no effect setting it if the tickmeter is relative to another one.
        /// </summary>
        public int TicksPerSeconds
        {
            get => _ticksPerSeconds;
            set => _ticksPerSeconds = Utilities.Max(0, value);
        }
        /// <summary>
        /// Bumps to the next tick, resulting to add 1 / TicksPerSeconds * Speed to ElapsedTime.
        /// </summary>
        public void Bump() => ticksToEmulate++;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ticksPerSeconds">How many ticks in one second ?</param>
        public Tickmeter(int ticksPerSeconds)
        {
            TicksPerSeconds = ticksPerSeconds;
            elapsed = Time.Zero;
            ticksToEmulate = 0;
        }

        protected override void Update()
        {
            if (!Paused)
                elapsed += Time.FromSeconds((float)ticksToEmulate / _ticksPerSeconds * Speed);
            ticksToEmulate = 0;
        }
    }
}
