using System.Diagnostics;
using System.IO;
using System.Text;

namespace AppHarbor.Metrics.Sample
{
	public class TraceTextWriter : TextWriter
	{
		public override Encoding Encoding
		{
			get
			{
				return Encoding.Default;
			}
		}

		public override void Close()
		{
			Trace.Close();
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		public override void Flush()
		{
			Trace.Flush();
			base.Flush();
		}

		public override void Write(bool value)
		{
			Trace.Write(value);
		}

		public override void Write(char value)
		{
			Trace.Write(value);
		}

		public override void Write(char[] buffer)
		{
			Trace.Write(buffer);
		}

		public override void Write(decimal value)
		{
			Trace.Write(value);
		}

		public override void Write(double value)
		{
			Trace.Write(value);
		}

		public override void Write(float value)
		{
			Trace.Write(value);
		}

		public override void Write(int value)
		{
			Trace.Write(value);
		}

		public override void Write(long value)
		{
			Trace.Write(value);
		}

		public override void Write(object value)
		{
			Trace.Write(value);
		}

		public override void Write(string value)
		{
			Trace.Write(value);
		}

		public override void Write(uint value)
		{
			Trace.Write(value);
		}

		public override void Write(ulong value)
		{
			Trace.Write(value);
		}

		public override void Write(string format, object arg0)
		{
			Trace.Write(string.Format(format, arg0));
		}

		public override void Write(string format, params object[] arg)
		{
			Trace.Write(string.Format(format, arg));
		}

		public override void Write(char[] buffer, int index, int count)
		{
			string x = new string(buffer, index, count);
			Trace.Write(x);
		}

		public override void Write(string format, object arg0, object arg1)
		{
			Trace.Write(string.Format(format, arg0, arg1));
		}

		public override void Write(string format, object arg0, object arg1, object arg2)
		{
			Trace.Write(string.Format(format, arg0, arg1, arg2));
		}

		public override void WriteLine()
		{
			Trace.WriteLine(string.Empty);
		}

		public override void WriteLine(bool value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(char value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(char[] buffer)
		{
			Trace.WriteLine(buffer);
		}

		public override void WriteLine(decimal value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(double value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(float value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(int value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(long value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(object value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(string value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(uint value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(ulong value)
		{
			Trace.WriteLine(value);
		}

		public override void WriteLine(string format, object arg0)
		{
			Trace.WriteLine(string.Format(format, arg0));
		}

		public override void WriteLine(string format, params object[] arg)
		{
			Trace.WriteLine(string.Format(format, arg));
		}

		public override void WriteLine(char[] buffer, int index, int count)
		{
			string x = new string(buffer, index, count);
			Trace.WriteLine(x);

		}

		public override void WriteLine(string format, object arg0, object arg1)
		{
			Trace.WriteLine(string.Format(format, arg0, arg1));
		}

		public override void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Trace.WriteLine(string.Format(format, arg0, arg1, arg2));
		}
	}
}
