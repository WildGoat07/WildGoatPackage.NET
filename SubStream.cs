using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// A stream that can only read a portion of his base stream.
    /// </summary>
    public class SubStream : Stream
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sub">Base stream.</param>
        /// <param name="maxLength">Max length of the stream.</param>
        /// <param name="offset">Offset of the stream.</param>
        /// <param name="resetPosition">True if the sub stream set its Position to 0.</param>
        public SubStream(Stream sub, long maxLength = -1, long offset = 0, bool resetPosition = true)
        {
            BaseStream = sub;
            if (maxLength < 0)
                _maxLength = sub.Length;
            else
                _maxLength = maxLength;
            _startOffset = offset;
            if (resetPosition)
                Position = 0;
        }
        /// <summary>
        /// Base stream to read from.
        /// </summary>
        public Stream BaseStream { get; }
        private long _maxLength;
        private long _startOffset;
        /// <summary>
        /// The max length of the portion.
        /// </summary>
        public long MaxLength
        {
            get => _maxLength;
            set => _maxLength = Utilities.Max(0, value);
        }
        /// <summary>
        /// The offset of the position of the portion.
        /// </summary>
        public long StartOffset
        {
            get => _startOffset;
            set => _startOffset = value.Capped(0, BaseStream.Length);
        }
        public override bool CanRead => BaseStream.CanRead;

        public override bool CanSeek => BaseStream.CanSeek;

        public override bool CanWrite => false;

        public override long Length => Utilities.Min(MaxLength, BaseStream.Length - StartOffset);

        public override long Position { get => BaseStream.Position - StartOffset; set => BaseStream.Position = value.Capped(0, Length) + StartOffset; }

        public override void Flush()
        {
            BaseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = count.Capped(0, (int)(Length - Position));
            return BaseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    return Position;
                case SeekOrigin.Current:
                    Position += offset;
                    return Position;
                case SeekOrigin.End:
                    Position = Length + offset;
                    return Position;
                default: return 0;
            }
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
