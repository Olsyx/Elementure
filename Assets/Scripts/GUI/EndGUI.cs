using Elementure.Audio;
using Elementure.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GUI { 
	public class EndGUI : MenuElement {

		[SerializeField] protected GameObject gameWonText;
		[SerializeField] protected GameObject gameOverText;
		[SerializeField] protected Text endLog;
		[SerializeField] protected string wonAudioId = "WonAudio";
		[SerializeField] protected string lostAudioId = "LostAudio";

		private void Awake() {
			gameWonText.SetActive(false);
			gameOverText.SetActive(false);
		}

		public void ShowGameWon() {
			Show();
			gameWonText.SetActive(true);
			gameOverText.SetActive(false);
			ShowLog();
			AudioManager.Play(wonAudioId);
		}

		public void ShowGameOver() {
			Show();
			gameWonText.SetActive(false);
			gameOverText.SetActive(true);
			ShowLog();
			AudioManager.Play(lostAudioId);
		}

		void ShowLog() {
			List<string> diary = DiaryLogger.GetLogSummary();
			endLog.text = "";
			for (int i = 0; i < diary.Count; i++) {
				endLog.text += "\n" + diary[i];
			}
		}
	}
}