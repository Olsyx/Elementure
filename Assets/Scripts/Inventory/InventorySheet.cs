using Elementure.GameLogic.Words;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Inventories {
	[CreateAssetMenu(fileName = "InventorySheet", menuName = "Elementure/Inventory Sheet", order = 1)]
	public class InventorySheet : ScriptableObject {
		public VerbTypes movementType = VerbTypes.Walk;
		public ModifierTypes movementModifier;

		[Space(10)]
		public VerbTypes verbA;
		public ModifierTypes modifierA;

		[Space(10)]
		public VerbTypes verbB;
		public ModifierTypes modifierB;
	}
}