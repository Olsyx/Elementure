using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GUI { 
	public class EndGUI : MenuElement {

		[SerializeField] protected GameObject gameWonText;
		[SerializeField] protected GameObject gameOverText;

		private void Start() {
			gameWonText.SetActive(false);
			gameOverText.SetActive(false);
		}

		public void ShowGameWon() {
			Show();
			gameWonText.SetActive(true);
			gameOverText.SetActive(false);
		}

		public void ShowGameOver() {
			Show();
			gameWonText.SetActive(false);
			gameOverText.SetActive(true);
		}
	}
}