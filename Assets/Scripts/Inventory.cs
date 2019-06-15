using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {
	[RequireComponent(typeof(Agent))]
	public class Inventory : MonoBehaviour {

		[SerializeField] protected int maxVerbs = 3;

		protected Agent agent;
		protected Verb movementVerb;
		protected List<Verb> verbs = new List<Verb>();

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
			movementVerb = new Walk(ModifierTypes.None, agent);
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
		#endregion

		#region Actions
		public void Move(Vector3 movementDirection) {
			movementVerb.Execute(movementDirection);
		}
		#endregion

		#region Queries
		#endregion

		#region Debug
		#endregion
	}
}