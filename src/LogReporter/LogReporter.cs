using System;

namespace Librato
{
	public class LogReporter
	{
		private readonly IMetricWriter _metricWriter;

		public LogReporter(IMetricWriter metricWriter)
		{
			_metricWriter = metricWriter;
		}

		public void Increment(string counterName)
		{
			throw new NotImplementedException();
		}
	}
}
