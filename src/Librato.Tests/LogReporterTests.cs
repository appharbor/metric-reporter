using Moq;
using Xunit;

namespace Librato.Tests
{
	public class LogReporterTests
	{
		private readonly Mock<IMetricWriter> _metricWriterMock;
		private readonly LogReporter _logReporter;

		public LogReporterTests()
		{
			_metricWriterMock = new Mock<IMetricWriter>(MockBehavior.Loose);
			_logReporter = new LogReporter(_metricWriterMock.Object);
		}

		[Fact]
		public void ShouldWriteCountMetricWhenIncrementing()
		{
			_logReporter.Increment("foo");

			_metricWriterMock.Verify(x => x.Write(It.IsAny<CountMetric>()));
		}

		[Fact]
		public void ShouldIncrementByOneWhenNoValueIsSpecified()
		{
			_logReporter.Increment("foo");

			_metricWriterMock.Verify(x => x.Write(It.Is<Metric>(y => y.Value == 1)));
		}
	}
}
