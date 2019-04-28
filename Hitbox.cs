using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WGP
{
    /// <summary>
    /// Base of any hitbox.
    /// </summary>
    public class Hitbox : ICloneable
    {
        #region Protected Internal Fields

        /// <summary>
        /// A group of ordered vertices composing the hitbox.
        /// </summary>
        protected internal List<Tuple<List<Point>, CombineMode>> Vertices;

        #endregion Protected Internal Fields

        #region Public Constructors

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy"></param>
        public Hitbox(Hitbox copy)
        {
            Vertices = ((Hitbox)copy.Clone()).Vertices;
            Position = copy.Position;
            Origin = copy.Origin;
            Scale = copy.Scale;
            Rotation = copy.Rotation;
        }

        #endregion Public Constructors

        #region Protected Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        protected Hitbox()
        {
            Vertices = new List<Tuple<List<Point>, CombineMode>>();
            Position = new Point();
            Origin = new Point();
            Scale = new Vector(1, 1);
            Rotation = 0;
        }

        #endregion Protected Constructors

        #region Public Enums

        /// <summary>
        /// Describe how the right hitbox will be added in the left hitbox of the Combine() method.
        /// </summary>
        public enum CombineMode
        {
            /// <summary>
            /// The default combine mode copy the current mode (ADD for single shape hitboxes, for
            /// multiple shape it depends on the previous calls of Combine() )
            /// </summary>
            DEFAULT,

            /// <summary>
            /// Adds the added shapes regardless of their current mode
            /// </summary>
            ADD,

            /// <summary>
            /// Removes the added shapes regardless of their current mode
            /// </summary>
            REMOVE,

            /// <summary>
            /// Multiply the added shapes regardless of their current mode
            /// </summary>
            INTERSECT
        }

        #endregion Public Enums

        #region Public Properties

        public Matrix InverseTransform
        {
            get
            {
                var res = Transform;
                res.Invert();
                return res;
            }
        }

        public Point Origin { get; set; }
        public Point Position { get; set; }
        public double Rotation { get; set; }
        public Vector Scale { get; set; }

        public Matrix Transform
        {
            get
            {
                var res = Matrix.Identity;
                res.Translate(Position.X, Position.Y);
                res.Rotate(Rotation);
                res.Scale(Scale.X, Scale.Y);
                res.Translate(-Origin.X, -Origin.Y);
                return res;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Create a new combined hitbox containing multiple shapes from other created hitboxes.
        /// </summary>
        /// <param name="hitbox1">Base hitbox.</param>
        /// <param name="hitbox2">Other hitbox.</param>
        /// <param name="combine">The way the other hitbox will be computed.</param>
        /// <returns>Combined hitbox.</returns>
        public static Hitbox Combine(Hitbox hitbox1, Hitbox hitbox2, CombineMode combine = CombineMode.DEFAULT)
        {
            hitbox1 = hitbox1.TransformHitbox();
            hitbox2 = hitbox2.TransformHitbox();
            Hitbox result = new Hitbox();
            foreach (var v in hitbox1.Vertices)
                result.Vertices.Add(new Tuple<List<Point>, CombineMode>(v.Item1, v.Item2));
            foreach (var v in hitbox2.Vertices)
            {
                if (combine == CombineMode.DEFAULT)
                    result.Vertices.Add(new Tuple<List<Point>, CombineMode>(v.Item1, v.Item2));
                else
                    result.Vertices.Add(new Tuple<List<Point>, CombineMode>(v.Item1, combine));
            }
            return result;
        }

        public static Hitbox operator -(Hitbox left, Hitbox right) => Combine(left, right, CombineMode.REMOVE);

        public static Hitbox operator *(Hitbox left, Hitbox right) => Combine(left, right, CombineMode.INTERSECT);

        public static Hitbox operator +(Hitbox left, Hitbox right) => Combine(left, right, CombineMode.ADD);

        public virtual object Clone()
        {
            Hitbox result = new Hitbox();
            result.Position = Position;
            result.Rotation = Rotation;
            result.Scale = Scale;
            result.Origin = Origin;
            foreach (var shape in Vertices)
            {
                var list = new List<Point>();
                foreach (var pt in shape.Item1)
                    list.Add(pt);
                result.Vertices.Add(new Tuple<List<Point>, CombineMode>(list, shape.Item2));
            }
            return result;
        }

        /// <summary>
        /// Check the collision between the hitbox and a point.
        /// </summary>
        /// <param name="pt">Point.</param>
        /// <returns>True if there is a collision, false otherwise.</returns>
        public bool Collision(Point pt) => Collision(pt, new Point(-999999, -999999));

        /// <summary>
        /// Check the collision between the hitbox and a point.
        /// </summary>
        /// <param name="pt">Point.</param>
        /// <param name="infinitePt">A point that can not be in the hitbox in any way.</param>
        /// <returns>True if there is a collision, false otherwise.</returns>
        public bool Collision(Point pt, Point infinitePt)
        {
            pt = InverseTransform.Transform(pt);
            bool finalHit = false;
            foreach (var shape in Vertices)
            {
                bool currentHit = false;

                List<Segment> list = new List<Segment>();
                {
                    var tmp = shape.Item1.ToArray();
                    Point old = tmp.First();
                    for (int i = 1; i <= tmp.Length; i++)
                    {
                        list.Add(new Segment(old, tmp[i % tmp.Length]));
                        old = tmp[i % tmp.Length];
                    }
                }
                int hit = 0;
                foreach (var seg in list)
                {
                    if (seg.Collision(new Segment(infinitePt, pt)))
                        hit++;
                }
                currentHit = hit % 2 == 1;

                if (currentHit)
                {
                    if (shape.Item2 == CombineMode.ADD)
                        finalHit = true;
                    else if (shape.Item2 == CombineMode.REMOVE)
                        finalHit = false;
                    else if (shape.Item2 == CombineMode.INTERSECT)
                        finalHit = !finalHit;
                }
            }
            return finalHit;
        }

        /// <summary>
        /// Check the collision between two hitboxes. May be very heavy for complexe hitboxes in
        /// certain cases.
        /// </summary>
        /// <param name="box2">Second hitbox.</param>
        /// <returns>True if there is a collision, false otherwise.</returns>
        public bool Collision(Hitbox box2) => Collision(box2, new Point(-999999, -999999));

        /// <summary>
        /// Check the collision between two hitboxes. May be very heavy for complexe hitboxes in
        /// certain cases.
        /// </summary>
        /// <param name="box2">Second hitbox.</param>
        /// <param name="infinitePt">A point that can not be in any hitbox in any way.</param>
        /// <returns>True if there is a collision, false otherwise.</returns>
        public bool Collision(Hitbox box2, Point infinitePt)
        {
            Hitbox box1 = TransformHitbox();
            box2 = box2.TransformHitbox();
            foreach (var shape in box1.Vertices)
            {
                if (shape.Item2 != CombineMode.REMOVE)
                {
                    foreach (var pt in shape.Item1)
                    {
                        if (box2.Collision(pt))
                            return true;
                    }
                }
            }
            foreach (var shape in box2.Vertices)
            {
                if (shape.Item2 != CombineMode.REMOVE)
                {
                    foreach (var pt in shape.Item1)
                    {
                        if (box1.Collision(pt))
                            return true;
                    }
                }
            }
            List<Point> inters = new List<Point>();
            foreach (var shape1 in box1.Vertices)
            {
                foreach (var shape2 in box2.Vertices)
                {
                    var vert1 = shape1.Item1;
                    var vert2 = shape2.Item1;
                    List<Segment> list1 = new List<Segment>();
                    List<Segment> list2 = new List<Segment>();

                    {
                        var tmp = vert1.ToArray();
                        Point old = tmp.First();
                        for (int i = 1; i <= tmp.Length; i++)
                        {
                            list1.Add(new Segment(old, tmp[i % tmp.Length]));
                            old = tmp[i % tmp.Length];
                        }
                    }
                    {
                        var tmp = vert2.ToArray();
                        Point old = tmp.First();
                        for (int i = 1; i <= tmp.Length; i++)
                        {
                            list2.Add(new Segment(old, tmp[i % tmp.Length]));
                            old = tmp[i % tmp.Length];
                        }
                    }
                    foreach (var seg1 in list1)
                    {
                        foreach (var seg2 in list2)
                        {
                            if (seg1.Collision(seg2))
                                inters.Add(seg1.Intersection(seg2));
                        }
                    }
                }
            }
            foreach (var pt in inters)
            {
                if (box1.Collision(pt, infinitePt) && box2.Collision(pt, infinitePt))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns an Axis Aligned Bounding Box of the actual hitbox.
        /// </summary>
        /// <returns>AABB of the hitbox.</returns>
        public Rect GetAABB()
        {
            var box = TransformHitbox();
            var list = new List<Point>();
            foreach (var shape in box.Vertices)
            {
                if (shape.Item2 != CombineMode.REMOVE)
                    list.AddRange(shape.Item1);
            }
            return Utilities.CreateRect(list);
        }

        /// <summary>
        /// Applies a transformation and returns the transformed one.
        /// </summary>
        /// <param name="tr">Transformation to apply.</param>
        /// <returns>Transformed hitbox.</returns>
        public Hitbox TransformHitbox(Matrix tr)
        {
            var result = new Hitbox();

            foreach (var shape in Vertices)
            {
                var list = new List<Point>();
                foreach (var pt in shape.Item1)
                    list.Add(tr.Transform(pt));
                result.Vertices.Add(new Tuple<List<Point>, CombineMode>(list, shape.Item2));
            }

            return result;
        }

        /// <summary>
        /// Applies its transformation and returns the transformed one.
        /// </summary>
        /// <returns>Transformed hitbox.</returns>
        public Hitbox TransformHitbox() => TransformHitbox(Transform);

        #endregion Public Methods
    }
}