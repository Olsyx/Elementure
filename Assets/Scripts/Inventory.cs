using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {
	[RequireComponent(typeof(Agent))]
	public class Inventory : MonoBehaviour {

		[SerializeField] protected InventorySheet initialInventory;

		protected Agent agent;
		public Verb VerbMovement { get; protected set; }
		public Verb VerbA { get; protected set; }
		public Verb VerbB { get; protected set; }

		public bool Initialized { get; protected set; }

		#region Init
		private void Awake() {
			Initialized = false;
			StoreComponents();
			Setup();
		}

		private void StoreComponents() {
			agent = GetComponent<Agent>();
		}

		private void Setup() {
			VerbMovement = VerbManager.GetVerb(agent, initialInventory.movementType, initialInventory.movementModifier);
			VerbMovement = VerbMovement ?? new Walk(ModifierTypes.None, agent);
			VerbA = VerbManager.GetVerb(agent, initialInventory.verbA, initialInventory.modifierA);
			VerbB = VerbManager.GetVerb(agent, initialInventory.verbB, initialInventory.modifierB);
		}

		private void Start() {
			StoreReferences();
			Init();
			Initialized = true;
		}

		private void StoreReferences() {

		}

		private void Init() {
		}
		#endregion

		#region Control
		private void Update() {
			VerbMovement?.Update();
			VerbA?.Update();
			VerbB?.Update();
		}
		#endregion

		#region Actions

		#endregion

		#region Queries
		#endregion

		#region Debug
		#endregion
	}
}