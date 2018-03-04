using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// A basic linear function.
    /// </summary>
    public class LinearFunction : IFunction
    {
        /// <summary>
        /// Returns the image of the function.
        /// </summary>
        /// <param name="antecedent">Antecedent. Must be between [0,1].</param>
        /// <returns></returns>
        public float Image(float antecedent)
        {
            return antecedent;
        }
    }
}
