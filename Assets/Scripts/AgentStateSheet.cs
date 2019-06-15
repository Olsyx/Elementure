using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	[CreateAssetMenu(fileName = "AgentStateSheet", menuName = "Elementure/Agent State Sheet", order = 1)]
	public class AgentStateSheet : ScriptableObject {
		[SerializeField] protected string id;
		[SerializeField] protected Modifier raceModifier;
		[SerializeField] protected int maxHealth;
		[SerializeField] protected int speed;
		[SerializeField] protected float cooldown;

	}

}