using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GameLogic.Others {

	public class TemporaryElement : MonoBehaviour {
		[SerializeField] protected float lifeTime = 1f;

		private void Start() {
			Destroy(this.gameObject, lifeTime);
		}
	}

}