using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
	[Serializable]
	public class ModifierProfile {
		public float speed = 1f;
		public float damage = 1f;
		public float radius = 1f;
		public GameObject prefab;
	}

	[CreateAssetMenu(fileName = "VerbModifiers", menuName = "Elementure/Verb Modifiers", order = 1)]
	public class VerbModifiers : ScriptableObject {
		[SerializeField] protected VerbTypes verbType;
		[SerializeField] protected ModifierProfile none;
		[SerializeField] protected ModifierProfile fire;
		[SerializeField] protected ModifierProfile water;
		[SerializeField] protected ModifierProfile air;
		[SerializeField] protected ModifierProfile twice;
		[SerializeField] protected ModifierProfile thrice;
		[SerializeField] protected ModifierProfile area;
		[SerializeField] protected ModifierProfile me;
		[SerializeField] protected ModifierProfile you;
		[SerializeField] protected ModifierProfile us;
	}

}