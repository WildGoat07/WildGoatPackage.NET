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
    /// A circle shaped hitbox.
    /// </summary>
    public class CircleHitbox : Transformable, IHitbox
    {
        public IEnumerable<Vector2f> Vertices
        {
            get
            {
                Vector2f[] result = new Vector2f[NumberOfVertices];
                for(int i = 0;i<NumberOfVertices;i++)
                    result[i] = Transform.TransformPoint(new Vector2f(Radius, 0).SetAngle(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVertices), 0f, 360f))));
                return result;
            }
        }
        /// <summary>
        /// The radius of the circle.
        /// </summary>
        public float Radius { get; set; }
        /// <summary>
        /// The number of vertices of the circle. More vertices means more precision but at a higher cost of performances. 20 by Default.
        /// </summary>
        public int NumberOfVertices { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public CircleHitbox()
        {
            Radius = 0;
            NumberOfVertices = 20;
        }
    }
}
