using System;
using System.IO;

namespace Librato
{
	public class L2MetWriter : IMetricWriter
	{
		private readonly TextWriter _textWriter;

		public L2MetWriter(TextWriter textWriter)
		{
			_textWriter = textWriter;
		}

		public void Write(Metric metric)
		{
			var l2MetType = GetL2MetType(metric);
			var prefix = string.IsNullOrEmpty(metric.Prefix) ? "" : string.Format("{0}.", metric.Prefix);
			var l2metMetric = string.Format("{0}#{1}{2}={3}", l2MetType, prefix, metric.Name, metric.Value);

			_textWriter.WriteLine(l2metMetric);
		}

		public void Write(string source, Metric metric)
		{
			throw new NotImplementedException();
		}

		private static string GetL2MetType(Metric metric)
		{
			switch (metric.MetricType)
			{
				case MetricType.Count:
					return "count";
				default:
					throw new NotSupportedException();
			}
		}
	}
}
