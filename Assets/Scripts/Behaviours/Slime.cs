using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Behaviours {

	[RequireComponent(typeof(Agent))]
	public class Slime : Brain {

		protected Agent player;
		protected override void StoreReferences() {
			player = FindObjectOfType<InputController>().GetComponent<Agent>();
		}

		protected override void ExecuteBehaviour() {
			if (!IsInSight(player)) {
				Idle();
				return;
			}

			if (!IsInAttackDistance(player.transform)) {
				Follow(player.transform);
				return;
			}

			Vector3 direction = player.transform.position - transform.position;
			self.Inventory.VerbA?.Trigger(direction.normalized);
		}
	}

}