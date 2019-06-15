using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public enum VerbTypes {
		Walk, Jump, Roll, Teleport, Strike, Shoot, Protect, Throw, Invert
	}

	public abstract class Verb : ScriptableObject {
		protected VerbSheet modifierSheet;

		public VerbTypes Type { get; protected set; }

		protected Agent agent;
		protected ModifierTypes modifier;
		protected ModifierProfile currentProfile;


		public Verb(ModifierTypes modifier, Agent agent) {
			this.agent = agent;
		}

		public abstract void LoadModifier();

		public abstract void Execute(Vector3 direction);

	}

}