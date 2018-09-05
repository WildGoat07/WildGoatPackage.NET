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
    /// A rectangular hitbox.
    /// </summary>
    public class RectangleHitbox : Transformable, IHitbox
    {
        public IEnumerable<Vector2f> Vertices
        {
            get
            {
                Vector2f[] result = new Vector2f[4];
                result[0] = Transform.TransformPoint(-HalfExtend);
                result[1] = Transform.TransformPoint(HalfExtend.OnlyX() - HalfExtend.OnlyY());
                result[2] = Transform.TransformPoint(HalfExtend);
                result[3] = Transform.TransformPoint(HalfExtend.OnlyY() - HalfExtend.OnlyX());
                return result;
            }
        }
        /// <summary>
        /// The half of the size of the rectangle (how much it will extend from the middle).
        /// </summary>
        public Vector2f HalfExtend { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public RectangleHitbox() { HalfExtend = new Vector2f(); }
    }
}
