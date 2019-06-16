using Elementure.GameLogic;
using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GUI {
	public class PlayerStateGUI : MenuElement {
		[SerializeField] protected Slider lifeSlider;
		[SerializeField] protected Text speciesTxt;
		[SerializeField] protected Text diary;
		
		public Text DiaryLog { get => diary; }

		private void Start() {
			speciesTxt.text = "SLIME";
			diary.text = "";
		}

		public override void StorePlayer(Agent player) {
			base.StorePlayer(player);
			lifeSlider.maxValue = player.Attributes.MaxHealth;
		}

		private void Update() {
			if (player == null || blocked) {
				return;
			}

			lifeSlider.value = player.CurrentHealth;
			diary.text = DiaryLogger.GetLastLog();
		}

		public override void Show() {
			lifeSlider.gameObject.SetActive(true);
			speciesTxt.gameObject.SetActive(true);
			diary.gameObject.SetActive(true);
		}

		public override void Hide() {
			lifeSlider.gameObject.SetActive(false);
			speciesTxt.gameObject.SetActive(false);
			diary.gameObject.SetActive(false);
		}
	}
}
