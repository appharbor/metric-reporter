namespace Librato
{
	public interface IMetricWriter
	{
		void Write(MetricType metricType, string counterName, long value);
	}
}
