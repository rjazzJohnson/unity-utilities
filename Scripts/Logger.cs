using UnityEngine;

namespace Conibear {
	using System.Diagnostics;
	using System.Text;

	public static class Logger {
		// To be used in a comparison with the new log being created. If they are the same don't print the same log twice
		private static string preivousLog = string.Empty;

		public static void Log(LogType logType = LogType.Log, string message = "Method has executed.", bool isSinglePrint = false) {
			StackFrame frame = new StackFrame(1);

			StringBuilder log = new StringBuilder();

			string callers_method_name = frame.GetMethod().Name;

			string callers_class_name = frame.GetMethod().DeclaringType.Name;

			// Create signature
			string signature = $"{callers_class_name}:{callers_method_name}-";

			log.Append(signature + message);

			if (isSinglePrint) {
				if (log.ToString() != preivousLog) {
					preivousLog = log.ToString();
					PrintLogByType(logType, log.ToString());
				}
			} else {
				PrintLogByType(logType, log.ToString());
			}
		}

		private static void PrintLogByType(LogType logType, string message) {
			switch (logType) {
				case LogType.Log:
					UnityEngine.Debug.Log(message);
					break;
				case LogType.Warning:
					UnityEngine.Debug.LogWarning(message);
					break;
				case LogType.Error:
					UnityEngine.Debug.LogError(message);
					break;
			}
		}
	}
}