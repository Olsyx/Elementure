using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GUI {

	public class MenuElement : MonoBehaviour {

		protected Agent player;

		protected bool showing;

		public virtual void StorePlayer(Agent player) {
			this.player = player;
			showing = gameObject.activeInHierarchy;
		}
		
		public void Toggle() {
			if (showing) {
				Hide();
			} else {
				Show();
			}
		}

		public virtual void Show() {
			showing = true;
			gameObject.SetActive(true);
		}

		public virtual void Hide() {
			showing = false;
			gameObject.SetActive(false);
		}
	}

}