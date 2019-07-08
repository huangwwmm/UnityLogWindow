namespace Debug
{
	public enum LogType
	{
		NotSet = -1,
		Error = 1 << 0,
		Assert = 1 << 1,
		Warning = 1 << 2,
		Log = 1 << 3,
		Exception = 1 << 4
	}
}
