using Elementure.Audio;
using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public class Strike : Verb {

		protected const string modifierSheetName = "StrikeSheet";
		protected const string firePrefabName = "Fire";
		protected const string waterPrefabName = "Puddle";

		protected GameObject firePrefab;
		protected GameObject waterPrefab;

		public Strike(ModifierTypes modifier, Agent agent) : base(modifier, agent) {
			Type = VerbTypes.Strike;
			firePrefab = (GameObject)Resources.Load(firePrefabName) as GameObject;
			waterPrefab = (GameObject)Resources.Load(waterPrefabName) as GameObject;
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

			float damage = profile.damage;

			List<Agent> targets = GetTargets(direction);
			for (int i = 0; i < targets.Count; i++) {
				targets[i].Damage(damage);
				AdditionalEffects(targets[i]);
			}

			LogToDiary(targets);
		}

		private void LogToDiary(List<Agent> targets) {
			if (!agent.Id.Equals("Player")) {
				return;
			}

			string target = (targets.Count == 0) ? "the air"
							: (targets.Count == 1) ? "an enemy"
							: "many enemies";

			DiaryLogger.Log($"Slimey struck {target}");
		}

		public override Vector3 GetEndPosition(Vector3 direction) {
			return agent.transform.position;
		}

		private void AdditionalEffects(Agent target) {
			PlayAudio();

			if (Modifier == ModifierTypes.None) {
				return;
			}

			Vector3 direction = target.transform.position - agent.transform.position;

			if (Modifier == ModifierTypes.Air) {
				target.transform.position += direction.normalized * 0.8f;
				return;
			}

			if (Modifier == ModifierTypes.Fire) {
				GameObject fire = GameObject.Instantiate(firePrefab);
				fire.transform.position = target.Feet.position - direction.normalized * 0.2f;
				return;
			}

			if (Modifier == ModifierTypes.Water) {
				GameObject water = GameObject.Instantiate(waterPrefab);
				water.transform.position = target.Feet.position - direction.normalized * 0.2f;
				return;
			}

		}

		private void PlayAudio() {
			string type = (Modifier == ModifierTypes.Air || Modifier == ModifierTypes.Fire || Modifier == ModifierTypes.Water) 
				            ? Modifier.ToString()
							: "None";

			AudioManager.Play($"Strike_{type}");
		}
	}

}