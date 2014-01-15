using Moq;
using Xunit;
using Xunit.Extensions;

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

		[Theory]
		[InlineData(1)]
		[InlineData(1.5)]
		[InlineData(2)]
		public void ShouldIncrementByValueWhenSpecified(double value)
		{
			_logReporter.Increment("foo", value);

			_metricWriterMock.Verify(x => x.Write(It.Is<Metric>(y => y.Value == value)));
		}

		[Fact]
		public void ShouldWriteMeasureMetricWhenMeasuring()
		{
			_logReporter.Measure("foo", 1);

			_metricWriterMock.Verify(x => x.Write(It.IsAny<MeasureMetric>()));
		}
	}
}
