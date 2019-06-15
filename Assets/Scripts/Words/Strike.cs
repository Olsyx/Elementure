using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Strike : Verb {

		protected const string modifierSheetName = "StrikeSheet";
		
		public Strike(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Strike;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(Modifier);
		}

		public override void Execute(Vector3 direction) {
			if (agent.State == Agent.AgentStates.Dead) {
				return;
			}

			agent.Animator.SetTrigger("Attack");

			float damage = profile.damage;

			List<Agent> targets = GetTargets(direction);
			for (int i = 0; i < targets.Count; i++) {
				targets[i].Damage(damage);
			}
		}
		
	}

}