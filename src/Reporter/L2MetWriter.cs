using System;
using System.Linq;
using System.IO;

namespace AppHarbor.Metrics.Reporter
{
	public class L2MetWriter : IMetricWriter
	{
		private readonly TextWriter _textWriter;

		public L2MetWriter(TextWriter textWriter)
		{
			_textWriter = textWriter;
		}

		public void Write(Metric metric, string source = null)
		{
			var l2MetType = GetL2MetType(metric);
			var prefix = metric.Prefixes.Any() ? string.Concat(string.Join(".", metric.Prefixes.ToArray()), ".") : "";
			var output = string.Format("{0}#{1}{2}={3}", l2MetType, prefix, metric.Name, metric.Value);
			if (!string.IsNullOrEmpty(source))
			{
				output = string.Format("source={0} {1}", source, output);
			}

			_textWriter.WriteLine(output);
		}

		private static string GetL2MetType(Metric metric)
		{
			switch (metric.MetricType)
			{
				case MetricType.Counter:
					return "count";
				case MetricType.Gauge:
					return "measure";
				default:
					throw new NotSupportedException();
			}
		}
	}
}
