using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// A basic pow function.
    /// </summary>
    public class PowFunction : IFunction
    {
        /// <summary>
        /// The exponent of the pow function.
        /// </summary>
        /// <value>Exponent.</value>
        public float Exponent { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="expo">Exponent. (Optional)</param>
        public PowFunction(float expo = 1)
        {
            Exponent = expo;
        }
        /// <summary>
        /// Returns the image of the function.
        /// </summary>
        /// <param name="antecedent">Antecedent. Must be between [0,1].</param>
        /// <returns></returns>
        public float Image(float antecedent)
        {
            return (float)Math.Pow(antecedent, Exponent);
        }
    }
}
