using System;
using System.Threading;
using AppHarbor.Metrics.Reporter;

namespace AppHarbor.Metrics.Sample
{
	public class Program
	{
		public static void Main()
		{
			// Write to console with the `Console.Out` text writer
			var textWriter = new TraceTextWriter();

			var metricWriter = new L2MetWriter(textWriter);
			var reporter = new MetricReporter(metricWriter);

			// Increment count by one
			reporter.Increment("users");

			// Increment by 8 and set source
			reporter.Increment("products", incrementBy: 8, source: "web.1");

			// Measure time to complete a task
			reporter.Measure("search.querytime", () =>
			{
				// Do work
				Thread.Sleep(1000);
			});

			// Add a prefix to a group of metrics
			reporter.Group("eu-region", x =>
			{
				x.Increment("sessions", 45, "web.2");
				x.Measure("db.execution_time", 344, "web.2");
			});

			Console.ReadLine();
		}
	}
}
