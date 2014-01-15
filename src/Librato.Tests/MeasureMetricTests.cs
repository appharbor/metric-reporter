using Xunit;

namespace Librato.Tests
{
	public class MeasureMetricTests
	{
		[Fact]
		public void ShouldSetMetricTypeToMeasure()
		{
			var measureMetric = new MeasureMetric("foo", 1);

			Assert.Equal(MetricType.Measure, measureMetric.MetricType);
		}
	}
}
