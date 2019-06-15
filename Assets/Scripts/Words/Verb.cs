using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public enum VerbTypes {
		Walk, Jump, Roll, Teleport, Strike, Shoot, Protect, Throw, Invert
	}

	public abstract class Verb : ScriptableObject {
		protected VerbModifierSheet modifiers;
		protected string modifierSheetPath = "Verb Sheets/";
		protected string modifierSheetName = "";

		public VerbTypes Type { get; protected set; }

		protected Agent agent;
		protected ModifierTypes currentModifier;
		protected ModifierProfile modifierProfile;


		public Verb(ModifierTypes modifier, Agent agent) {
			this.agent = agent;
			modifiers = ScriptableObject.CreateInstance<VerbModifierSheet>();

			modifierProfile = modifiers.GetProfile(modifier);
		}

		public abstract void Execute(Vector3 direction);

	}

}