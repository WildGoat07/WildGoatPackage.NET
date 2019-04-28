using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// Line class. Used in arithmetics.
    /// </summary>
    public class Line
    {
        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Line()
        {
            Position = new Point();
            Direction = new Angle();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy">Line to copy.</param>
        public Line(Line copy) : this()
        {
            Position = copy.Position;
            Direction = copy.Direction;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pt">Point of the line.</param>
        /// <param name="angle">Angle of the line.</param>
        public Line(Point pt, Angle angle) : base()
        {
            Position = pt;
            Direction = angle;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pt1">First point of the line.</param>
        /// <param name="pt2">A point of the line different than <paramref name="pt1"/>.</param>
        public Line(Point pt1, Point pt2) : base()
        {
            Position = pt1;
            Direction = (pt2 - pt1).GetAngle();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The direction of the line. Always normalized.
        /// </summary>
        public Angle Direction { get; set; }

        /// <summary>
        /// A point of the line.
        /// </summary>
        public Point Position { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Creates a segment from the line.
        /// </summary>
        /// <param name="length">Length of the segment.</param>
        /// <returns>Created segment from the line.</returns>
        public Segment AsSegment(float length)
        {
            return new Segment(Position, Direction, length);
        }

        /// <summary>
        /// Returns a point of the line.
        /// </summary>
        /// <param name="t">The scalar parameter.</param>
        /// <returns>Point equal to : "Position + t * Direction".</returns>
        public Point GetPoint(double t)
        {
            return Position + t * Direction.GenerateVector();
        }

        #endregion Public Methods
    }
}