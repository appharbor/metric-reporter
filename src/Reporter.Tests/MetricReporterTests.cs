using System.Diagnostics;
using System.Linq;
using System.Threading;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace AppHarbor.Metrics.Reporter.Tests
{
	public class MetricReporterTests
	{
		private readonly Mock<IMetricWriter> _metricWriterMock;
		private readonly Mock<StopwatchFactory> _stopwatchFactory;
		private readonly MetricReporter _metricReporter;

		public MetricReporterTests()
		{
			_metricWriterMock = new Mock<IMetricWriter>(MockBehavior.Loose);
			_stopwatchFactory = new Mock<StopwatchFactory>(MockBehavior.Loose);
			_metricReporter = new MetricReporter(_metricWriterMock.Object, _stopwatchFactory.Object);
		}

		[Fact]
		public void ShouldWriteCountMetricWhenIncrementing()
		{
			_metricReporter.Increment("foo");

			_metricWriterMock.Verify(x => x.Write(It.IsAny<CounterMetric>(), null));
		}

		[Fact]
		public void ShouldIncrementByOneWhenNoValueIsSpecified()
		{
			_metricReporter.Increment("foo");

			_metricWriterMock.Verify(x => x.Write(It.Is<Metric>(y => y.Value == 1), null));
		}

		[Theory]
		[InlineData(1)]
		[InlineData(1.5)]
		[InlineData(2)]
		public void ShouldIncrementByValueWhenSpecified(double value)
		{
			_metricReporter.Increment("foo", value);

			_metricWriterMock.Verify(x => x.Write(It.Is<Metric>(y => y.Value == value), null));
		}

		[Fact]
		public void ShouldWriteMeasureMetricWhenMeasuring()
		{
			_metricReporter.Measure("foo", 1);

			_metricWriterMock.Verify(x => x.Write(It.IsAny<GaugeMetric>(), null));
		}

		[Fact]
		public void ShouldMeasureTimeWhenInvokedWithAction()
		{
			var stopwatch = new Stopwatch();
			_stopwatchFactory.Setup(x => x.Get()).Returns(stopwatch);

			_metricReporter.Measure("foo", () => Thread.Sleep(1));

			_metricWriterMock.Verify(x => x.Write(It.Is<Metric>(y => y.Value == stopwatch.ElapsedMilliseconds), null));
		}

		[Fact]
		public void ShouldIncrementWithSourceWhenSpecified()
		{
			var source = "foo";
			_metricReporter.Increment("bar", source: source);

			_metricWriterMock.Verify(x => x.Write(It.IsAny<Metric>(), source));
		}

		[Fact]
		public void ShouldMeasureWithSourceWhenSpecified()
		{
			var source = "foo";
			_metricReporter.Measure("bar", 1, source: source);

			_metricWriterMock.Verify(x => x.Write(It.IsAny<Metric>(), source));
		}

		[Fact]
		public void ShouldPrefixMetricsWhenGrouping()
		{
			var prefix = "foo";
			_metricReporter.Group(prefix, x =>
			{
				x.Increment("bar");
			});

			_metricWriterMock.Verify(x => x.Write(It.Is<Metric>(y => y.Prefixes.Contains(prefix) && y.Prefixes.Count == 1), null));
		}

		[Fact]
		public void ShouldAddMultiplePrefixesWithNestedGrouping()
		{
			var prefix = "foo";
			var nestedPrefix = "bar";

			_metricReporter.Group(prefix, x =>
			{
				x.Group(nestedPrefix, y =>
				{
					y.Increment("baz");
				});
			});

			_metricWriterMock.Verify(x => x.Write(It.Is<Metric>(y =>
				y.Prefixes[0] == prefix && y.Prefixes[1] == nestedPrefix && y.Prefixes.Count == 2), null));
		}

		[Fact]
		public void ShouldInvokeActionWhenGrouping()
		{
			bool isActionCalled = false;
			_metricReporter.Group("foo", x => isActionCalled = true);

			Assert.True(isActionCalled);
		}
	}
}
