using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GUI {

	public class MenuElement : MonoBehaviour {

		protected Agent player;

		protected bool showing;
		protected bool blocked;

		public bool IsShowing { get => showing; }
		public bool IsBlocked { get => blocked; }

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
			if (blocked) {
				return;
			}
			showing = true;
			gameObject.SetActive(true);
		}

		public virtual void Hide() {
			if (blocked) {
				return;
			}
			showing = false;
			gameObject.SetActive(false);
		}

		public virtual void Block() {
			blocked = true;
		}

		public virtual void Unblock() {
			blocked = false;
		}
	}

}