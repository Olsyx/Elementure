using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Strike : Verb {

		protected const string modifierSheetName = "StrikeSheet";
		
		public Strike(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
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

			float distance = agent.Attributes.AttackDistance * profile.distance;
			switch (modifier) {
				case ModifierTypes.Area:
					StrikeArea();
					break;
				case ModifierTypes.You:
					StrikeYou();
					break;
				case ModifierTypes.Us:
					StrikeUs();
					break;
				default:
					NormalStrike(direction);
					break;
			}
		}
		
		private void NormalStrike(Vector3 direction) {
			agent.Collider.enabled = false;
			RaycastHit hit;
			if (!Physics.Raycast(agent.transform.position, direction, out hit, profile.distance)) {
				agent.Collider.enabled = true;
				return;
			}
			agent.Collider.enabled = true;

			Agent target = hit.rigidbody?.GetComponent<Agent>();
			if (target == null) {
				return;
			}

			target.Damage((int)profile.damage);
		}

		private void StrikeArea() {
			List<Agent> agents = AgentManager.GetAllAgents();

			float damage = profile.damage;

			for (int i = 0; i < agents.Count; i--) {
				if (Vector3.Distance(agent.transform.position, agents[i].transform.position) <= profile.radius) {
					agents[i].Damage((int)damage);
				}
			}
		}
		
		private void StrikeYou() {
			List<Agent> agents = AgentManager.GetAllAgents();
			agents.Remove(this.agent);

			float damage = profile.damage;

			for (int i = 0; i < agents.Count; i++) {
				agents[i].Damage((int)damage);
			}
		}

		private void StrikeUs() {
			List<Agent> agents = AgentManager.GetAllAgents();

			float damage = profile.damage;
			for (int i = 0; i < agents.Count; i++) {
				agents[i].Damage((int)damage);
			}
		}
	}

}