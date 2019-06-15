using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Jump : Verb {

		protected const string modifierSheetName = "JumpSheet";

		protected bool jumping;

		protected Vector3 jumpingPoint;
		protected Vector3 jumpingDirection;
		protected bool peakReached;
		protected float height = 3f;
		protected float distance;
		protected float horizontalSpeed;
		protected float verticalSpeed;

		public Jump(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Teleport;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(modifier);
		}

		public override void Execute(Vector3 direction) {
			if (jumping || agent.State == Agent.AgentStates.Dead) {
				return;
			}

			jumpingPoint = agent.transform.position;
			jumpingDirection = direction.normalized;

			distance = profile.distance;
			horizontalSpeed = profile.speed;
			float time = distance / horizontalSpeed;

			verticalSpeed = (2 * height) / time;
			peakReached = false;

			agent.Body.isKinematic = true;
			agent.Body.useGravity = false;

			jumping = true;
		}

		public override void Update() {
			if (!jumping) {
				base.Update();
				return;
			}

			if (agent.IsGrounded()) {
				EndJump();
			}

			Vector3 movement = jumpingDirection * horizontalSpeed;
			movement += (peakReached ? Vector3.down : Vector3.up) * verticalSpeed;

			agent.transform.position += movement * Time.deltaTime;
		}

		private void EndJump() {
			jumping = false;
			cooldownTimer = agent.Attributes.Cooldown;
			agent.Body.isKinematic = false;
			agent.Body.useGravity = true;
		}

	}

}