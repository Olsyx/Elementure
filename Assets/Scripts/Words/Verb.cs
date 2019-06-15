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

		protected Agent agent;
		protected ModifierTypes modifier;
		protected ModifierProfile profile;
		
		protected float cooldownTimer;

		public Verb(ModifierTypes modifier, Agent agent) {
			this.agent = agent;
			LoadModifierProfile();
		}
		
		public abstract void LoadModifierProfile();

		public virtual void Update() {
			cooldownTimer -= Time.deltaTime;
		}

		public void Trigger(Vector3 direction) {
			if (cooldownTimer > 0f) {
				return;
			}
			Execute(direction);
			cooldownTimer = agent.Attributes.Cooldown;
		}

		public abstract void Execute(Vector3 direction);

		public void SetModifier(ModifierTypes newModifier) {
			modifier = newModifier;
			LoadModifierProfile();
		}

		public bool IsMovementType() {
			return Type == VerbTypes.Walk || Type == VerbTypes.Jump || Type == VerbTypes.Roll;
		}
	}

}