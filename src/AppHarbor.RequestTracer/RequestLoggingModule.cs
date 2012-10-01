using System;
using System.Configuration;
using System.Text;
using System.Web;

namespace AppHarbor.RequestTracer
{
	/// <summary>
	/// Implements the request logger.
	/// </summary>
	public class RequestLoggingModule : IHttpModule
	{
		#region Constants
		/// <summary>
		/// Defines the name of the setting which to use to check if this logger is enabled.
		/// </summary>
		private const string LoggerEnabledSettingKey = "appharbor.requesttracer.enabled";
		#endregion
		#region Implementation of IHttpModule
		/// <summary>
		/// Initializes a module and prepares it to handle requests.
		/// </summary>
		/// <param name="context">An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
		public void Init(HttpApplication context)
		{
			// check if the module should be enabled
			var enabled = ConfigurationManager.AppSettings[LoggerEnabledSettingKey];
			if (string.IsNullOrEmpty(enabled) || !(enabled.Equals("true", StringComparison.OrdinalIgnoreCase) || enabled.Equals("1")))
				return;

			// log that we are logging :)
			new LogEvent("Request tracing is enabled").Raise();

			// log every incoming request
			context.BeginRequest += (sender, args) =>
			                        {
			                        	var output = new StringBuilder();
			                        	var httpContext = HttpContext.Current;
			                        	var request = httpContext.Request;
			                        	// write out some request details
			                        	output.AppendLine("### Request variables. look ma, my no special ports and my remote IP");
			                        	output.AppendLine("Request.Url: " + request.Url);
			                        	output.AppendLine("Request.UserHostAddress: " + request.UserHostAddress);
			                        	output.AppendLine("Request.Path: " + request.Path);
			                        	output.AppendLine("Request.PathInfo: " + request.PathInfo);

			                        	output.AppendLine();
			                        	output.AppendLine();

			                        	// dump server variables
			                        	output.AppendLine("### Server variables");
			                        	var serverVariables = request.ServerVariables;
			                        	foreach (var key in serverVariables.AllKeys)
			                        		output.AppendLine(string.Format("{0}: {1}", key, serverVariables[key]));

			                        	// dump request headers
			                        	output.AppendLine("### Request headers");
			                        	var headerVariables = request.Headers;
			                        	foreach (var key in headerVariables.AllKeys)
			                        		output.AppendLine(string.Format("{0}: {1}", key, headerVariables[key]));

			                        	// dump query string
			                        	output.AppendLine("### Querystring (GET)");
			                        	var queryStringVariables = request.QueryString;
			                        	foreach (var key in queryStringVariables.AllKeys)
			                        		output.AppendLine(string.Format("{0}: {1}", key, queryStringVariables[key]));

			                        	// dump POST
			                        	output.AppendLine("### Form variables (POST)");
			                        	var formVariables = request.Form;
			                        	foreach (var key in formVariables.AllKeys)
			                        		output.AppendLine(string.Format("{0}: {1}", key, formVariables[key]));

			                        	// dump Session
			                        	output.AppendLine("### Session");
			                        	var session = httpContext.Session;
			                        	if (session == null)
			                        		output.AppendLine("No session");
			                        	else
			                        	{
			                        		var count = session.Count;
			                        		output.AppendFormat("Count: {0}\r\n", count);
			                        		foreach (string key in session.Keys)
			                        			output.AppendLine(string.Format("{0}: {1}", key, session[key]));
			                        	}

			                        	// dump cookies
			                        	output.AppendLine("### Cookies");
			                        	var cookieCollection = request.Cookies;
			                        	foreach (var key in cookieCollection.AllKeys)
			                        		output.AppendLine(string.Format("{0}: {1}", key, cookieCollection[key]));

			                        	// raise the event
			                        	new LogEvent(output.ToString()).Raise();
			                        };
		}
		/// <summary>
		/// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
		/// </summary>
		public void Dispose()
		{
			// nothing to do here
		}
		#endregion
	}
}