using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Interface for functions.
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Returns the image of the function.
        /// </summary>
        /// <param name="antecedent">Antecedent. Must be between [0,1].</param>
        /// <returns></returns>
        float Image(float antecedent);
    }
}
