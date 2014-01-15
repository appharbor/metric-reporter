namespace Librato
{
	public abstract class Metric
	{
		private readonly MetricType _metricType;
		private readonly string _name;
		private readonly long _value;

		public Metric(MetricType metricType, string name, long value)
		{
			_metricType = metricType;
			_name = name;
			_value = value;
		}

		public virtual long Value
		{
			get
			{
				return _value;
			}
		}
	}
}
