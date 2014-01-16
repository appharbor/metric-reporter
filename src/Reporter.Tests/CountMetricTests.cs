using Xunit;

namespace AppHarbor.Metrics.Reporter.Tests
{
	public class CountMetricTests
	{
		[Fact]
		public void ShouldSetMetricTypeToCount()
		{
			var countMetric = new CounterMetric("foo", 1);

			Assert.Equal(MetricType.Count, countMetric.MetricType);
		}
	}
}
