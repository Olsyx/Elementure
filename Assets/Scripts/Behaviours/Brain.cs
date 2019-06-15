using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Behaviours {

	[RequireComponent(typeof(Agent))]
	public abstract class Brain : MonoBehaviour {

		protected Agent self;
		public bool Initialized { get; protected set; }
		
		#region Init
		private void Awake() {
			Initialized = false;
			StoreComponents();
			Setup();
		}

		protected virtual void StoreComponents() {
			self = GetComponent<Agent>();
		}

		protected virtual void Setup() {
		}

		private void Start() {
			StoreReferences();
			Init();
			Initialized = true;
		}

		protected virtual void StoreReferences() {

		}

		protected virtual void Init() {
		}
		#endregion

		#region Control
		private void Update() {
			if (self.State == Agent.AgentStates.Dead) {
				return;
			}

			ExecuteBehaviour();
		}

		protected abstract void ExecuteBehaviour();
		#endregion

		#region Actions
		protected void Idle() {
			float x = Random.Range(-1f, 1f);
			float z = Random.Range(-1f, 1f);
			Vector3 movementDirection = new Vector3(x, 0, z);
			self.Inventory.VerbMovement.Trigger(movementDirection.normalized);
		}

		protected void Follow(Transform target) {
			float x = target.transform.position.x - transform.position.x;
			float z = target.transform.position.z - transform.position.z;
			Vector3 movementDirection = new Vector3(x, 0, z);
			self.Inventory.VerbMovement.Execute(movementDirection.normalized);
		}
		#endregion

		#region Queries
		public bool IsInSight(Transform target) {
			return Vector3.Distance(target.position, transform.position) < self.Attributes.SightDistance;
		}

		public bool IsInAttackDistance(Transform target) {
			return Vector3.Distance(target.position, transform.position) < self.Attributes.AttackDistance;
		}

		#endregion

		#region Debug

		private void OnDrawGizmos() {
			if (!Application.isPlaying) {
				StoreComponents();
			}

			#if UNITY_EDITOR
				UnityEditor.Handles.color = Color.white;
				UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, self.Attributes.SightDistance);
			#endif
		}

		#endregion
	}

}