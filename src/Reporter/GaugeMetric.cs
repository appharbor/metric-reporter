namespace AppHarbor.Metrics.Reporter
{
	public class GaugeMetric : Metric
	{
		public GaugeMetric(string name, double value)
			: base(MetricType.Gauge, name, value)
		{
		}
	}
}
