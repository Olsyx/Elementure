using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {
	public static class GameMaster {

		public static bool Paused { get; private set; }

		public static void PauseGame() {
			Paused = true;
		}

		public static void ResumeGame() {
			Paused = false;
		}
	}
}