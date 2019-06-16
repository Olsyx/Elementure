using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Items;
using Elementure.GameLogic.Words;
using Elementure.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Inventories {
	[RequireComponent(typeof(Agent))]
	public class Inventory : MonoBehaviour {

		[SerializeField] protected InventorySheet initialInventory;
		[SerializeField] protected GameObject verbItemPrefab;
		[SerializeField] protected GameObject modifierItemPrefab;

		protected Agent agent;
		public Verb VerbMovement { get; protected set; }
		public Verb VerbA { get; protected set; }
		public Verb VerbB { get; protected set; }

		public InventoryGUI Gui { get; protected set; }

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

		public void GiveGuiControl(InventoryGUI guiControl) {
			Gui = guiControl;
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
		public void Drop() {
			GameObject spawnedMovement = Spawn(VerbMovement);
			spawnedMovement.transform.position = transform.position + Vector3.back;

			GameObject spawnedA = Spawn(VerbA);
			if (spawnedA != null) {
				spawnedA.transform.position = transform.position + Vector3.left;
			}

			GameObject spawnedB = Spawn(VerbB);
			if (spawnedB != null) {
				spawnedB.transform.position = transform.position + Vector3.right;
			}
		}

		private GameObject Spawn(Verb verb) {
			if (verb == null) {
				return null;
			}

			int coin = Random.Range(0, 1000) > 500 ? 1 : 0;
			return (coin == 1) ? SpawnModifierItem(verb.Modifier) : SpawnVerbItem(verb.Type);
		}

		private GameObject SpawnVerbItem(VerbTypes verb) {
			GameObject verbObject = Instantiate(verbItemPrefab);
			VerbItem verbItem = verbObject.GetComponent<VerbItem>();
			verbItem.SetVerb(verb);
			return verbObject;
		}

		private GameObject SpawnModifierItem(ModifierTypes modifier) {
			GameObject modifierObject = Instantiate(modifierItemPrefab);
			ModifierItem modifierItem = modifierObject.GetComponent<ModifierItem>();
			modifierItem.SetModifier(modifier);
			return modifierObject;
		}

		public void ChangeMovementVerb(VerbTypes verb) {
			VerbMovement = VerbManager.GetVerb(agent, verb, VerbMovement.Modifier);
		}

		public void ChangeMovementModifier(ModifierTypes modifier) {
			VerbMovement.SetModifier(modifier);
		}

		public void ChangeVerbA(VerbTypes verb) {
			VerbA = VerbManager.GetVerb(agent, verb, VerbA.Modifier);
		}

		public void ChangeModifierA(ModifierTypes modifier) {
			VerbA.SetModifier(modifier);
		}

		public void ChangeVerbB(VerbTypes verb) {
			VerbB = VerbManager.GetVerb(agent, verb, VerbB.Modifier);
		}

		public void ChangeModifierB(ModifierTypes modifier) {
			VerbB.SetModifier(modifier);
		}
		#endregion

		#region Queries
		#endregion

		#region Debug
		#endregion
	}
}