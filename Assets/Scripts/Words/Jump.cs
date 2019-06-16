using Elementure.Audio;
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

			distance = agent.Attributes.WalkDistance * profile.distance;
			horizontalSpeed = profile.speed;
			float time = distance / horizontalSpeed;

			verticalSpeed = (2 * height) / time;
			peakReached = false;

			agent.Body.useGravity = false;

			jumping = true;

			LogToDiary();
			AudioManager.Play("Jump");
		}

		public override void Update() {
			agent.Animator?.SetBool("Jumping", jumping);

			if (!jumping) {
				base.Update();
				return;
			}

			if (agent.Colliding || (peakReached && agent.IsGrounded())) {
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
							: "earth";

			DiaryLogger.Log($"Slimey jumped from the {terrainType.ToLower()}");
		}
	}

}