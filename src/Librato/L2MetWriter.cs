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
			Write(null, metric);
		}

		public void Write(string source, Metric metric)
		{
			var l2MetType = GetL2MetType(metric);
			var prefix = string.IsNullOrEmpty(metric.Prefix) ? "" : string.Format("{0}.", metric.Prefix);
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
				case MetricType.Count:
					return "count";
				case MetricType.Measure:
					return "measure";
				default:
					throw new NotSupportedException();
			}
		}
	}
}
