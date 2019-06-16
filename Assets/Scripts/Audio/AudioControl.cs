using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.Audio {

	public class AudioControl : MonoBehaviour {
		[SerializeField] protected AudioSheet audioSheet;

		protected AudioSource loopSource;
		protected List<AudioSource> sources = new List<AudioSource>();

		private void Start() {
			AudioManager.RegisterControl(this);
			loopSource = NewSource(true);
			sources.Add(NewSource(false));
		}

		public void Play(string id, bool playOnce) {
			AudioClip clip = audioSheet.Get(id);
			if (clip == null) {
				return;
			}

			if (playOnce && IsAudioBeingPlayed(clip)) {
				return;
			}

			AudioSource source = GetFreeSource();
			if (source == null) {
				source = NewSource(false);
				sources.Add(source);
			}

			source.clip = clip;
			source.Play();
		}

		public void Loop(string id) {
			AudioClip clip = audioSheet.Get(id);
			if (clip == null) {
				return;
			}

			loopSource.Stop();
			loopSource.clip = clip;
			loopSource.Play();
		}
		
		protected bool IsAudioBeingPlayed(AudioClip clip) {
			int i = 0;
			while (i < sources.Count && (!sources[i].isPlaying || sources[i].clip != clip)) {
				i++;
			}

			return i < sources.Count;
		}

		protected AudioSource GetFreeSource() {
			int i = 0;
			while (i < sources.Count && sources[i].isPlaying) {
				i++;
			}

			return i < sources.Count ? sources[i] : null;
		}

		protected AudioSource NewSource(bool loop) {
			AudioSource source = gameObject.AddComponent<AudioSource>();
			source.playOnAwake = false;
			source.loop = loop;
			return source;
		}

	}

}