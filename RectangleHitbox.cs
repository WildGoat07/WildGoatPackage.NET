using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WGP
{
    /// <summary>
    /// A rectangular hitbox.
    /// </summary>
    public class RectangleHitbox : SingleShapeHitbox
    {
        #region Private Fields

        private Vector _halfExtend;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RectangleHitbox() : base()
        {
            HalfExtend = new Vector();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy"></param>
        public RectangleHitbox(RectangleHitbox copy) : this()
        {
            HalfExtend = new Vector();
            Position = copy.Position;
            Origin = copy.Origin;
            Scale = copy.Scale;
            Rotation = copy.Rotation;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The half of the size of the rectangle (how much it will extend from the middle).
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

        #endregion Public Properties

        #region Public Methods

        public override object Clone() => new RectangleHitbox(this);

        #endregion Public Methods

        #region Private Methods

        private void Update()
        {
            Vertices.Clear();
            var result = new List<Point>();
            result.Add(new Point() - HalfExtend);
            result.Add(new Point() + HalfExtend.OnlyX() - HalfExtend.OnlyY());
            result.Add(new Point() + HalfExtend);
            result.Add(new Point() + HalfExtend.OnlyY() - HalfExtend.OnlyX());
            Vertices.Add(new Tuple<List<Point>, CombineMode>(result, CombineMode.ADD));
        }

        #endregion Private Methods
    }
}