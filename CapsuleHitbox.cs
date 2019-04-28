using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// A capsule shaped hitbox.
    /// </summary>
    public class CapsuleHitbox : SingleShapeHitbox
    {
        #region Private Fields

        private Vector _halfExtend;
        private int _numberOfVerticesPerCorner;
        private double _radius;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public CapsuleHitbox() : base()
        {
            _halfExtend = new Vector();
            _radius = 0;
            NumberOfVerticesPerCorner = 5;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy"></param>
        public CapsuleHitbox(CapsuleHitbox copy) : this()
        {
            _halfExtend = copy.HalfExtend;
            _radius = copy.Radius;
            NumberOfVerticesPerCorner = copy.NumberOfVerticesPerCorner;
            Position = copy.Position;
            Origin = copy.Origin;
            Scale = copy.Scale;
            Rotation = copy.Rotation;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The half of the size of the rectangle.
        /// </summary>
        public Vector HalfExtend
        {
            get => _halfExtend;
            set
            {
                _halfExtend = value;
                Update();
            }
        }

        /// <summary>
        /// The number of vertices per corner. A higher value means more precise collisions but at a
        /// higher cost of performances.
        /// </summary>
        public int NumberOfVerticesPerCorner
        {
            get => _numberOfVerticesPerCorner;
            set
            {
                _numberOfVerticesPerCorner = value;
                Update();
            }
        }

        /// <summary>
        /// The radius of the corners.
        /// </summary>
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Update();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override object Clone() => new CapsuleHitbox(this);

        #endregion Public Methods

        #region Private Methods

        private void Update()
        {
            Vertices.Clear();
            var result = new List<Point>();
            double usedRadius = Utilities.Min(Radius, HalfExtend.X, HalfExtend.Y);
            for (int i = 0; i < NumberOfVerticesPerCorner; i++)
            {
                var vec = new Point(Utilities.Max(0, HalfExtend.X - usedRadius), -HalfExtend.Y + usedRadius);
                var secVec = new Vector(0, -usedRadius);
                secVec.Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                result.Add(vec + secVec);
            }
            for (int i = 0; i < NumberOfVerticesPerCorner; i++)
            {
                var vec = new Point(Utilities.Max(0, HalfExtend.X - usedRadius), HalfExtend.Y - usedRadius);
                var secVec = new Vector(usedRadius, 0);
                secVec.Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                result.Add(vec);
            }
            for (int i = 0; i < NumberOfVerticesPerCorner; i++)
            {
                var vec = new Point(Utilities.Min(0, -HalfExtend.X + usedRadius), HalfExtend.Y - usedRadius);
                var secVec = new Vector(0, usedRadius);
                secVec.Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                result.Add(vec);
            }
            for (int i = 0; i < NumberOfVerticesPerCorner; i++)
            {
                var vec = new Point(Utilities.Min(0, -HalfExtend.X + usedRadius), -HalfExtend.Y + usedRadius);
                var secVec = new Vector(-usedRadius, 0);
                secVec.Rotate(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVerticesPerCorner - 1), 0f, 90)));
                result.Add(vec);
            }
            Vertices.Add(new Tuple<List<Point>, CombineMode>(result, CombineMode.ADD));
        }

        #endregion Private Methods
    }
}