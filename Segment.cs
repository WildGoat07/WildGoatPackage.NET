using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// The segment is a line that is limited in length.
    /// </summary>
    public class Segment : Line
    {
        #region Public Constructors

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
        /// <param name="pt">First point of the segment.</param>
        /// <param name="angle">Last point of the segment.</param>
        /// <param name="length">Length of the segment.</param>
        public Segment(Point pt, Angle angle, double length) : base(pt, angle)
        {
            Length = length;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pt1">First point of the segment.</param>
        /// <param name="pt2">Last point of the segment.</param>
        public Segment(Point pt1, Point pt2) : base(pt1, pt2)
        {
            Length = (pt2 - pt1).Length;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Length of the segment.
        /// </summary>
        public double Length { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates a line from the segment.
        /// </summary>
        /// <returns>Line created from the segment.</returns>
        public Line AsLine()
        {
            return new Line(Position, Direction);
        }

        #endregion Public Methods
    }
}