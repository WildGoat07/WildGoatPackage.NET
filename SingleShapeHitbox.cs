using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Hitbox that has only one shape.
    /// </summary>
    public abstract class SingleShapeHitbox : Hitbox
    {
        #region Protected Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        protected SingleShapeHitbox() : base() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy"></param>
        protected SingleShapeHitbox(CustomHitbox copy) : base(copy)
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// An ordered list of points describing the shape of the hitbox.
        /// </summary>
        public SFML.System.Vector2f[] Points => Vertices.Count > 0 ? TransformHitbox().Vertices.First().Item1.ToArray() : null;

        #endregion Public Properties
    }
}