using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Words {

	public enum ModifierTypes {
		None, Fire, Water, Air, Twice, Thrice, Area, Me, You, Us
	}

	[Serializable]
	public class ModifierProfile {
		public float speed = 1f;
		public float damage = 1f;
		public float radius = 1f;
		public GameObject prefab;
	}

	[CreateAssetMenu(fileName = "VerbModifierSheet", menuName = "Elementure/Verb Modifier Sheet", order = 1)]
	public class VerbModifierSheet : ScriptableObject {
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

		public ModifierProfile GetProfile(ModifierTypes type) {
			switch (type) {
				case ModifierTypes.Fire:
					return fire;
				case ModifierTypes.Water:
					return water;
				case ModifierTypes.Air:
					return air;
				case ModifierTypes.Twice:
					return twice;
				case ModifierTypes.Thrice:
					return thrice;
				case ModifierTypes.Area:
					return area;
				case ModifierTypes.Me:
					return me;
				case ModifierTypes.You:
					return you;
				case ModifierTypes.Us:
					return us;
				default:
					return none;
			}
		}
	}

}