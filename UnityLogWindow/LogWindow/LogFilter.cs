using System;

namespace Debug
{
	public class LogFilter : ICloneable
	{
		public int Level = int.MaxValue;

		public object Clone()
		{
			LogFilter filter = new LogFilter
			{
				Level = Level
			};
			return filter;
		}

		public bool Filter(LogItem item)
		{
			if (!FilterLevel(item.LT))
			{
				return false;
			}
			return true;
		}

		public bool FilterLevel(LogType level)
		{
			return (((int)level) & Level) != 0;
		}

		public void SetLevel(LogType level, bool state)
		{
			if (state)
			{
				Level = Level | (int)level;
			}
			else
			{
				Level = Level & (~(int)level);
			}
		}
	}
}