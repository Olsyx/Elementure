﻿using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Roll : Verb {

		protected const string modifierSheetName = "RollSheet";

		protected bool rolling;

		protected Vector3 startingPoint;
		protected Vector3 direction;
		protected float distance;
		protected float speed;

		public Roll(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Roll;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(modifier);
		}

		public override void Execute(Vector3 direction) {
			if (rolling || agent.State == Agent.AgentStates.Dead) {
				return;
			}

			cooldownTimer = agent.Attributes.Cooldown;
			startingPoint = agent.transform.position;
			this.direction = direction.normalized;

			distance = profile.distance;
			speed = profile.speed;

			rolling = true;
		}

		public override void Update() {
			if (!rolling) {
				base.Update();
				return;
			}

			rolling = Vector3.Distance(agent.transform.position, startingPoint) < distance;
			agent.transform.position += direction * speed * Time.deltaTime;
		}

	}

}