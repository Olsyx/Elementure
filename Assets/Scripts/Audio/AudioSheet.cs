using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elementure.Audio {
	[Serializable]
	public class AudioEntry {
		public string id;
		public AudioClip clip;
	}

	[CreateAssetMenu(fileName = "AudioSheet", menuName = "Elementure/Audio Sheet", order = 1)]
	public class AudioSheet : ScriptableObject {
		[SerializeField] protected List<AudioEntry> audioEntries;

		public AudioClip Get(string id) {
			return audioEntries.FirstOrDefault(a => a.id.Equals(id))?.clip;
		}
	}

}