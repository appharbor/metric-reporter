namespace Librato
{
	public class MeasureMetric : Metric
	{
		public MeasureMetric(string name, double value)
			: base(MetricType.Measure, name, value)
		{
		}
	}
}
