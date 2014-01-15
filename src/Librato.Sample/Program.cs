using System;
using System.Threading;

namespace Librato.Sample
{
	public class Program
	{
		public static void Main()
		{
			var writer = new L2MetWriter(Console.Out);
			var reporter = new MetricReporter(writer);

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
