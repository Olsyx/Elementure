using Elementure.GameLogic.Words;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;
using Elementure.GameLogic.Agents;
using Elementure.Audio;

namespace Elementure.GameLogic {

	[Serializable]
	public class ProjectileClass {
		public ModifierTypes modifier;
		public Sprite sprite;
		public GameObject trail;
	}

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(BoxCollider))]
	public class Projectile : MonoBehaviour {

		[SerializeField] protected float speed;
		[SerializeField] protected float trailSpawnGap = 0.5f;
		[SerializeField] protected SpriteRenderer renderer;
		[SerializeField] protected Transform trailSpawnPoint;
		[SerializeField] protected List<ProjectileClass> types = new List<ProjectileClass>();

		[NonSerialized] public Vector3 direction;

		protected float damage;
		protected float countdown;
		protected ModifierTypes modifier;
		protected GameObject trailPrefab;
		protected bool spawnTrail;

		#region Init
		public void SetData(ModifierTypes type, float damage) {
			modifier = type;
			this.damage = damage;
			SpawnType();
		}

		private void SpawnType() {
			ProjectileClass projectileClass = types.FirstOrDefault(p => p.modifier == this.modifier);

			if (projectileClass == null) {
				return;
			}

			renderer.sprite = projectileClass.sprite;
			spawnTrail = projectileClass.trail != null;
			trailPrefab = projectileClass.trail;
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

			if (!spawnTrail) {
				return;
			}

			countdown -= Time.deltaTime;
			if (countdown > 0) {
				return;
			}

			countdown = trailSpawnGap;
			SpawnTrail();
		}

		void SpawnTrail() {
			GameObject trail = Instantiate(trailPrefab);
			trail.transform.position = trailSpawnPoint.transform.position;
		}

		private void Collided(GameObject other) {
			Agent target = other.GetComponent<Agent>();
			if (target != null) {
				target.Damage(damage);
				AudioManager.Play("Damage_None");
			}
			Destroy(this.gameObject);
		}
		#endregion


	}

}