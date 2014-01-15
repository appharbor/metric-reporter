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
		public void ShouldWriteL2MetMetrics()
		{
			var metricName = "foo";
			var metricValue = 123;

			var metric = new CountMetric(metricName, metricValue);
			_l2MetWriter.Write(metric);

			_textWriterMock.Verify(x => x.WriteLine(string.Format("{0}#{1}={2}", "count", metricName, metricValue)));
		}
	}
}
