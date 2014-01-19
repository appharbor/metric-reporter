using System.Linq;
using Moq;
using Xunit;

namespace AppHarbor.Metrics.Reporter.Tests
{
	public class PrefixingMetricWriterTests
	{
		[Fact]
		public void ShouldAddPrefixToMetric()
		{
			var prefix = "foo";
			var metric = new CounterMetric("bar", 1);
			var writer = new PrefixingMetricWriter(prefix, new Mock<IMetricWriter>().Object);

			writer.Write(metric, null);

			Assert.Single(metric.Prefixes);
			Assert.Equal(metric.Prefixes.Single(), prefix);
		}
	}
}
