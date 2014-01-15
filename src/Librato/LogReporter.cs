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

		public void Increment(string counterName, double value = 1, string source = null)
		{
			var metric = new CountMetric(counterName, value);
			WriteMetric(source, metric);
		}

		public void Measure(string counterName, double value, string source = null)
		{
			var metric = new MeasureMetric(counterName, value);
			WriteMetric(source, metric);
		}

		public void Measure(string counterName, Action action, string source = null)
		{
			var stopWatch = _stopwatchFactory.Get();

			stopWatch.Start();
			action();
			stopWatch.Stop();

			Measure(counterName, stopWatch.ElapsedMilliseconds, source);
		}

		private void WriteMetric(string source, Metric metric)
		{
			if (string.IsNullOrEmpty(source))
			{
				_metricWriter.Write(metric);
			}
			else
			{
				_metricWriter.Write(source, metric);
			}
		}
	}
}
