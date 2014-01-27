The `metric-reporter` library provides an easy interface for writing metrics to your logs or another stream. It includes a writer to write [l2met 2.0](https://github.com/ryandotsmith/l2met) formatted log lines ultimately bound for Librato's l2met service. If your application is running on AppHarbor this allows you to easily log metrics that can be send to Librato using [a log drain](http://support.appharbor.com/kb/tips-and-tricks/logging#log-drains).

This library is a port of [Librato's own log-reporter](https://github.com/librato/librato-logreporter) library intended for use with .NET applications.

## Quick Start

Install `metric-reporter` in your application using NuGet:

    Install-Package metric-reporter

Initialize a `MetricReporter` while injecting required dependencies:

    var writer = new MetricWriter(Console.Out);
    var reporter = new MetricReporter(metricWriter);

You can now start writing metrics like so:

    # Increment counter metric by one
    reporter.Increment("user.sessions");

    # Benchmark time to complete a task
	reporter.Measure("search.querytime", () =>
	{
	        //Do work
	        Thread.Sleep(1000);
	});

	# Track averages across processes/thread/requests etc
	reporter.Measure("payload.size", 3443);

## Configuration

#### Output

You can write to any TextWriter by injecting it into the `MetricWriter` class. For instance, use `Console.Out` for writing to the standard output. Use the included `TraceTextWriter` to write to your trace output like so:

    var textWriter = new TraceTextWriter();
    var metricWriter = new MetricWriter(textWriter);

NOTE: Using the TraceTextWriter is recommended when you use this library on AppHarbor - just make sure [tracing](http://support.appharbor.com/kb/tips-and-tricks/tracing) is enabled.

Use a `StreamWriter` to write metrics to a file stream:

    var stream = new FileStream("c:\\foobar.txt", FileMode.Append);
    var textWriter = new StreamWriter(stream);

Silence output by initializing the `StreamWriter` with `Stream.Null`:

    var textWriter = new StreamWriter(Stream.Null);


#### Source

Librato's `log-reporter` supports specifying a metric source and so does this library. A default source can optionally be specified when initializing a `MetricReporter`:

    var reporter = new MetricReporter(metricWriter, defaultSource: "web.1");

If no default source is specified your metric will be submitted without a source, unless specified when calling the helper methods `Increment` and `Measure`. You can also override the default source:

    reporter.Increment("requests.total", source: "web.1");
    reporter.Measure("request.time", 433, source: "web.1");

NOTE: On AppHarbor you can access the worker name with [the `appharbor.worker.name` appSettings key](http://support.appharbor.com/kb/getting-started/managing-environments#worker-name).


## Custom Measurements

Tracking anything that interests you is easy with the `metric-reporter` library. There are four primary helpers for the `MetricReporter` class available:

#### Increment

Use for tracking a running total of something _across_ jobs or requests, examples:

    # increment the 'jobs.completed' metric by one
    reporter.Increment("jobs.completed");

    # increment by five
    reporter.Increment("items.purchased", incrementBy: 5);

    # increment with a custom source
    reporter.Increment("user.purchases", source: "web.1");

 Other things you might track this way: user activity, requests of a certain type or to a certain route, total jobs queued or processed, emails sent or received

#### Measure

Use when you want to track an average value _per_-measurement period. Examples:

    reporter.Measure("payload.size", 212);

    # report from a custom source
    reporter.Measure("jobs.by.user', source: job.Requestor.Id)

#### Measure (action)

Like the other `Measure` overload this is per-period, but uses the time it takes to execute the delegate as the measurement value:

The block form auto-submits the time it took for its contents to execute as the measurement value:

    reporter.Measure("twitter.lookup.time", () =>
    {
        twitterRequest.GetResponse();
    });

#### group

There is also a grouping helper, to make managing nested metrics easier. So this:

    reporter.Measure("memcached.gets", 20);
    reporter.Measure("memcached.sets", 2);
    reporter.Measure("memcached.bytes.read", 342152);
    reporter.Measure("memcached.bytes.written", 2183);

Can also be written as:

    reporter.Group("memcached", x =>
    {
            x.Measure("gets", 20);
            x.Measure("set", 2);

            x.Group("bytes", y =>
            {
                    y.Measure("read", 342152);
                    y.Measure("written", 2183);
            });
    });
