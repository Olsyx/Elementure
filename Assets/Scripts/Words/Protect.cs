using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Protect : Verb {

		protected const string modifierSheetName = "ProtectSheet";

		public Protect(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Teleport;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(Modifier);
		}

		public override void Execute(Vector3 direction) {
			if (agent.State == Agent.AgentStates.Dead) {
				return;
			}

			// TODO
		}

		public override Vector3 GetEndPosition(Vector3 direction) {
			return agent.transform.position;
		}
	}

}