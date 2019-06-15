using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Roll : Verb {

		protected const string modifierSheetName = "RollSheet";

		public Roll(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Teleport;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(modifier);
		}

		public override void Execute(Vector3 direction) {
			if (agent.State == Agent.AgentStates.Dead) {
				return;
			}

			// TODO
		}
	}

}