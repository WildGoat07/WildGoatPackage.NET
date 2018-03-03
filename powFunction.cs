using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    public class PowFunction : IFunction
    {
        public float Exponent { get; set; }

        public PowFunction(float expo = 1)
        {
            Exponent = expo;
        }

        public float Image(float antecedent)
        {
            return (float)Math.Pow(antecedent, Exponent);
        }
    }
}
