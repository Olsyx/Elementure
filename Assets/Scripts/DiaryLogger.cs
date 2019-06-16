using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {

	public static class DiaryLogger {

		private static List<string> logs = new List<string>();

		public static void Log(string newLog) {
			logs.Add(newLog);
		}

		public static string GetLastLog() {
			return logs.Count > 0 ? logs[logs.Count - 1] : "";
		}
	}

}