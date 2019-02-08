using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WGP
{
    /// <summary>
    /// A memory stream that can insert data anywhere in its memory or remove data anywhere in its memory.
    /// </summary>
    public class AdvancedMemoryStream : Stream
    {
        #region Private Fields

        private long _position;
        private List<byte> data;
        private bool disposed;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public AdvancedMemoryStream()
        {
            _position = 0;
            data = new List<byte>();
            disposed = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="size">Size of the memory to allocate.</param>
        public AdvancedMemoryStream(int size) : base()
        {
            SetLength(size);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bytes">Initial buffer of the stream.</param>
        public AdvancedMemoryStream(byte[] bytes) : base()
        {
            data = new List<byte>(bytes);
        }

        #endregion Public Constructors

        #region Public Properties

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        public override long Length
        {
            get
            {
                if (disposed)
                    throw new ObjectDisposedException(ToString());
                return data.Count;
            }
        }

        public override long Position
        {
            get
            {
                if (disposed)
                    throw new ObjectDisposedException(ToString());
                return _position;
            }
            set
            {
                if (disposed)
                    throw new ObjectDisposedException(ToString());
                _position = value.Capped(0, Length);
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Flush()
        {
        }

        /// <summary>
        /// Insert a chunk of data in the memory.
        /// </summary>
        /// <param name="buffer">Data to insert.</param>
        /// <param name="offset">Position in the buffer to which the insertion begins.</param>
        /// <param name="count">The number of byte to insert.</param>
        public void Insert(byte[] buffer, int offset, int count)
        {
            if (disposed)
                throw new ObjectDisposedException(ToString());
            if (buffer == null)
                throw new ArgumentNullException();
            if (buffer.Length < offset + count)
                throw new ArgumentException();
            if (offset < 0 || count < 0)
                throw new IndexOutOfRangeException();
            if (count == 0)
                return;
            for (int i = offset; i < offset + count; i++)
            {
                data.Insert((int)Position, buffer[i]);
                Position++;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (disposed)
                throw new ObjectDisposedException(ToString());
            count = count.Capped(0, (int)(Length - Position));
            if (buffer == null)
                throw new ArgumentNullException();
            if (buffer.Length < offset + count)
                throw new ArgumentException();
            if (offset < 0 || count < 0)
                throw new IndexOutOfRangeException();
            if (count == 0)
                return 0;
            data.CopyTo((int)Position, buffer, offset, count);
            Position += count;
            return count;
        }

        /// <summary>
        /// Remove a portion of the memory.
        /// </summary>
        /// <param name="count">Number of bytes to remove.</param>
        public void Remove(int count)
        {
            if (disposed)
                throw new ObjectDisposedException(ToString());
            if (count > Length - Position)
                throw new ArgumentException();
            data.RemoveRange((int)Position, count);
            if (Position == Length)
                Position--;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (disposed)
                throw new ObjectDisposedException(ToString());
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;

                case SeekOrigin.Current:
                    Position += offset;
                    break;

                case SeekOrigin.End:
                    Position = Length + offset;
                    break;
            }
            return Position;
        }

        public override void SetLength(long value)
        {
            if (disposed)
                throw new ObjectDisposedException(ToString());
            if (value > Length)
                data.AddRange(new byte[value - Length]);
            else if (value < Length)
            {
                data.RemoveRange((int)value, data.Count - (int)value);
                data.TrimExcess();
            }
            Position = Position;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (disposed)
                throw new ObjectDisposedException(ToString());
            if (buffer == null)
                throw new ArgumentNullException();
            if (buffer.Length < offset + count)
                throw new ArgumentException();
            if (offset < 0 || count < 0)
                throw new IndexOutOfRangeException();
            if (count == 0)
                return;
            int eraseCount = Utilities.Min((int)(Length - Position), count);
            int addCount = count - eraseCount;
            var erase = new byte[eraseCount];
            Array.Copy(buffer, 0, erase, 0, eraseCount);
            foreach (var item in erase)
            {
                data[(int)Position] = item;
                Position++;
            }
            if (addCount > 0)
            {
                var add = new byte[addCount];
                Array.Copy(buffer, eraseCount, add, 0, addCount);
                data.AddRange(add);
                Position += addCount;
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                data = null;
            }
            disposed = true;
            base.Dispose(disposing);
        }

        #endregion Protected Methods
    }
}