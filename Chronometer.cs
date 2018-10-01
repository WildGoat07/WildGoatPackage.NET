using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Chronometer with multiple functions.
    /// </summary>
    public class Chronometer
    {
        private dynamic referenceTime;
        private Time elapsed;
        private float speed;
        private bool paused;
        private Time buffer;
        private Time oldTime;
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
        public float Speed
        {
            get => speed;
            set
            {
                Update();
                speed = value;
            }
        }
        /// <summary>
        /// The current elapsed time.
        /// </summary>
        public Time ElapsedTime
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
        /// Constructor.
        /// </summary>
        public Chronometer()
        {
            paused = false;
            oldTime = Time.Zero;
            referenceTime = new Clock();
            elapsed = Time.Zero;
            buffer = new Time();
            speed = 1;
        }
        /// <summary>
        /// Constructor. Used to set relative to another Chronometer. The main timer should not change its elapsed time to a lower value, as it may set a negative value to its childs.
        /// </summary>
        /// <param name="timer">Relative to.</param>
        public Chronometer(Chronometer timer) : this()
        {
            oldTime = timer.ElapsedTime;
            referenceTime = timer;
        }
        private void Update()
        {
            buffer = referenceTime.ElapsedTime - oldTime;
            oldTime = referenceTime.ElapsedTime;
            if (!Paused)
                elapsed += buffer * Speed;
        }
        /// <summary>
        /// Restarts the chronometer.
        /// </summary>
        public void Restart()
        {
            Update();
            elapsed = Time.Zero;
        }
        public static implicit operator Chronometer(Clock chrono) => new Chronometer(chrono);
    }
}
