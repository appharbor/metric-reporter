namespace Librato
{
	public interface IMetricWriter
	{
		void Write(Metric metric);
		void Write(Metric metric, string source);
	}
}
