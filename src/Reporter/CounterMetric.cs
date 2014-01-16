namespace AppHarbor.Metrics.Reporter
{
	public class CounterMetric : Metric
	{
		public CounterMetric(string name, double value)
			: base(MetricType.Counter, name, value)
		{
		}
	}
}
