using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public enum VerbTypes {
		None, Walk, Jump, Roll, Teleport, Strike, Shoot, Protect, Throw, Invert
	}

	public abstract class Verb {
		protected VerbSheet modifierSheet;

		public VerbTypes Type { get; protected set; }
		public ModifierTypes Modifier { get; protected set; }

		protected Agent agent;
		protected ModifierProfile profile;
		
		protected float cooldownTimer;

		#region Init
		public Verb(ModifierTypes modifier, Agent agent) {
			this.agent = agent;
			LoadModifierProfile();
		}

		public abstract void LoadModifierProfile();
		#endregion

		#region Control
		public virtual void Update() {
			cooldownTimer -= Time.deltaTime;
		}

		public void SetModifier(ModifierTypes newModifier) {
			Modifier = newModifier;
			LoadModifierProfile();
		}
		#endregion

		#region Actions
		public void Trigger(Vector3 direction) {
			if (cooldownTimer > 0f) {
				return;
			}
			Execute(direction);
			cooldownTimer = agent.Attributes.Cooldown;
		}

		public abstract void Execute(Vector3 direction);
		#endregion

		#region Queries
		public bool IsMovementType() {
			return Type == VerbTypes.Walk || Type == VerbTypes.Jump || Type == VerbTypes.Roll;
		}

		protected List<Agent> GetTargets(Vector3 direction) {
			switch (Modifier) {
				case ModifierTypes.Area:
					return GetTargetsInArea();

				case ModifierTypes.You:
					return GetYou();

				case ModifierTypes.Us:
					return GetUs();
			}

			Agent target = GetTarget(direction);
			return target != null ? new List<Agent>() { target } : new List<Agent>();
		}

		protected Agent GetTarget(Vector3 direction) {
			agent.Collider.enabled = false;

			float distance = agent.Attributes.AttackDistance * profile.distance;

			RaycastHit hit;
			if (!Physics.Raycast(agent.transform.position, direction, out hit, distance)) {
				agent.Collider.enabled = true;
				return null;
			}
			agent.Collider.enabled = true;

			return hit.rigidbody?.GetComponent<Agent>(); ;
		}

		protected List<Agent> GetTargetsInArea() {
			List<Agent> agents = AgentManager.GetAllAgents();
			for (int i = agents.Count - 1; i >= 0; i--) {
				if (Vector3.Distance(agent.transform.position, agents[i].transform.position) > profile.radius) {
					agents.RemoveAt(i);
				}
			}
			return agents;
		}

		protected List<Agent> GetYou() {
			List<Agent> agents = AgentManager.GetAllAgents();
			agents.Remove(this.agent);
			return agents;
		}

		protected List<Agent> GetUs() {
			return AgentManager.GetAllAgents();
		}
		#endregion
	}

}