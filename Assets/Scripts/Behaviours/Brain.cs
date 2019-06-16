using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
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
			Verb movementVerb = self.Inventory.VerbMovement;
			float x = Random.Range(-1f, 1f);
			float z = Random.Range(-1f, 1f);

			self.movementDirection = new Vector3(x, 0, z);
			if (self.movementDirection.magnitude > 0) {
				self.lookingDirection = self.movementDirection;
			}

			Vector3 endPoint = movementVerb.GetEndPosition(self.movementDirection);
			TileController tile = GetTileAt(endPoint);
			if (tile == null || !CheckTileAgainstVerb(tile, movementVerb)) {
				return;
			}

			movementVerb.Trigger(self.movementDirection.normalized);
		}

		protected void Follow(Transform target) {
			Verb movementVerb = self.Inventory.VerbMovement;
			float x = target.transform.position.x - transform.position.x;
			float z = target.transform.position.z - transform.position.z;
			self.movementDirection = new Vector3(x, 0, z);
			if (self.movementDirection.magnitude > 0) {
				self.lookingDirection = self.movementDirection;
			}

			/*Vector3 endPoint = movementVerb.GetEndPosition(self.movementDirection);
			TileController tile = GetTileAt(endPoint);
			if (tile == null || !CheckTileAgainstVerb(tile, movementVerb)) {
				return;
			}*/

			self.Inventory.VerbMovement.Execute(self.movementDirection.normalized);
		}
		#endregion

		#region Queries
		public bool IsInSight(Agent target) {
			return target.State == Agent.AgentStates.Alive && Vector3.Distance(target.transform.position, transform.position) < self.Attributes.SightDistance;
		}

		public bool IsInAttackDistance(Transform target) {
			return Vector3.Distance(target.position, transform.position) < self.Attributes.AttackDistance;
		}

		public TileController GetTileAt(Vector3 point) {
			RaycastHit[] hits = Physics.RaycastAll(point, Vector3.down, 5f);

			TileController tile = null;
			int i = 0;
			while (i < hits.Length && (tile = hits[i].collider.GetComponent<TileController>()) == null) {
				i++;
			}
			return tile;
		}

		public bool CheckTileAgainstVerb(TileController tile, Verb verb) {
			return (tile.CurrentState == TileStates.Fire && verb.Modifier == ModifierTypes.Fire)
				   || (tile.CurrentState == TileStates.Water && verb.Modifier == ModifierTypes.Water)
				   || (tile.CurrentState == TileStates.Wind && verb.Modifier == ModifierTypes.Air)
				   || tile.CurrentState == TileStates.Normal;
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