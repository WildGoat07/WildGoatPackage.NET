using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace WGP
{
    /// <summary>
    /// Line class. Used in arithmetics.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// A point of the line.
        /// </summary>
        public Vector2f Position { get; set; }
        private Vector2f direction;
        /// <summary>
        /// The direction of the line. Always normalized.
        /// </summary>
        public Vector2f Direction
        {
            get => direction;
            set => direction = value.Normalize();
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public Line() { }
        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy">Line to copy.</param>
        public Line(Line copy) : this()
        {
            Position = copy.Position;
            direction = copy.Direction;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pt1">First point of the line.</param>
        /// <param name="pt2">A point of the line different than <paramref name="pt1"/>.</param>
        public Line(Vector2f pt1, Vector2f pt2) : base()
        {
            Position = pt1;
            Direction = pt2 - pt1;
        }
        /// <summary>
        /// Returns a point of the line.
        /// </summary>
        /// <param name="t">The scalar parameter.</param>
        /// <returns>Point equal to : "Position + t * Direction".</returns>
        public Vector2f GetPoint(float t)
        {
            return Position + t * Direction;
        }
    }
}
