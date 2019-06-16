using Elementure.GameLogic.Words;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GUI { 
	public class InventoryGUI : MenuElement {

		[SerializeField] protected Text movementVerbText;
		[SerializeField] protected Text movementModifierText;
		[SerializeField] protected Text verbAText;
		[SerializeField] protected Text modifierAText;
		[SerializeField] protected Text verbBText;
		[SerializeField] protected Text modifierBText;

		private void Update() {
			if (player == null) {
				return;
			}
			UpdateTexts();
		}

		private void UpdateTexts() {
			UpdateVerbText(player.Inventory.VerbMovement, movementVerbText, movementModifierText);
			UpdateVerbText(player.Inventory.VerbA, verbAText, modifierAText);
			UpdateVerbText(player.Inventory.VerbB, verbBText, modifierBText);
		}

		private void UpdateVerbText(Verb verb, Text verbText, Text modifierText) {
			verbText.text = verb != null ? verb.Type.ToString() : "None";
			modifierText.text = verb != null ? verb.Modifier.ToString() : "None";
		}
	}
}