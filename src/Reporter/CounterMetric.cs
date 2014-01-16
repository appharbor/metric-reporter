namespace AppHarbor.Metrics.Reporter
{
	public class CounterMetric : Metric
	{
		public CounterMetric(string name, double value)
			: base(MetricType.Count, name, value)
		{
		}
	}
}
