using System;
using System.IO;
using System.Threading;
using AppHarbor.Metrics.Reporter;

namespace AppHarbor.Metrics.Sample
{
	public class Program
	{
		public static void Main()
		{
			// Initialize a TextWriter
			var textWriter = Console.Out;

			/*
			Write to a file instead:
				var stream = new FileStream("c:\\foobar.txt", FileMode.Append);
				var textWriter = new StreamWriter(stream);

			Write to Trace (use this on AppHarbor):
				var textWriter = new TraceTextWriter();
			*/

			var metricWriter = new L2MetWriter(textWriter);
			var reporter = new MetricReporter(metricWriter);

			// Increment "users" counter by one
			reporter.Increment("users");

			// Increment "products" counter by 8 while setting source
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

				// Add a nested prefix to a subgroup of metrics
				x.Group("tcp", y =>
				{
					y.Measure("time_wait", 23);
				});
			});
			Console.ReadLine();
		}
	}
}
