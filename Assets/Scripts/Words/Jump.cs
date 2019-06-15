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
		protected float height = 1.5f;
		protected float distance;
		protected float horizontalSpeed;
		protected float verticalSpeed;

		public Jump(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Jump;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(Modifier);
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

			agent.Body.useGravity = false;

			jumping = true;
		}

		public override void Update() {
			agent.Animator?.SetBool("Jumping", jumping);

			if (!jumping) {
				base.Update();
				return;
			}

			if (peakReached && agent.IsGrounded()) {
				EndJump();
			}

			agent.transform.position += jumpingDirection * horizontalSpeed * 0.5f * Time.deltaTime;

			if (peakReached) {
				return;
			}

			agent.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
			peakReached = (agent.transform.position.y - jumpingPoint.y) >= height;
			agent.Body.useGravity = peakReached;
		}

		private void EndJump() {
			jumping = false;
			cooldownTimer = agent.Attributes.Cooldown;
		}

	}

}