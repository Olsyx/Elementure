using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GUI {
	public class PlayerStateGUI : MonoBehaviour {
		[SerializeField] protected Slider lifeSlider;
		[SerializeField] protected Text speciesTxt;

		Agent player;

		private void Start() {
			speciesTxt.text = "SLIME";
		}

		public void StorePlayer() {
			player = AgentManager.GetAgent("Player");
			lifeSlider.maxValue = player.Attributes.MaxHealth;
		}

		private void Update() {
			if (player == null) {
				return;
			}

			lifeSlider.value = player.CurrentHealth;
		}

	}
}
