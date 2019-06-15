using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GameLogic.Items {

	public class ModifierItem : Item {
		public ModifierTypes modifier;
		public Text modifierText;

		private void Awake() {
			UpdateGUI();
		}

		public void SetModifier(ModifierTypes modifier) {
			this.modifier = modifier;
			UpdateGUI();
		}

		private void UpdateGUI() {
			modifierText.text = modifier.ToString();
		}

		public override void ApplyTo(Agent agent) {
			// TODO: agent.Inventory.OpenVerbQuickMenu();
		}

	}
}