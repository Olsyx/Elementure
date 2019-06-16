using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GUI {

	public class MenuElement : MonoBehaviour {

		protected Agent player;

		public virtual void StorePlayer(Agent player) {
			this.player = player;
		}
		
		public virtual void Show() {
			gameObject.SetActive(true);
		}

		public virtual void Hide() {
			gameObject.SetActive(false);
		}
	}

}