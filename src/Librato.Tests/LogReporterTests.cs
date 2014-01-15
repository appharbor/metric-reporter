using Moq;
using Xunit;

namespace Librato.Tests
{
	public class LogReporterTests
	{
		[Fact]
		public void ShouldWriteCountMetricWhenIncrementing()
		{
			var metricWriterMock = new Mock<IMetricWriter>(MockBehavior.Loose);
			var logReporter = new LogReporter(metricWriterMock.Object);

			logReporter.Increment("foo");

			metricWriterMock.Verify(x => x.Write(It.IsAny<CountMetric>()));
		}
	}
}
