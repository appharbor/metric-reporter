using System;

namespace Librato
{
	public class MetricReporter
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

		private class PrefixingMetricReporter : MetricReporter
		{
			private readonly string _prefix;

			public PrefixingMetricReporter(string prefix, IMetricWriter metricWriter, StopwatchFactory stopwatchFactory)
				: base(metricWriter, stopwatchFactory)
			{
				_prefix = prefix;
			}

			protected override void WriteMetric(string source, Metric metric)
			{
				metric.Prefix = _prefix;
				base.WriteMetric(source, metric);
			}
		}

		public void Group(string prefix, Action<MetricReporter> logReporterAction)
		{
			var logReporter = new PrefixingMetricReporter(prefix, _metricWriter, _stopwatchFactory);
			logReporterAction(logReporter);
		}

		public void Increment(string counterName, double incrementBy = 1, string source = null)
		{
			var metric = new CountMetric(counterName, incrementBy);
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

		protected virtual void WriteMetric(string source, Metric metric)
		{
			if (string.IsNullOrEmpty(source))
			{
				_metricWriter.Write(metric);
			}
			else
			{
				_metricWriter.Write(metric, source);
			}
		}
	}
}
