using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Compiles data from a stream.
    /// </summary>
    public class DataCompiler : Dictionary<string, DataCompiler.StreamOptions>
    {
        /// <summary>
        /// Struct containing informations when extracting data from a stream.
        /// </summary>
        public struct StreamOptions
        {
            /// <summary>
            /// Input stream.
            /// </summary>
            public Stream Stream;
            /// <summary>
            /// Length of the part of the stream to read.
            /// </summary>
            public long Length;
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="stream">Input stream</param>
            /// <param name="length">Length of the part to read set to -1 to read to the end of the stream. The output stream must handle seeking if set to -1.</param>
            public StreamOptions(Stream stream, long length = -1)
            {
                Stream = stream;
                Length = length;
            }
        }
        public DataCompiler() : base() { }
        public DataCompiler(int capacity) : base(capacity) { }
        public DataCompiler(IEqualityComparer<string> comparer) : base(comparer) { }
        public DataCompiler(int capacity, IEqualityComparer<string> comparer) : base(capacity, comparer) { }
        public DataCompiler(IDictionary<string, StreamOptions> dictionary) : base(dictionary) { }
        public DataCompiler(IDictionary<string, StreamOptions> dictionary, IEqualityComparer<string> comparer) : base(dictionary, comparer) { }
        public DataCompiler(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)  { }
        public void Add(string key, Stream stream) => Add(key, new StreamOptions(stream));
        /// <summary>
        /// Writes all the given streams into the given ouptut stream.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        public void WriteTo(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanWrite)
                throw new Exception("Can't write in this stream");
            try
            {
                stream.WriteInt32(Count);
                foreach (var item in this)
                {
                    {
                        stream.WriteInt32(item.Key.Length);
                        stream.WriteString(item.Key);
                    }
                    if (item.Value.Length >= 0)
                    {
                        stream.WriteInt64(item.Value.Length);
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            stream.WriteUInt8(item.Value.Stream.ReadUInt8());
                        }
                    }
                    else
                    {
                        byte[] b = new byte[1];
                        long size = 0;
                        long sizePos = stream.Position;
                        stream.WriteInt64(0);
                        while (item.Value.Stream.Read(b, 0, 1) == 1)
                        {
                            stream.Write(b, 0, 1);
                            size++;
                        }
                        long curr = stream.Position;
                        stream.Position = sizePos;
                        stream.WriteInt64(size);
                        stream.Position = curr;
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("Unable to write data", e);
            }
        }
    }
    /// <summary>
    /// Extract files made with the FileCompiler.
    /// </summary>
    public class DataExtracter : Dictionary<string, Stream>
    {
        internal class CompiledDataStream : Stream
        {
            internal Stream Origin { get; set; }
            internal long Offset { get; set; }
            internal long Size { get; set; }
            private long position { get; set; }

            internal CompiledDataStream() { }

            public override bool CanRead => true;

            public override bool CanSeek => true;

            public override bool CanWrite => false;

            public override long Length => Size;

            public override long Position
            {
                get => position;
                set
                {
                    if (value > Length || value < 0)
                        throw new IndexOutOfRangeException("The position is out of the stream size.");
                    position = value;
                }
            }

            public override void Flush()
            {
                Origin.Flush();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                if (count + Position > Length)
                    count = (int)(Length - Position);
                Origin.Position = Offset + Position;
                int result = Origin.Read(buffer, offset, count);
                Position += result;
                return result;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                if (origin == SeekOrigin.Begin)
                    Position = offset.Capped(0, Length - 1);
                if (origin == SeekOrigin.Current)
                    Position = (offset + position).Capped(0, Length - 1);
                if (origin == SeekOrigin.End)
                    Position = (offset + Length - 1).Capped(0, Length - 1);
                return Position;

            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (disposing)
                    Origin.Dispose();
            }
        }

        public DataExtracter() : base() { }
        public DataExtracter(int capacity) : base(capacity) { }
        public DataExtracter(IEqualityComparer<string> comparer) : base(comparer) { }
        public DataExtracter(int capacity, IEqualityComparer<string> comparer) : base(capacity, comparer) { }
        public DataExtracter(IDictionary<string, Stream> dictionary) : base(dictionary) { }
        public DataExtracter(IDictionary<string, Stream> dictionary, IEqualityComparer<string> comparer) : base(dictionary, comparer) { }
        public DataExtracter(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Reads from the given stream to extract the internal streams containing the data. The stream must not be closed or modified if you want to use the extracted streams (they are actually all linked to the given stream).
        /// </summary>
        /// <param name="stream">Input stream.</param>
        public void ReadFrom(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new Exception("Can't read from this stream");
            if (!stream.CanSeek)
                throw new Exception("Can't seek from this stream");
            Clear();
            try
            {
                int size = stream.ReadInt32();
                for (int i = 0;i<size;i++)
                {
                    string id;
                    int strSize = stream.ReadInt32();
                    id = stream.ReadString((uint)strSize);
                    CompiledDataStream tmpStream = new CompiledDataStream();
                    tmpStream.Origin = stream;
                    tmpStream.Size = stream.ReadInt64();
                    tmpStream.Offset = stream.Position;
                    stream.Seek(tmpStream.Size, SeekOrigin.Current);
                    Add(id, tmpStream);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to read data", e);
            }
        }

    }
}
