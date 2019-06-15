﻿using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Throw : Verb {

		protected const string modifierSheetName = "ThrowSheet";

		public Throw(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Throw;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(modifier);
		}

		public override void Execute(Vector3 direction) {
			if (agent.State == Agent.AgentStates.Dead) {
				return;
			}

			// TODO
		}
	}

}