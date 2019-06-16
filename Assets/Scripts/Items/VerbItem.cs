using System.Collections;
using System.Collections.Generic;
using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GameLogic.Items {

	public class VerbItem : Item {
		public VerbTypes verb;
		public Text verbText;

		private void Awake() {
			UpdateGUI();
		}

		public void SetVerb(VerbTypes verb) {
			this.verb = verb;
			UpdateGUI();
		}

		private void UpdateGUI() {
			verbText.text = verb.ToString();
		}

		public override void ApplyTo(Agent agent) {
			agent.Inventory.Gui.OpenToChoose(verb);
			Destroy(this.gameObject);
		}
	}
}