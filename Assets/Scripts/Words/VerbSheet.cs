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
	}
	
	public class VerbSheet {
		public ModifierProfile none = new ModifierProfile();
		public ModifierProfile fire = new ModifierProfile();
		public ModifierProfile water = new ModifierProfile();
		public ModifierProfile air = new ModifierProfile();
		public ModifierProfile twice = new ModifierProfile();
		public ModifierProfile thrice = new ModifierProfile();
		public ModifierProfile area = new ModifierProfile();
		public ModifierProfile me = new ModifierProfile();
		public ModifierProfile you = new ModifierProfile();
		public ModifierProfile us = new ModifierProfile();

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