using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Agents {

	public static class AgentManager {

		private static Dictionary<string, Agent> agents = new Dictionary<string, Agent>();

		public static void Register(Agent agent) {
			if (agents.ContainsKey(agent.Id)) {
				Debug.LogError($"{agent.Id} already exists!");
				return;
			}

			agents.Add(agent.Id, agent);
		}

		public static void Remove(Agent agent) {
			agents.Remove(agent.Id);
		}

		public static Agent GetAgent(string id) {
			return agents[id];
		}

		public static List<Agent> GetAllAgents() {
			return agents.Values.ToList();
		}

	}

}