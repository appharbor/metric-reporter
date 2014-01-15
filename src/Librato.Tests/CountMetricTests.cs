using Xunit;

namespace Librato.Tests
{
	public class CountMetricTests
	{
		[Fact]
		public void ShouldSetMetricTypeToCount()
		{
			var countMetric = new CountMetric("foo", 1);
			Assert.Equal(MetricType.Count, countMetric.MetricType);
		}
	}
}
