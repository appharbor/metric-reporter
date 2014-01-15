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
			_textWriter.WriteLine(string.Format("{0}#{1}={2}", l2MetType, metric.Name, metric.Value));
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
