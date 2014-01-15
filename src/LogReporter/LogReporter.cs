using System;
using System.IO;

namespace Librato
{
	public class LogReporter
	{
		private readonly TextWriter _writer;

		public LogReporter(TextWriter writer)
		{
			_writer = writer;
		}

		public void Increment()
		{
			throw new NotImplementedException();
		}
	}
}
