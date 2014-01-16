using System.Collections.Generic;

namespace AppHarbor.Metrics.Reporter
{
	public abstract class Metric
	{
		private readonly IList<string> _prefixes;
		private readonly MetricType _metricType;
		private readonly string _name;
		private readonly double _value;

		public Metric(MetricType metricType, string name, double value)
		{
			_prefixes = new List<string>();
			_metricType = metricType;
			_name = name;
			_value = value;
		}

		public virtual MetricType MetricType
		{
			get
			{
				return _metricType;
			}
		}

		public virtual string Name
		{
			get
			{
				return _name;
			}
		}

		public virtual double Value
		{
			get
			{
				return _value;
			}
		}

		public virtual IList<string> Prefixes
		{
			get
			{
				return _prefixes;
			}
		}
	}
}
