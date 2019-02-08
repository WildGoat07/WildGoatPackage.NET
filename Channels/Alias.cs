using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP.CHANNELS
{
    public class DoubleChannel : Channel<float> { }

    public class DoubleMasterChannel : MasterChannel<float> { }

    public class FloatChannel : Channel<float> { }

    public class FloatMasterChannel : MasterChannel<float> { }

    public class IntChannel : Channel<float> { }

    public class IntMasterChannel : MasterChannel<float> { }

    public class LongChannel : Channel<float> { }

    public class LongMasterChannel : MasterChannel<float> { }

    public class TransformChannel : Channel<SFML.Graphics.Transform> { }

    public class TransformMasterChannel : MasterChannel<SFML.Graphics.Transform> { }

    public class UIntChannel : Channel<float> { }

    public class UIntMasterChannel : MasterChannel<float> { }

    public class ULongChannel : Channel<float> { }

    public class ULongMasterChannel : MasterChannel<float> { }
}