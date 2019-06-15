using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Elementure.GameLogic.Agents {

	[Serializable]
	public class AgentEvent : UnityEvent<Agent> { }

	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class Agent : MonoBehaviour {

		public enum AgentStates {
			Alive, Dead
		}

		[SerializeField] protected string id;
		[SerializeField] AgentAttributeSheet attributes;

		[Header("Events")]
		[SerializeField] AgentEvent OnHealed = new AgentEvent();
		[SerializeField] AgentEvent OnDamaged = new AgentEvent();
		[SerializeField] AgentEvent OnDead = new AgentEvent();

		protected int currentHealth;
		protected float cooldownTimer;

		public string Id { get => id; }
		public bool Initialized { get; protected set; }
		public AgentAttributeSheet Attributes { get => attributes; }
		public AgentStates State { get; protected set; }
		public Inventory Inventory { get; protected set; }

		#region Init
		private void Awake() {
			Initialized = false;
			StoreComponents();
			Setup();
		}

		private void StoreComponents() {
			Inventory = GetComponent<Inventory>();
		}

		private void Setup() {
			currentHealth = attributes.MaxHealth;
		}

		private void Start() {
			StoreReferences();
			Init();
			Initialized = true;
		}

		private void StoreReferences() {
		}

		private void Init() {
			State = currentHealth > 0 ? AgentStates.Alive : AgentStates.Dead;
		}
		#endregion

		#region Control
		#endregion

		#region Actions
		public void Damage(int points) {
			currentHealth -= points;
			OnDamaged?.Invoke(this);
			if (currentHealth <= 0) {
				State = AgentStates.Dead;
				OnDead?.Invoke(this);
			}
		}

		public void Heal(int points) {
			currentHealth += points;
			currentHealth = Mathf.Min(currentHealth, attributes.MaxHealth);
			OnHealed?.Invoke(this);
		}

		public void InstaKill() {
			Damage(currentHealth);
		}
		#endregion

		#region Queries
		#endregion

		#region Debug
		#endregion

	}

}