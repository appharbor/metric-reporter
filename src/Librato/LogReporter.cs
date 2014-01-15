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

		public void Increment(string counterName, double value = 1)
		{
			var metric = new CountMetric(counterName, value);
			_metricWriter.Write(metric);
		}

		public void Measure(string counterName, double value)
		{
			throw new NotImplementedException();
		}
	}
}
