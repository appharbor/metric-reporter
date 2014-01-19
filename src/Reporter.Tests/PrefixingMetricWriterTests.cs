using System.Collections.Generic;
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

		[Fact]
		public void ShouldAddNestedPrefixes()
		{
			var firstPrefix = "foo";
			var secondPrefix = "bar";
			var metric = new CounterMetric("baz", 1);

			var writer = new PrefixingMetricWriter(firstPrefix, new Mock<IMetricWriter>().Object);
			var secondWriter = new PrefixingMetricWriter(secondPrefix, writer);

			secondWriter.Write(metric, null);

			Assert.Equal(2, metric.Prefixes.Count);
			Assert.Equal(metric.Prefixes, new List<string> { firstPrefix, secondPrefix });
		}
	}
}
