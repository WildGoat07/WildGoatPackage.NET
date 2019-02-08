using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

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
            Vertices.Add(new Tuple<List<Vector2f>, CombineMode>(new List<Vector2f>(), CombineMode.ADD));
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
        public List<Vector2f> Shape => Vertices.First().Item1;

        #endregion Public Properties

        #region Public Methods

        public override object Clone() => new CustomHitbox(this);

        #endregion Public Methods
    }
}