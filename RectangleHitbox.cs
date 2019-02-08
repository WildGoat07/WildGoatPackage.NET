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
    public class RectangleHitbox : SingleShapeHitbox
    {
        #region Private Fields

        private Vector2f _halfExtend;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RectangleHitbox() : base()
        {
            HalfExtend = new Vector2f();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="copy"></param>
        public RectangleHitbox(RectangleHitbox copy) : this()
        {
            HalfExtend = new Vector2f();
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
        public Vector2f HalfExtend
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
            var result = new List<Vector2f>();
            result.Add(-HalfExtend);
            result.Add(HalfExtend.OnlyX() - HalfExtend.OnlyY());
            result.Add(HalfExtend);
            result.Add(HalfExtend.OnlyY() - HalfExtend.OnlyX());
            Vertices.Add(new Tuple<List<Vector2f>, CombineMode>(result, CombineMode.ADD));
        }

        #endregion Private Methods
    }
}