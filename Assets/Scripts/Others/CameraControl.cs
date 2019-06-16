using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic {

	[RequireComponent(typeof(Camera))]
	public class CameraControl : MonoBehaviour {

		[SerializeField] protected float speed = 5f;
		[SerializeField] protected float distance = 5f;

		protected Vector3 separation;
		public Agent Player { get; protected set; }

		public virtual void StorePlayer(Agent player) {
			Player = player;
			transform.position = player.transform.position + Vector3.up * distance;
			transform.LookAt(player.transform.position);
			separation = transform.position - player.transform.position;
		}

		private void Update() {
			if (Player == null) {
				return;
			}

			transform.position = Vector3.Lerp(transform.position, Player.transform.position + separation, speed);
		}
	}

}