using System;
using System.Collections.Generic;

namespace Debug
{
	public class LogFilter : ICloneable
	{
		public int Level = int.MaxValue;
		public bool Collapse = true;
		public string FilterText = null;

		private HashSet<string> m_LogItemHashs = new HashSet<string>();
		private bool m_ShowStackTrace;

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
			bool containsFilter = string.IsNullOrEmpty(FilterText)
				|| item.Text.ToUpper().Contains(FilterText)
				|| item.Tag.ToUpper().Contains(FilterText)
				|| m_ShowStackTrace && item.ST.ToUpper().Contains(FilterText);
			if (!containsFilter)
			{
				return false;
			}

			// 这条判断要放在最后，因为会改变m_LogItemHashs
			if (Collapse
				&& !m_LogItemHashs.Add(item.GetLogHash()))
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

		public void Reset(bool showStackTrace)
		{
			m_LogItemHashs = new HashSet<string>();
			m_ShowStackTrace = showStackTrace;
		}
	}
}