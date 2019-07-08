using System;
using System.IO;
using UnityEngine;

namespace LogRecord
{
#if UNITY_EDITOR
	[UnityEditor.InitializeOnLoad]
#endif
	public static class LogRecord
	{
		private static readonly string LOGRECORD_DIRECTORY = string.Format("{0}Unity_LogRecord\\", Path.GetTempPath());

		private static StreamWriter ms_StreamWriter;
		private static string ms_LogRecordPath;

		static LogRecord()
		{
			Application.logMessageReceivedThreaded -= OnLogReceived;
			try
			{
				ms_LogRecordPath = string.Format("{0}{1}.log", LOGRECORD_DIRECTORY, DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));
				Debug.Log("LogRecord path: " + ms_LogRecordPath);
				if (!Directory.Exists(LOGRECORD_DIRECTORY))
				{
					Directory.CreateDirectory(LOGRECORD_DIRECTORY);
				}
				ms_StreamWriter = new StreamWriter(ms_LogRecordPath
					, false
					, System.Text.Encoding.UTF8);
				ms_StreamWriter.AutoFlush = true;
				Application.logMessageReceivedThreaded += OnLogReceived;
			}
			catch (Exception e)
			{
				Debug.LogError("LogRecord initialize exception:\n" + e.ToString());
			}
		}

#if UNITY_EDITOR
		public static void OnReportLogButtonClick()
		{
			Debug.Log("LogRecord path: " + ms_LogRecordPath);
			UnityEditor.EditorUtility.RevealInFinder(ms_LogRecordPath);
		}
#endif

		private static void OnLogReceived(string condition, string stackTrace, LogType type)
		{
			LogItem logItem = new LogItem(type, DateTime.Now, "Default", condition, stackTrace);
			bool oldPrettryPrint = LitJson.JsonMapper.GetStaticJsonWriter().PrettyPrint;
			LitJson.JsonMapper.GetStaticJsonWriter().PrettyPrint = false;
			string json = LitJson.JsonMapper.ToJson(logItem);
			LitJson.JsonMapper.GetStaticJsonWriter().PrettyPrint = oldPrettryPrint;
			ms_StreamWriter.WriteLine(json);
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void _ForInitialize()
		{
		}

		public struct LogItem
		{
			/// <summary>
			/// LogType的缩写
			/// </summary>
			public readonly int LT;
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
				LT = 1 << (int)logType;
				DT = dateTime;
				Tag = tag;
				Text = text;
				ST = stackTrace;
			}
		}
	}
}