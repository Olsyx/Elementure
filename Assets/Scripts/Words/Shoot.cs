using Elementure.Audio;
using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Shoot : Verb {

		protected const string modifierSheetName = "ShootSheet";
		protected const string projectilePrefabName = "Projectile";
		protected const float doubleAngle = 35f;
		protected const float tripleAngle = 60f;

		protected GameObject projectilePrefab;

		public Shoot(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Shoot;
			projectilePrefab = (GameObject) Resources.Load(projectilePrefabName) as GameObject;
		}

		public override void LoadModifierProfile() {
			modifierSheet = VerbManager.LoadProfile(modifierSheetName);
			profile = modifierSheet.GetProfile(Modifier);
		}

		public override void Execute(Vector3 direction) {
			if (agent.State == Agent.AgentStates.Dead) {
				return;
			}

			agent.Animator.SetTrigger("Attack");
						
			if (Modifier == ModifierTypes.Twice) {
				ShootDouble(agent.lookingDirection);
				return;
			}

			if (Modifier == ModifierTypes.Thrice) {
				ShootTriple(agent.lookingDirection);
				return;
			}

			if (Modifier == ModifierTypes.Area) {
				ShootArea(agent.lookingDirection);
				return;
			}

			List<Agent> targets = GetTargets(direction);
			if (targets != null && targets.Count > 0) {
				ShootAt(GetTargets(direction));
				return;
			}

			SpawnProjectile(agent.lookingDirection);
		}

		void ShootDouble(Vector3 direction) {
			List<Agent> targets = new List<Agent>();

			SpawnProjectile(Quaternion.Euler(0, -doubleAngle, 0) * direction);
			SpawnProjectile(Quaternion.Euler(0, doubleAngle, 0) * direction);
		}

		void ShootTriple(Vector3 direction) {
			List<Agent> targets = new List<Agent>();

			SpawnProjectile(Quaternion.Euler(0, -tripleAngle, 0) * direction);
			SpawnProjectile(direction);
			SpawnProjectile(Quaternion.Euler(0, tripleAngle, 0) * direction);
		}

		void ShootArea(Vector3 direction) {
			List<Agent> targets = new List<Agent>();

			SpawnProjectile(Quaternion.Euler(0, -90, 0) * direction);
			SpawnProjectile(direction);
			SpawnProjectile(Quaternion.Euler(0, 90, 0) * direction);
			SpawnProjectile(Quaternion.Euler(0, 180, 0) * direction);
		}

		void ShootAt(List<Agent> targets) {
			for (int i = 0; i < targets.Count; i++) {
				Vector3 delta = targets[i].transform.position - agent.transform.position;
				SpawnProjectile(delta.normalized);
			}
		}

		void SpawnProjectile(Vector3 direction) {
			GameObject projectileObject = GameObject.Instantiate(projectilePrefab);
			projectileObject.transform.position = agent.transform.position + direction * 1.2f;

			Projectile projectile = projectileObject.GetComponent<Projectile>();
			projectile.direction = direction;
			projectile.SetData(Modifier, profile.damage);

			PlayAudio();
		}
		
		public override Vector3 GetEndPosition(Vector3 direction) {
			return agent.transform.position;
		}

		private void PlayAudio() {
			string type = (Modifier == ModifierTypes.Air || Modifier == ModifierTypes.Fire || Modifier == ModifierTypes.Water)
							? Modifier.ToString()
							: "None";

			AudioManager.Play($"Shoot_{type}");
		}
		#region Debug
		public override void DrawGizmos(ModifierTypes modifier, Color color, float size) {
			switch (modifier) {
				case ModifierTypes.Twice:
					DrawDoubleShots(color, size);
					break;
				case ModifierTypes.Thrice:
					DrawTripleShots(color, size);
					break;
				case ModifierTypes.Area:
					DrawTripleShots(color, size);
					break;
				case ModifierTypes.Us:
					break;
				case ModifierTypes.You:
					break;
				default:
					DrawSingleShot(color, size);
					break;
			}
		}

		void DrawArea(Color color) {
			#if UNITY_EDITOR
				UnityEditor.Handles.color = color;
				UnityEditor.Handles.DrawWireDisc(agent.transform.position, Vector3.up, profile.radius);
			#endif
		}

		void DrawSingleShot(Color color, float size) {
			DrawShot(agent.transform.TransformDirection(Vector3.forward), agent.Attributes.AttackDistance * profile.distance, color, size);
		}

		void DrawDoubleShots(Color color, float size) {
			Vector3 forward = agent.transform.TransformDirection(Vector3.forward);
			DrawShot(Quaternion.Euler(0, -doubleAngle, 0) * forward, agent.Attributes.AttackDistance * profile.distance, color, size);
			DrawShot(Quaternion.Euler(0, doubleAngle, 0) * forward, agent.Attributes.AttackDistance * profile.distance, color, size);
		}

		void DrawTripleShots(Color color, float size) {
			Vector3 forward = agent.transform.TransformDirection(Vector3.forward);
			DrawShot(Quaternion.Euler(0, -tripleAngle, 0) * forward, agent.Attributes.AttackDistance * profile.distance, color, size);
			DrawShot(forward, agent.Attributes.AttackDistance * profile.distance, color, size);
			DrawShot(Quaternion.Euler(0, tripleAngle, 0) * forward, agent.Attributes.AttackDistance * profile.distance, color, size);
		}

		void DrawShot(Vector3 direction, float distance, Color color, float size) {
			Vector3 endPoint = agent.transform.position + direction * distance;
			Gizmos.color = color;
			Gizmos.DrawLine(agent.transform.position, endPoint);
			Gizmos.DrawWireSphere(endPoint, size);
		}
		#endregion
	}

}