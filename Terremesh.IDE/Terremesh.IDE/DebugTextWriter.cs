using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Terremesh.IDE
{
    /// <summary>
    /// Implements a stream writer for writing text to Trace log.
    /// </summary>
    class TraceTextWriter : StreamWriter
    {
        /// <summary>
        /// Creates instance of the TraceTextWriter.
        /// </summary>
        public TraceTextWriter()
            : base(new TraceOutStream(), Encoding.Unicode, 1024)
        {
            this.AutoFlush = true;
        }

        /// <summary>
        /// Implements a stream for writing text to Trace log.
        /// </summary>
        private class TraceOutStream : Stream
        {
            public override void Write(byte[] buffer, int offset, int count)
            {
                Trace.Write(Encoding.Unicode.GetString(buffer, offset, count));
            }

            public override bool CanRead { get { return false; } }
            public override bool CanSeek { get { return false; } }
            public override bool CanWrite { get { return true; } }
            public override void Flush() { Trace.Flush(); }
            public override long Length { get { throw new InvalidOperationException(); } }
            public override int Read(byte[] buffer, int offset, int count) { throw new InvalidOperationException(); }
            public override long Seek(long offset, SeekOrigin origin) { throw new InvalidOperationException(); }
            public override void SetLength(long value) { throw new InvalidOperationException(); }
            public override long Position
            {
                get { throw new InvalidOperationException(); }
                set { throw new InvalidOperationException(); }
            }
        };
    }
}
