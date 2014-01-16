using System.IO;
using Moq;
using Xunit;

namespace AppHarbor.Metrics.Reporter.Tests
{
	public class L2MetWriterTests
	{
		private static readonly string DefaultMetricName = "foo";
		private static readonly double DefaultMetricValue = 1;

		private readonly Mock<TextWriter> _textWriterMock;
		private readonly L2MetWriter _l2MetWriter;

		public L2MetWriterTests()
		{
			_textWriterMock = new Mock<TextWriter>(MockBehavior.Loose);
			_l2MetWriter = new L2MetWriter(_textWriterMock.Object);
		}

		[Fact]
		public void ShouldSupportMeasureMetric()
		{
			var metric = new GaugeMetric(DefaultMetricName, DefaultMetricValue);

			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("measure#{0}={1}", DefaultMetricName, DefaultMetricValue)));
		}

		[Fact]
		public void ShouldSupportCountMetric()
		{
			var metric = new CounterMetric(DefaultMetricName, DefaultMetricValue);

			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("count#{0}={1}", DefaultMetricName, DefaultMetricValue)));
		}

		[Fact]
		public void ShouldWriteL2MetFormattedMetric()
		{
			var metric = new CounterMetric(DefaultMetricName, DefaultMetricValue);

			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("{0}#{1}={2}", "count", DefaultMetricName, DefaultMetricValue)));
		}

		[Fact]
		public void ShouldWriteMetricWithPrefixIfSet()
		{
			var prefix = "bar";
			var metric = new CounterMetric(DefaultMetricName, DefaultMetricValue)
			{
				Prefix = prefix,
			};

			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("{0}#{1}.{2}={3}", "count", prefix, DefaultMetricName, DefaultMetricValue)));
		}

		[Fact]
		public void ShouldPrependSourceWhenSpecified()
		{
			var metric = new CounterMetric(DefaultMetricName, DefaultMetricValue);
			var source = "baz";

			_l2MetWriter.Write(metric, source);

			_textWriterMock.Verify(x => x.WriteLine(It.Is<string>(
				y => y.StartsWith(string.Format("source={0} ", source)))));
		}
	}
}
