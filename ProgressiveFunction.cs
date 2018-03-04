using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// The progressvie function is a combination of 2 pow function (first normal, the second inverted) to looks smooth.
    /// </summary>
    public class ProgressiveFunction : IFunction
    {
        /// <summary>
        /// The roughness is the exponent of the pow functions.
        /// </summary>
        /// <value>Exponent of the pow function.</value>
        public float Roughness { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roughness">Roughness of the function. (Optional)</param>
        public ProgressiveFunction(float roughness = 1)
        {
            Roughness = roughness;
        }
        /// <summary>
        /// Returns the image of the function.
        /// </summary>
        /// <param name="antecedent">Antecedent. Must be between [0,1].</param>
        /// <returns></returns>
        public float Image(float antecedent)
        {
            if (antecedent <= 0.5)
                return new PowFunction(Roughness).Image(antecedent * 2) / 2;
            else
                return (1 - new PowFunction(Roughness).Image(Utilities.Percent(antecedent, 1, 0.5f))) / 2 + 0.5f;
        }
    }
}
