using System;

namespace Debug
{
	/// <summary>
	/// 用缩写是为了转json时减少字符
	/// </summary>
	public struct LogItem
	{
		/// <summary>
		/// LogType的缩写
		/// </summary>
		public readonly LogType LT;
		/// <summary>
		/// DataTime的缩写
		/// </summary>
		public readonly DateTime DT;
		public readonly string Tag;
		public readonly string Text;
		/// <summary>
		/// StackTrace的缩写
		/// </summary>
		public readonly string ST;

		public LogItem(LogType logType, DateTime dateTime, string tag, string text, string stackTrace)
		{
			LT = logType;
			DT = dateTime;
			Tag = tag;
			Text = text;
			ST = stackTrace;
		}

		public string GetLogHash()
		{
			return string.Format("{0}-{1}-{2}", LT, Tag, Text);
		}
	}
}
