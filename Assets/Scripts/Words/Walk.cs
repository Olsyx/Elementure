using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Walk : Verb {
		protected const string modifierSheetName = "WalkSheet";

		public Walk(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Walk;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(modifier);
		}

		public override void Execute(Vector3 direction) {
			if (agent.State == Agent.AgentStates.Dead) {
				return;
			}

			agent.transform.position += direction * agent.Attributes.Speed * Time.deltaTime;
			cooldownTimer = agent.Attributes.Cooldown;
		}
	}

}