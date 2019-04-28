using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP.CHANNELS
{
    public class DoubleChannel : Channel<double> { }

    public class DoubleMasterChannel : MasterChannel<double> { }

    public class FloatChannel : Channel<float> { }

    public class FloatMasterChannel : MasterChannel<float> { }

    public class IntChannel : Channel<int> { }

    public class IntMasterChannel : MasterChannel<int> { }

    public class LongChannel : Channel<long> { }

    public class LongMasterChannel : MasterChannel<long> { }

    public class TransformChannel : Channel<SFML.Graphics.Transform> { }

    public class TransformMasterChannel : MasterChannel<SFML.Graphics.Transform> { }

    public class UIntChannel : Channel<uint> { }

    public class UIntMasterChannel : MasterChannel<uint> { }

    public class ULongChannel : Channel<ulong> { }

    public class ULongMasterChannel : MasterChannel<ulong> { }
}