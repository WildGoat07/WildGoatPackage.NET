using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WGP
{
    /// <summary>
    /// Chronometer with multiple functions.
    /// </summary>
    public class Chronometer
    {
        #region Internal Fields

        internal DateTime clock;
        internal TimeSpan elapsed;
        internal TimeSpan oldTime;

        #endregion Internal Fields

        #region Private Fields

        private bool paused;
        private Chronometer referenceChrono;
        private double speed;
        private bool useTimer;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Chronometer()
        {
            useTimer = true;
            paused = false;
            clock = DateTime.Now;
            oldTime = TimeSpan.Zero;
            elapsed = TimeSpan.Zero;
            speed = 1;
            referenceChrono = null;
        }

        /// <summary>
        /// Constructor. Used to set relative to another Chronometer. The main timer should not
        /// change its elapsed time to a lower value, as it may set a negative value to its childs.
        /// </summary>
        /// <param name="timer">Relative to.</param>
        public Chronometer(Chronometer timer) : this()
        {
            useTimer = false;
            oldTime = timer.ElapsedTime;
            referenceChrono = timer;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The current elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                Update();
                return elapsed;
            }
            set
            {
                Update();
                elapsed = value;
            }
        }

        /// <summary>
        /// If the chronometer is paused or not.
        /// </summary>
        public bool Paused
        {
            get => paused;
            set
            {
                Update();
                paused = value;
            }
        }

        /// <summary>
        /// Change the speed. The speed is the factor by which is multiplied the elapsed time.
        /// </summary>
        public double Speed
        {
            get => speed;
            set
            {
                Update();
                speed = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Restarts the chronometer.
        /// </summary>
        public void Restart()
        {
            Update();
            elapsed = TimeSpan.Zero;
        }

        #endregion Public Methods

        #region Internal Methods

        internal virtual void Update()
        {
            TimeSpan buffer;
            if (useTimer)
                buffer = DateTime.Now - clock - oldTime;
            else
                buffer = referenceChrono.ElapsedTime - oldTime;
            oldTime += buffer;
            if (!Paused)
                elapsed += TimeSpan.FromMilliseconds(buffer.TotalMilliseconds * speed);
        }

        #endregion Internal Methods
    }
}