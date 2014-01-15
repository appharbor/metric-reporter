using System.Diagnostics;

namespace Librato
{
	public class StopwatchFactory
	{
		public virtual Stopwatch Get()
		{
			return new Stopwatch();
		}
	}
}
