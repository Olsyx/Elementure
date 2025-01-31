﻿using Elementure.Audio;
using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Walk : Verb {
		protected const string modifierSheetName = "WalkSheet";
		
		protected bool walking;

		protected Vector3 startingPoint;
		protected Vector3 direction;
		protected float distance;
		protected float speed;

		public Walk(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Walk;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(Modifier);
		}
		
		public override void Execute(Vector3 direction) {
			if (walking || agent.State == Agent.AgentStates.Dead) {
				return;
			}

			cooldownTimer = agent.Attributes.Cooldown;
			startingPoint = agent.transform.position;
			this.direction = direction.normalized;

			distance = agent.Attributes.WalkDistance * profile.distance;
			speed = agent.Attributes.Speed * profile.speed;

			LogToDiary();

			walking = true;
		}

		public override void Update() {
			agent.Animator?.SetBool("Walking", walking);

			if (!walking) {
				base.Update();
				return;
			}

			agent.transform.position += direction * speed * Time.deltaTime;
			PlayAudio();

			walking = !agent.Colliding && Vector3.Distance(agent.transform.position, startingPoint) < distance;
		}

		public override Vector3 GetEndPosition(Vector3 direction) {
			float distance = agent.Attributes.WalkDistance * profile.distance;
			return agent.transform.position + direction * distance;
		}

		private void LogToDiary() {
			if (!agent.Id.Equals("Player")) {
				return;
			}

			string terrainType = (Modifier == ModifierTypes.Air || Modifier == ModifierTypes.Fire || Modifier == ModifierTypes.Water)
							? Modifier.ToString()
							: "land";

			DiaryLogger.Log($"Slimey started to walk through the {terrainType.ToLower()}");
		}
		
		private void PlayAudio() {
			TileController tile = agent.GetTile();
			string type = tile != null && (tile.CurrentState == TileStates.Wind || tile.CurrentState == TileStates.Fire || tile.CurrentState == TileStates.Water)
							? tile.CurrentState.ToString()
							: "None";

			AudioManager.Play($"Movement_{type}", true);
		}

	}

}