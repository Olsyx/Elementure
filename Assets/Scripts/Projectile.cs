using Elementure.GameLogic.Words;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using Elementure.GameLogic.Agents;

namespace Elementure.GameLogic {

	[Serializable]
	public class ProjectileClass {
		public ModifierTypes modifier;
		public GameObject prefab;
	}

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(BoxCollider))]
	public class Projectile : MonoBehaviour {

		[SerializeField] protected float speed;
		[SerializeField] protected GameObject defaultPrefab;
		[SerializeField] protected List<ProjectileClass> types = new List<ProjectileClass>();

		[NonSerialized] public Vector3 direction;

		protected float damage;
		protected ModifierTypes modifier;

		#region Init
		public void SetData(ModifierTypes type, float damage) {
			modifier = type;
			SpawnType();
		}

		private void SpawnType() {
			ProjectileClass projectileClass = types.FirstOrDefault(p => p.modifier == this.modifier);
			GameObject prefab = (projectileClass != null) ? projectileClass.prefab : defaultPrefab;
			
			if (prefab == null) {
				return;
			}

			GameObject graphics = Instantiate(prefab, this.transform);
			graphics.transform.localPosition = new Vector3();
		}
		#endregion

		#region Collisions
		private void OnTriggerEnter(Collider other) {
			Collided(other.attachedRigidbody == null ? other.gameObject : other.attachedRigidbody.gameObject);
		}

		private void OnCollisionEnter(Collision other) {
			Collided(other.rigidbody == null ? other.gameObject : other.rigidbody.gameObject);
		}
		#endregion

		#region Control
		private void Update() {
			transform.position += direction * speed * Time.deltaTime;
		}

		private void Collided(GameObject other) {
			Agent target = other.GetComponent<Agent>();
			target?.Damage(damage);
			Destroy(this.gameObject);
		}
		#endregion


	}

}