using System;
using System.Diagnostics;

namespace Librato
{
	public class LogReporter
	{
		private readonly IMetricWriter _metricWriter;
		private readonly StopwatchFactory _stopwatchFactory;

		public LogReporter(IMetricWriter metricWriter, StopwatchFactory stopwatchFactory)
		{
			_metricWriter = metricWriter;
			_stopwatchFactory = stopwatchFactory;
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
			var stopWatch = _stopwatchFactory.Get();

			stopWatch.Start();
			action();
			stopWatch.Stop();

			Measure(counterName, stopWatch.ElapsedMilliseconds);
		}
	}
}
