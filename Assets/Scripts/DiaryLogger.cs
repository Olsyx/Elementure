using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {

	public static class DiaryLogger {

		private static List<string> logs = new List<string>();

		public static void Log(string newLog) {
			if (GetLastLog().Equals(newLog)) {
				return;
			}
			logs.Add(newLog);
		}

		public static string GetLastLog() {
			return logs.Count > 0 ? logs[logs.Count - 1] : "";
		}

		public static List<string> GetLogSummary() {
			List<string> diary = new List<string>();
			if (logs.Count > 6) {
				diary.Add(logs[0]);
				diary.Add("... after many obstacles...");
			}

			int startIndex = Mathf.Max(0, logs.Count - 5);
			for (int i = startIndex; i < logs.Count; i++) {
				diary.Add(logs[i]);
			}

			return diary;
		}
	}

}