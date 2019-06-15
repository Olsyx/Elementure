using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Shoot : Verb {

		protected const string modifierSheetName = "ShootSheet";

		public Shoot(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Shoot;
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

			List<Agent> targets = Modifier == ModifierTypes.Twice ? GetDoubleTargets(direction)
								  : Modifier == ModifierTypes.Thrice ? GetTripleTargets(direction)
								  : GetTargets(direction);

			ShootAt(GetTargets(direction));
		}

		List<Agent> GetDoubleTargets(Vector3 direction) {
			List<Agent> targets = new List<Agent>();

			Agent rightTarget = GetTarget(Quaternion.Euler(0, -45, 0) * direction);
			if (rightTarget != null) {
				targets.Add(rightTarget);
			}

			Agent leftTarget = GetTarget(Quaternion.Euler(0, 45, 0) * direction);
			if (leftTarget != null) {
				targets.Add(leftTarget);
			}

			return targets;
		}

		List<Agent> GetTripleTargets(Vector3 direction) {
			List<Agent> targets = new List<Agent>();

			Agent rightTarget = GetTarget(Quaternion.Euler(0, -60, 0) * direction);
			if (rightTarget != null) {
				targets.Add(rightTarget);
			}

			Agent frontTarget = GetTarget(direction);
			if (frontTarget != null) {
				targets.Add(frontTarget);
			}

			Agent leftTarget = GetTarget(Quaternion.Euler(0, 60, 0) * direction);
			if (leftTarget != null) {
				targets.Add(leftTarget);
			}

			return targets;
		}

		void ShootAt(List<Agent> targets) {

			for (int i = 0; i < targets.Count; i++) {
				//SpawnProjectile(targets[i]);
			}
		}


		void SpawnProjectile() {

		}
	}

}