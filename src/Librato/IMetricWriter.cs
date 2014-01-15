namespace Librato
{
	public interface IMetricWriter
	{
		void Write(Metric metric);
		void Write(string source, Metric metric);
	}
}
