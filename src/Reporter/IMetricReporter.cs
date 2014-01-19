using System;

namespace AppHarbor.Metrics.Reporter
{
	public interface IMetricReporter
	{
		void Group(string prefix, Action<IMetricReporter> logReporterAction);
		void Increment(string counterName, double incrementBy = 1, string source = null);
		void Measure(string gaugeName, Action action, string source = null);
		void Measure(string gaugeName, double value, string source = null);
	}
}
