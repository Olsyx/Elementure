using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {

	[RequireComponent(typeof(Agent))]
	public class InputController : MonoBehaviour {

		protected Agent self;

		public bool Initialized { get; protected set; }
		Vector3 movementDirection;

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

			ProcessInput();
			self.Inventory?.Move(movementDirection);
		}

		private void ProcessInput() {
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");
			movementDirection = new Vector3(x, 0, z);
		}
		#endregion

		#region Debug
		#endregion
	}
}