﻿using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {

	[RequireComponent(typeof(Agent))]
	public class InputController : MonoBehaviour {

		protected Agent self;
		
		public bool Initialized { get; protected set; }

		protected float verticalAxis, horizontalAxis;

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
				TriggerA();
			}

			if (Input.GetButton("B")) {
				TriggerB();
			}
		}

		private void SetMovementDirection() {
			float x = horizontalAxis == 0 ? Input.GetAxis("Horizontal") : horizontalAxis;
			float z = verticalAxis == 0 ? Input.GetAxis("Vertical") : verticalAxis;
			ResetAxis();
			self.movementDirection = new Vector3(x, 0, z);
			self.movementDirection = self.movementDirection.normalized;
		}

		void ResetAxis() {
			horizontalAxis = 0f;
			verticalAxis = 0f;
		}
		
		private void OnTriggerEnter(Collider other) {
			Item item = other.GetComponent<Item>();
			if (item == null) {
				return;
			}

			item.ApplyTo(this.self);
		}
		#endregion

		#region Actions
		public void AddToVerticalAxis(float value) {
			verticalAxis += value;
			verticalAxis = Mathf.Min(1, verticalAxis);
		}

		public void AddToHorizontalAxis(float value) {
			horizontalAxis += value;
			horizontalAxis = Mathf.Min(1, horizontalAxis);
		}

		public void TriggerA() {
			self.Inventory.VerbA?.Trigger(self.lookingDirection);
		}

		public void TriggerB() {
			self.Inventory.VerbB?.Trigger(self.lookingDirection);
		}
		#endregion

		#region Debug
		#endregion
	}
}