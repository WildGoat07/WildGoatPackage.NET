using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    public class ProgressiveFunction : IFunction
    {
        public float Roughness { get; set; }
        public ProgressiveFunction(float roughness = 1)
        {
            Roughness = roughness;
        }
        public float Image(float antecedent)
        {
            if (antecedent <= 0.5)
                return new PowFunction(Roughness).Image(antecedent * 2) / 2;
            else
                return (1 - new PowFunction(Roughness).Image(Utilities.Percent(antecedent, 1, 0.5f))) / 2 + 0.5f;
        }
    }
}
