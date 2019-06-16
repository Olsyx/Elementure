using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Others {

	public class DpsDamage : MonoBehaviour {
		[SerializeField] protected float damage;
		[SerializeField] protected float timeGap;

		List<Agent> agents = new List<Agent>();
		List<float> timers = new List<float>();

		#region Control
		private void Update() {
			for (int i = 0; i < timers.Count; i++) {
				TryToDamage(i);
			}
		}

		private void TryToDamage(int index) {
			timers[index] += Time.deltaTime;
			if (timers[index] > 0) {
				return;
			}

			timers[index] = timeGap;
			agents[index].Damage(damage);
		}

		private void Add(Agent agent) {
			agents.Add(agent);
			timers.Add(0);
		}

		private void Remove(Agent agent) {
			int index = agents.IndexOf(agent);
			timers.RemoveAt(index);
			agents.RemoveAt(index);
		}
		#endregion

		#region Collision
		private void OnTriggerEnter(Collider other) {
			Agent agent = other.GetComponent<Agent>();
			if (agent == null) {
				return;
			}
			Add(agent);
		}


		private void OnTriggerExit(Collider other) {
			Agent agent = other.GetComponent<Agent>();
			if (agent == null) {
				return;
			}
			Remove(agent);
		}
		#endregion
	}

}