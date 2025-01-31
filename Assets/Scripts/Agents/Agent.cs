﻿using System;
using System.Linq;
using Elementure.GameLogic.Inventories;
using UnityEngine;
using UnityEngine.Events;
using Elementure.Audio;
using System.Collections.Generic;

namespace Elementure.GameLogic.Agents {

	[Serializable]
	public class AgentEvent : UnityEvent<Agent> { }

	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class Agent : MonoBehaviour {

		private const string TileTag = "Tile";

		public enum AgentStates {
			Alive, Dead
		}

		[SerializeField] protected string id;
		[SerializeField] protected AgentAttributeSheet attributes;
		[SerializeField] protected Transform feet;
		[SerializeField] protected Animator animator;
		[SerializeField] protected string deathAudio = "Death_None";

		[Header("Events")]
		[SerializeField] public AgentEvent OnHealed = new AgentEvent();
		[SerializeField] public AgentEvent OnDamaged = new AgentEvent();
		[SerializeField] public AgentEvent OnDead = new AgentEvent();

		protected int currentHealth;
		protected List<Collider> collisions = new List<Collider>();

		public bool Initialized { get; protected set; }

		public string Id { get => id; }
		public bool Colliding { get { return collisions.Count > 0; } }
		public int CurrentHealth { get => currentHealth; }

		public AgentAttributeSheet Attributes { get => attributes; }
		public AgentStates State { get; protected set; }
		public Inventory Inventory { get; protected set; }

		public Rigidbody Body { get; protected set; }
		public BoxCollider Collider { get; protected set; }
		public Animator Animator { get => animator; }
		public Transform Feet { get => feet; }

		[NonSerialized] public Vector3 movementDirection, lookingDirection;

		#region Init
		private void Awake() {
			Initialized = false;
			AgentManager.Register(this);
			StoreComponents();
			Setup();
		}

		private void StoreComponents() {
			Inventory = GetComponent<Inventory>();
			Body = GetComponent<Rigidbody>();
			Collider = GetComponent<BoxCollider>();
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
		private void OnCollisionEnter(Collision collision) {
			if (!collisions.Contains(collision.collider)) {
				collisions.Add(collision.collider);
			}
		}

		private void OnCollisionExit(Collision collision) {
			collisions.Remove(collision.collider);
		}
		#endregion

		#region Actions
		public void Damage(float points) {
			if (points <= 0) {
				return;
			}
			AudioManager.Play($"Damage_None");

			currentHealth -= (int)points;
			OnDamaged?.Invoke(this);

			if (currentHealth > 0) {
				return;
			}

			State = AgentStates.Dead;
			OnDead?.Invoke(this);
			AudioManager.Play(deathAudio);
		}

		public void Heal(int points) {
			currentHealth += points;
			currentHealth = Mathf.Min(currentHealth, attributes.MaxHealth);
			OnHealed?.Invoke(this);
		}

		public void InstaKill() {
			Damage(currentHealth);
		}

		public void Disappear() {
			AgentManager.Remove(this);
			Destroy(this.gameObject, 1f);
		}
		#endregion

		#region Queries
		public bool IsGrounded() {
			RaycastHit[] hits = Physics.RaycastAll(feet.position, Vector3.down, 5f);

			if (hits == null || hits.Length <= 0) {
				return false;
			}
			
			return hits.Any(h => h.distance <= feet.localPosition.magnitude);
		}

		public TileController GetTile() {
			RaycastHit[] hits = Physics.RaycastAll(feet.position, Vector3.down, 5f);

			TileController tile = null;
			int i = 0;
			while (i < hits.Length && (tile = hits[i].collider.GetComponent<TileController>()) == null) {
				i++;
			}
			return tile;
		}
		#endregion

		#region Debug
		#endregion

	}

}