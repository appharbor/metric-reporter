using System.IO;
using Moq;
using Xunit;

namespace Librato.Tests
{
	public class L2MetWriterTests
	{
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
			var metricName = "foo";
			var metricValue = 1;
			var metric = new MeasureMetric(metricName, metricValue);

			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("measure#{0}={1}", metricName, metricValue)));
		}

		[Fact]
		public void ShouldWriteL2MetFormattedMetric()
		{
			var metricName = "foo";
			var metricValue = 123;

			var metric = new CountMetric(metricName, metricValue);
			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("{0}#{1}={2}", "count", metricName, metricValue)));
		}

		[Fact]
		public void ShouldWriteMetricWithPrefixIfSet()
		{
			var prefix = "foo";
			var metricName = "bar";
			var metricValue = 1;

			var metric = new CountMetric(metricName, 1)
			{
				Prefix = prefix,
			};

			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("{0}#{1}.{2}={3}", "count", prefix, metricName, metricValue)));
		}

		[Fact]
		public void ShouldPrependSourceWhenSpecified()
		{
			var metric = new CountMetric("foo", 1);
			var source = "baz";

			_l2MetWriter.Write(source, metric);

			_textWriterMock.Verify(x => x.WriteLine(It.Is<string>(
				y => y.StartsWith(string.Format("source={0} ", source)))));
		}
	}
}
