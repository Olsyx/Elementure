﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure {
	[CreateAssetMenu(fileName = "AgentAttributeSheet", menuName = "Elementure/Agent Attribute Sheet", order = 1)]
	public class AgentAttributeSheet : ScriptableObject {
		//[SerializeField] protected Modifier raceModifier;
		[SerializeField] protected int maxHealth = 10;
		[SerializeField] protected int speed = 1;
		[SerializeField] protected float cooldown = 3;

		public int MaxHealth { get => maxHealth; }
		public int Speed { get => speed; }
		public float Cooldown { get => cooldown; }
	}

}