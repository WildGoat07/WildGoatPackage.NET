using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace WGP
{
    /// <summary>
    /// The segment is a line that is limited in length.
    /// </summary>
    public class Segment : Line
    {
        /// <summary>
        /// Length of the segment.
        /// </summary>
        public float Length { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public Segment() : base()
        {

        }
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy">Segment to copy.</param>
        public Segment(Segment copy) : base(copy)
        {
            Length = copy.Length;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pt1">First point of the segment.</param>
        /// <param name="pt2">Last point of the segment.</param>
        public Segment(Vector2f pt1, Vector2f pt2) : base(pt1, pt2)
        {
            Length = (pt1 - pt2).Length();
        }
    }
}
