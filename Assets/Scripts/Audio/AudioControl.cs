using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.Audio {

	public class AudioControl : MonoBehaviour {
		[SerializeField] protected AudioSheet audioSheet;

		protected List<AudioSource> sources = new List<AudioSource>();

		private void Start() {
			AudioManager.RegisterControl(this);
			sources = GetComponents<AudioSource>().ToList();
			if (sources.Count <= 0) {
				sources.Add(NewSource());
			}
		}

		public void Play(string id) {
			AudioClip clip = audioSheet.Get(id);
			if (clip == null) {
				return;
			}

			AudioSource source = GetFreeSource();
			if (source == null) {
				source = NewSource();
				sources.Add(source);
			}

			source.clip = clip;
			source.Play();
		}

		protected AudioSource GetFreeSource() {
			int i = 0;
			while (i < sources.Count && sources[i].isPlaying) {
				i++;
			}

			return i < sources.Count ? sources[i] : null;
		}

		protected AudioSource NewSource() {
			AudioSource source = gameObject.AddComponent<AudioSource>();
			source.playOnAwake = false;
			source.loop = false;
			return source;
		}
	}

}