namespace AppHarbor.Metrics.Reporter
{
	public class PrefixingMetricWriter : IMetricWriter
	{
		private readonly string _prefix;
		private readonly IMetricWriter _metricWriter;

		public PrefixingMetricWriter(string prefix, IMetricWriter metricWriter)
		{
			_prefix = prefix;
			_metricWriter = metricWriter;
		}

		public void Write(Metric metric, string source)
		{
			metric.Prefixes.Insert(0, _prefix);
			_metricWriter.Write(metric, source);
		}
	}
}
