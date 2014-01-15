namespace Librato
{
	public class CountMetric : Metric
	{
		public CountMetric(string name, long value)
			: base(MetricType.Count, name, value)
		{
		}
	}
}
