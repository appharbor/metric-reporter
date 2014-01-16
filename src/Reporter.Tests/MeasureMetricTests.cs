using Xunit;

namespace AppHarbor.Metrics.Reporter.Tests
{
	public class MeasureMetricTests
	{
		[Fact]
		public void ShouldSetMetricTypeToMeasure()
		{
			var measureMetric = new GaugeMetric("foo", 1);

			Assert.Equal(MetricType.Gauge, measureMetric.MetricType);
		}
	}
}
