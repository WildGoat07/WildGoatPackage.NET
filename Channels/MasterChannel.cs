using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP.CHANNELS
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type to share between channels.</typeparam>
    public class MasterChannel<T> : Module<T>, IDisposable
    {
        /// <summary>
        /// The combining function will be called to transfer a value from the superior to the child.
        /// </summary>
        public Func<T, T, T> CombineFunction { get; set; }

        public override T FinalValue => Value;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MasterChannel() : base()
        {
            Value = default;
            CombineFunction = null;
            childs = new List<Channel<T>>();
        }
        public override void AddChild(Channel<T> chan)
        {
            childs.Add(chan);
            chan.setMasterTo(this);
            chan.Superior = this;
        }
        public override void RemoveChild(Channel<T> chan)
        {
            childs.Remove(chan);
            chan.Superior = null;
            chan.setMasterTo(null);
        }

        public void Dispose()
        {
            foreach (var child in childs)
            {
                child.Superior = null;
                child.setMasterTo(null);
            }
        }
    }
}
