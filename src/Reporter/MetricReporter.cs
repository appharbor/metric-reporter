using System;

namespace AppHarbor.Metrics.Reporter
{
	public class MetricReporter : IMetricReporter
	{
		private readonly IMetricWriter _metricWriter;
		private readonly StopwatchFactory _stopwatchFactory;

		public MetricReporter(IMetricWriter metricWriter)
			: this(metricWriter, new StopwatchFactory())
		{
		}

		public MetricReporter(IMetricWriter metricWriter, StopwatchFactory stopwatchFactory)
		{
			_metricWriter = metricWriter;
			_stopwatchFactory = stopwatchFactory;
		}

		public void Group(string prefix, Action<MetricReporter> logReporterAction)
		{
			var metricWriter = new PrefixingMetricWriter(prefix, _metricWriter);
			var logReporter = new MetricReporter(metricWriter);

			logReporterAction(logReporter);
		}

		public void Increment(string counterName, double incrementBy = 1, string source = null)
		{
			var metric = new CounterMetric(counterName, incrementBy);
			_metricWriter.Write(metric, source);
		}

		public void Measure(string gaugeName, Action action, string source = null)
		{
			var stopWatch = _stopwatchFactory.Get();

			stopWatch.Start();
			action();
			stopWatch.Stop();

			Measure(gaugeName, stopWatch.ElapsedMilliseconds, source);
		}

		public void Measure(string gaugeName, double value, string source = null)
		{
			var metric = new GaugeMetric(gaugeName, value);
			_metricWriter.Write(metric, source);
		}
	}
}
