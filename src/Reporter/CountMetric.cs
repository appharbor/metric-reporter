namespace AppHarbor.Metrics.Reporter
{
	public class CountMetric : Metric
	{
		public CountMetric(string name, double value)
			: base(MetricType.Count, name, value)
		{
		}
	}
}
