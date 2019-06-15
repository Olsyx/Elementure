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

		public override void Execute(Vector3 direction) {
			if (agent.State == Agent.AgentStates.Dead) {
				return;
			}

			agent.transform.position += direction * agent.Attributes.Speed * Time.deltaTime;
		}

		public override void LoadModifier() {
			modifierSheet = VerbSheetLoader.Load(modifierSheetName);
			currentProfile = modifierSheet.GetProfile(modifier);
		}
	}

}