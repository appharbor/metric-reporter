using System;
using System.Diagnostics;

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
			var metric = new MeasureMetric(counterName, value);
			_metricWriter.Write(metric);
		}

		public void Measure(string counterName, Action action)
		{
			var stopWatch = new Stopwatch();

			stopWatch.Start();
			action();
			stopWatch.Stop();

			Measure(counterName, stopWatch.ElapsedMilliseconds);
		}
	}
}
