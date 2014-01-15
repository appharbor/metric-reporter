namespace Librato
{
	public class CountMetric : Metric
	{
		public CountMetric(string name, double value)
			: base(MetricType.Count, name, value)
		{
		}
	}
}
