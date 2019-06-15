using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {

	[RequireComponent(typeof(Agent))]
	public class InputController : MonoBehaviour {

		protected Agent self;

		public bool Initialized { get; protected set; }

		#region Init
		private void Awake() {
			Initialized = false;
			StoreComponents();
			Setup();
		}

		private void StoreComponents() {
			self = GetComponent<Agent>();
		}

		private void Setup() {
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
			if (self.State == Agent.AgentStates.Dead) {
				return;
			}

			SetMovementDirection();

			if (self.movementDirection.magnitude > 0.001f) {
				self.lookingDirection = self.movementDirection;
				self.Inventory?.VerbMovement.Execute(self.movementDirection);
				//self.Inventory.quickMenu.Close()
			} 

			if (Input.GetButton("A")) {
				self.Inventory.VerbA?.Trigger(self.lookingDirection);
			}

			if (Input.GetButton("B")) {
				self.Inventory.VerbB?.Trigger(self.lookingDirection);
			}
		}

		private void SetMovementDirection() {
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");
			self.movementDirection = new Vector3(x, 0, z);
			self.movementDirection = self.movementDirection.normalized;
		}

		private void OnTriggerEnter(Collider other) {
			Item item = other.GetComponent<Item>();
			if (item == null) {
				return;
			}

			item.ApplyTo(this.self);

			// Word => self.Inventory.quickMenu.Open(wordType)
		}

		private void OnTriggerExit(Collider other) {
			//self.Inventory.quickMenu.Close();
		}
		#endregion

		#region Debug
		#endregion
	}
}