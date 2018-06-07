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
        private dynamic ReferenceTime { get; set; }
        private Time Elapsed { get; set; }
        private float speed;
        private Time Buffer { get; set; }
        private Time OldTime { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Chronometer()
        {
            OldTime = Time.Zero;
            ReferenceTime = new Clock();
            Elapsed = SFML.System.Time.Zero;
            Buffer = new Time();
            speed = 1;
        }
        /// <summary>
        /// Constructor. Used to set relative to another Chronometer.
        /// </summary>
        /// <param name="timer">Relative to.</param>
        public Chronometer(Chronometer timer) : this()
        {
            OldTime = timer.ElapsedTime;
            ReferenceTime = timer;
            Elapsed = SFML.System.Time.Zero;
            Buffer = new Time();
            speed = 1;
        }
        /// <summary>
        /// Change the speed. The speed is the factor by which is multiplied the elapsed time.
        /// </summary>
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                Update();
                speed = value;
            }
        }
        private void Update()
        {
            Buffer = ReferenceTime.ElapsedTime - OldTime;
            OldTime = ReferenceTime.ElapsedTime;
            Elapsed += Buffer * speed;
        }
        /// <summary>
        /// The current elapsed time.
        /// </summary>
        public Time ElapsedTime
        {
            get
            {
                Update();
                return Elapsed;
            }
            set
            {
                Update();
                Elapsed = value;
            }
        }
        /// <summary>
        /// Restarts the chronometer.
        /// </summary>
        public void Restart()
        {
            Update();
            Elapsed = SFML.System.Time.Zero;
        }
    }
}
