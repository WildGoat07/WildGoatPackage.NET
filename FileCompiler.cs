using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGP
{
    /// <summary>
    /// Compiles files into one.
    /// </summary>
    public class FileCompiler
    {
        /// <summary>
        /// List of the files to compile.
        /// </summary>
        public Dictionary<string, string> Files;
        /// <summary>
        /// Constructor.
        /// </summary>
        public FileCompiler()
        {
            Files = new Dictionary<string, string>();
        }
        /// <summary>
        /// Compiles the files.
        /// </summary>
        /// <param name="path">Path of the output.</param>
        public void Compile(string path)
        {
            if (Files == null)
                throw new Exception("No files specified.");
            System.IO.FileStream streamOutput;
            try
            {
                streamOutput = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurrenced loading the ouput file to \"" + path + "\" : ", e);
            }
            {
                string tmp = Utilities.Int32ToFOURCC(Files.Count);
                byte[] key = new byte[4];
                key[0] = (byte)tmp[0];
                key[1] = (byte)tmp[1];
                key[2] = (byte)tmp[2];
                key[3] = (byte)tmp[3];
                streamOutput.Write(key, 0, 4);
            }
            foreach (var fileStr in Files)
            {
                if (fileStr.Key.Count() != 4)
                    throw new Exception("The identifer \"" + fileStr.Key + "\" isn't a FOURCC.");
                {
                    byte[] key = new byte[4];
                    key[0] = (byte)fileStr.Key[0];
                    key[1] = (byte)fileStr.Key[1];
                    key[2] = (byte)fileStr.Key[2];
                    key[3] = (byte)fileStr.Key[3];
                    streamOutput.Write(key, 0, 4);
                }
                System.IO.FileStream streamInput;
                try
                {
                    streamInput = new System.IO.FileStream(fileStr.Value, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                catch (Exception e)
                {
                    throw new Exception("An error occurrenced loading the input file to \"" + fileStr.Value + "\" : ", e);
                }
                {
                    string tmp = Utilities.Int32ToFOURCC((int)streamInput.Length);
                    byte[] key = new byte[4];
                    key[0] = (byte)tmp[0];
                    key[1] = (byte)tmp[1];
                    key[2] = (byte)tmp[2];
                    key[3] = (byte)tmp[3];
                    streamOutput.Write(key, 0, 4);
                }
            }
            foreach (var fileStr in Files)
            {
                System.IO.FileStream streamInput;
                try
                {
                    streamInput = new System.IO.FileStream(fileStr.Value, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                catch (Exception e)
                {
                    throw new Exception("An error occurrenced loading the input file to \"" + fileStr.Value + "\" : ", e);
                }
                byte[] bin = new byte[1];
                int i = 0;
                while (streamInput.Read(bin, 0, 1) == 1)
                {
                    streamOutput.Write(bin, 0, 1);
                    i++;
                }
                Console.WriteLine(i);
            }
        }
    }
    /// <summary>
    /// Extract files made with the FileCompiler.
    /// </summary>
    public class FileExtracter
    {
        public class CompiledFileStream : Stream
        {
            internal Stream Origin { get; set; }
            internal long Offset { get; set; }
            internal long Size { get; set; }
            private long position { get; set; }

            internal CompiledFileStream() { }

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
                throw new NotSupportedException();
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
                    Position = Utilities.Min(Length - 1, offset);
                if (origin == SeekOrigin.Current)
                    Position = Utilities.Min(Length - 1, offset + Position);
                if (origin == SeekOrigin.End)
                    Position = Utilities.Min(Length - 1, Length - 1 + offset);
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
        }
        /// <summary>
        /// List of the streams to the files extracted.
        /// </summary>
        public Dictionary<string, Stream> Files { get; private set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        public FileExtracter()
        {
            Files = new Dictionary<string, Stream>();
        }
        /// <summary>
        /// Loads a file and extracts its compiled files.
        /// </summary>
        /// <param name="path">Path to the compiled file.</param>
        public void Load(string path)
        {
            FileStream input;
            try
            {
                input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurrenced loading the input file to \"" + path + "\" : ", e);
            }
            int nbFiles;
            {
                byte[] tmpb = new byte[4];
                input.Read(tmpb, 0, 4);
                string str = "";
                str += (char)tmpb[0];
                str += (char)tmpb[1];
                str += (char)tmpb[2];
                str += (char)tmpb[3];
                nbFiles = Utilities.FOURCCToInt32(str);
            }
            int offset = 0;
            for (int i = 0;i<nbFiles;i++)
            {
                string fourcc;
                {
                    byte[] tmpb = new byte[4];
                    input.Read(tmpb, 0, 4);
                    fourcc = "";
                    fourcc += (char)tmpb[0];
                    fourcc += (char)tmpb[1];
                    fourcc += (char)tmpb[2];
                    fourcc += (char)tmpb[3];
                }
                int size;
                {
                    byte[] tmpb = new byte[4];
                    input.Read(tmpb, 0, 4);
                    string tmp = "";
                    tmp += (char)tmpb[0];
                    tmp += (char)tmpb[1];
                    tmp += (char)tmpb[2];
                    tmp += (char)tmpb[3];
                    size = Utilities.FOURCCToInt32(tmp);
                }
                Files.Add(fourcc, new CompiledFileStream() { Origin = input, Offset = offset + nbFiles * 8 + 4, Size = size, Position = 0 });
                offset += size;
            }
        }
    }
}
