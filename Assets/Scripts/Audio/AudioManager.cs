using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.Audio {
	public static class AudioManager {

		private static AudioControl audioControl;

		public static void RegisterControl(AudioControl control) {
			audioControl = control;
		}

		public static void Play(string id) {
			audioControl?.Play(id);
		}

		public static void Loop(string id) {
			audioControl?.Loop(id);
		}
	}
}