using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP.CHANNELS
{
    /// <summary>
    /// A channel. It will transfer the master value to himself and his childs.
    /// </summary>
    /// <typeparam name="T">Type to share between channels.</typeparam>
    public class Channel<T> : Module<T>, IDisposable
    {
        public override T FinalValue
        {
            get
            {
                if (master == null)
                    throw new Exception("No master set");
                else if (Superior == null)
                    throw new Exception("No superior set");
                return master.CombineFunction(Superior, Value);
            }
        }
        internal void setMasterTo(MasterChannel<T> m)
        {
            master = m;
            foreach (var child in childs)
            {
                child.setMasterTo(m);
            }
        }
        /// <summary>
        /// Set the superior of this channel.
        /// </summary>
        /// <param name="superior">Channel or MasterChannel superior of this channel.</param>
        public void SetChildOf(Module<T> superior)
        {
            superior.AddChild(this);
        }
        /// <summary>
        /// Remove itself from its superior.
        /// </summary>
        public void RemoveFromSuperior()
        {
            Superior.RemoveChild(this);
        }

        public override void AddChild(Channel<T> chan)
        {
            childs.Add(chan);
            chan.setMasterTo(master);
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

        /// <summary>
        /// Constructor.
        /// </summary>
        public Channel() : base() { }
    }
}
