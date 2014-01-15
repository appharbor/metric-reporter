using System;

namespace Librato
{
	public class LogReporter
	{
		private readonly ILogWriter _logWriter;

		public LogReporter(ILogWriter logWriter)
		{
			_logWriter = logWriter;
		}

		public void Increment(string counterName)
		{
			throw new NotImplementedException();
		}
	}
}
