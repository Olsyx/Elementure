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
			verbText.text = verb.ToString();
		}

		public override void ApplyTo(Agent agent) {
			// TODO: agent.Inventory.OpenVerbQuickMenu();
		}
	}
}