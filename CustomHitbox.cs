using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// A custom shape for a hitbox. Can be convex or concave.
    /// </summary>
    public class CustomHitbox : SingleShapeHitbox
    {
        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomHitbox() : base()
        {
            Vertices.Add(new Tuple<List<Point>, CombineMode>(new List<Point>(), CombineMode.ADD));
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy"></param>
        public CustomHitbox(CustomHitbox copy) : base(copy)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// List of points composing the shape.
        /// </summary>
        public List<Point> Shape => Vertices.First().Item1;

        #endregion Public Properties

        #region Public Methods

        public override object Clone() => new CustomHitbox(this);

        #endregion Public Methods
    }
}