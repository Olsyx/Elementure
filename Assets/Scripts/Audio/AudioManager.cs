using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.Audio {
	public static class AudioManager {

		private static AudioControl audioControl;

		public static void RegisterControl(AudioControl control) {
			audioControl = control;
		}

		public static void Play(string id, bool playOnce = false) {
			audioControl?.Play(id, playOnce);
		}

		public static void Loop(string id) {
			audioControl?.Loop(id);
		}
	}
}