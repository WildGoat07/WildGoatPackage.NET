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
    public class CustomHitbox : Transformable, IHitbox
    {
        public IEnumerable<Vector2f> Vertices
        {
            get
            {
                var result = new Vector2f[CustomShape.Count];
                int index = 0;
                foreach (var pt in CustomShape)
                {
                    result[index] = Transform.TransformPoint(pt);
                    index++;
                }
                return result;
            }
        }
        /// <summary>
        /// List of points composing the shape. DO NOT USE THE "Vertices" PROPERTY !
        /// </summary>
        public List<Vector2f> CustomShape { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomHitbox() { CustomShape = new List<Vector2f>(); }
    }
}
