using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Custom function.
    /// </summary>
    public class Function : IFunction
    {
        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public Function()
        {
            Fct = null;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fct">Function used.</param>
        public Function(Func<float, float> fct)
        {
            Fct = fct;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Function called when trying to get the Image.
        /// </summary>
        public Func<float, float> Fct { get; set; }

        #endregion Public Properties

        #region Public Methods

        public float Image(float antecedent) => Fct == null ? throw new InvalidOperationException("Fct is not set") : Fct(antecedent);

        #endregion Public Methods
    }
}