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
    /// Base of any hitbox.
    /// </summary>
    public interface IHitbox
    {
        /// <summary>
        /// A group of ordered vertices composing the hitbox.
        /// </summary>
        IEnumerable<Vector2f> Vertices { get; }
    }
    public static partial class Extensions
    {
        /// <summary>
        /// Check the collision between two hitboxes.
        /// </summary>
        /// <param name="box1">First hitbox.</param>
        /// <param name="box2">Second hitbox.</param>
        /// <returns>True if there is a collision, false otherwise.</returns>
        public static bool Collision(this IHitbox box1, IHitbox box2) => Collision(box1, box2, new Vector2f(-999999, -999999));
        /// <summary>
        /// Check the collision between two hitboxes.
        /// </summary>
        /// <param name="box1">First hitbox.</param>
        /// <param name="box2">Second hitbox.</param>
        /// <param name="infinitePt">A point that can not be in any hitbox in any way.</param>
        /// <returns>True if there is a collision, false otherwise.</returns>
        public static bool Collision(this IHitbox box1, IHitbox box2, Vector2f infinitePt)
        {
            var vert1 = box1.Vertices;
            var vert2 = box2.Vertices;
            if (!Utilities.CreateRect(vert1.ToArray()).Intersects(Utilities.CreateRect(vert2.ToArray())))
                return false;
            List<Segment> list1 = new List<Segment>();
            List<Segment> list2 = new List<Segment>();

            {
                var tmp = vert1.ToArray();
                Vector2f old = tmp.First();
                for (int i = 1;i<=tmp.Length;i++)
                {
                    list1.Add(new Segment(old, tmp[i % tmp.Length]));
                    old = tmp[i % tmp.Length];
                }
            }
            {
                var tmp = vert2.ToArray();
                Vector2f old = tmp.First();
                for (int i = 1;i<=tmp.Length;i++)
                {
                    list2.Add(new Segment(old, tmp[i % tmp.Length]));
                    old = tmp[i % tmp.Length];
                }
            }
            {
                var pt = vert2.First();
                int count = 0;
                foreach (var seg in list1)
                {
                    if (new Segment(pt, infinitePt).Collision(seg))
                        count++;
                }
                if (count % 2 == 1)
                    return true;
            }
            {
                var pt = vert1.First();
                int count = 0;
                foreach (var seg in list2)
                {
                    if (new Segment(pt, infinitePt).Collision(seg))
                        count++;
                }
                if (count % 2 == 1)
                    return true;
            }
            foreach (var seg1 in list1)
            {
                foreach (var seg2 in list2)
                {
                    if (seg1.Collision(seg2))
                        return true;
                }
            }
            return false;
        }
    }
}
