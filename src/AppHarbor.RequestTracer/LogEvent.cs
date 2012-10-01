using System;
using System.Web.Management;

namespace AppHarbor.RequestTracer
{
	/// <summary>
	/// 
	/// </summary>
	public class LogEvent : WebRequestErrorEvent
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public LogEvent(string message) : base(null, null, 100001, new Exception(message))
		{
		}
	}
}