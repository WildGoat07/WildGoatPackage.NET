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
    /// A capsule shaped hitbox.
    /// </summary>
    public class CapsuleHitbox : Transformable, IHitbox
    {
        public IEnumerable<Vector2f> Vertices
        {
            get
            {
                var result = new Vector2f[4 * NumberOfVerticesPerCorner];
                int index = 0;
                float usedRadius = Utilities.Min(Radius, HalfExtend.X, HalfExtend.Y);
                for(int i = 0;i<NumberOfVerticesPerCorner;i++)
                {
                    var vec = new Vector2f(Utilities.Max(0, HalfExtend.X - usedRadius), -HalfExtend.Y + usedRadius);
                    vec += new Vector2f(0, -usedRadius).Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                    result[index] = Transform.TransformPoint(vec);
                    index++;
                }
                for(int i = 0;i<NumberOfVerticesPerCorner;i++)
                {
                    var vec = new Vector2f(Utilities.Max(0, HalfExtend.X - usedRadius), HalfExtend.Y - usedRadius);
                    vec += new Vector2f(usedRadius, 0).Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                    result[index] = Transform.TransformPoint(vec);
                    index++;
                }
                for(int i = 0;i<NumberOfVerticesPerCorner;i++)
                {
                    var vec = new Vector2f(Utilities.Min(0, -HalfExtend.X + usedRadius), HalfExtend.Y - usedRadius);
                    vec += new Vector2f(0, usedRadius).Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                    result[index] = Transform.TransformPoint(vec);
                    index++;
                }
                for(int i = 0;i<NumberOfVerticesPerCorner;i++)
                {
                    var vec = new Vector2f(Utilities.Min(0, -HalfExtend.X + usedRadius), -HalfExtend.Y + usedRadius);
                    vec += new Vector2f(-usedRadius, 0).Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                    result[index] = Transform.TransformPoint(vec);
                    index++;
                }
                return result;
            }
        }
        /// <summary>
        /// The half of the size of the rectangle (how much it will extend from the middle).
        /// </summary>
        public Vector2f HalfExtend { get; set; }
        /// <summary>
        /// The radius of the corners.
        /// </summary>
        public float Radius { get; set; }
        /// <summary>
        /// The number of vertices per corner. A higher value means more precise collisions but at a higher cost of performances.
        /// </summary>
        public int NumberOfVerticesPerCorner { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public CapsuleHitbox()
        {
            HalfExtend = new Vector2f();
            Radius = 0;
            NumberOfVerticesPerCorner = 5;
        }
    }
}
