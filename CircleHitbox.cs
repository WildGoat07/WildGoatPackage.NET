using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// A circle shaped hitbox.
    /// </summary>
    public class CircleHitbox : SingleShapeHitbox
    {
        #region Private Fields

        private int _numberOfVertices;
        private double _radius;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public CircleHitbox() : base()
        {
            _radius = 0;
            NumberOfVertices = 20;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy"></param>
        public CircleHitbox(CircleHitbox copy) : this()
        {
            _numberOfVertices = copy.NumberOfVertices;
            Radius = copy.Radius;
            Position = copy.Position;
            Origin = copy.Origin;
            Scale = copy.Scale;
            Rotation = copy.Rotation;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The number of vertices of the circle. More vertices means more precision but at a higher
        /// cost of performances. 20 by Default.
        /// </summary>
        public int NumberOfVertices
        {
            get => _numberOfVertices;
            set
            {
                _numberOfVertices = Utilities.Max(0, value);
                Update();
            }
        }

        /// <summary>
        /// The radius of the circle.
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

        public override object Clone() => new CircleHitbox(this);

        #endregion Public Methods

        #region Private Methods

        private void Update()
        {
            Vertices.Clear();
            var list = new List<Point>();
            for (int i = 0; i < NumberOfVertices; i++)
            {
                var pt = new Vector(Radius, 0);
                pt.SetAngle(Angle.FromDegrees(Utilities.Interpolation(Utilities.Percent(i, 0, NumberOfVertices), 0f, 360f)));
                list.Add(pt + new Point());
            }
            Vertices.Add(new Tuple<List<Point>, CombineMode>(list, CombineMode.ADD));
        }

        #endregion Private Methods
    }
}