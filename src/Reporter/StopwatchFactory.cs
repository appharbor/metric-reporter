using System.Diagnostics;

namespace AppHarbor.Metrics.Reporter
{
	public class StopwatchFactory
	{
		public virtual Stopwatch Get()
		{
			return new Stopwatch();
		}
	}
}
