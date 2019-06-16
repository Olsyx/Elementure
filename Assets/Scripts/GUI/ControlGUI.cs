using Elementure.GameLogic;
using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GUI { 
	public class ControlGUI : MenuElement {

		[SerializeField] protected Text movementVerbText;
		[SerializeField] protected Text movementModifierText;
		[SerializeField] protected Text verbAText;
		[SerializeField] protected Text modifierAText;
		[SerializeField] protected Text verbBText;
		[SerializeField] protected Text modifierBText;
		[SerializeField] protected List<Button> buttons;

		InputController input;

		public override void StorePlayer(Agent player) {
			base.StorePlayer(player);
			input = player.GetComponent<InputController>();
		}

		public void Up() {
			input.AddToVerticalAxis(1);
		}

		public void Down() {
			input.AddToVerticalAxis(-1);
		}

		public void Left() {
			input.AddToHorizontalAxis(-1);
		}

		public void Right() {
			input.AddToHorizontalAxis(1);
		}
		
		public void ButtonA() {
			input.TriggerA();
		}

		public void ButtonB() {
			input.TriggerB();
		}

		public void DisableButtons() {
			for (int i = 0; i < buttons.Count; i++) {
				buttons[i].interactable = false;
			}
		}

		public void EnableButtons() {
			for (int i = 0; i < buttons.Count; i++) {
				buttons[i].interactable = true;
			}
		}

		private void Update() {
			if (player == null) {
				return;
			}

			movementVerbText.text = player.Inventory.VerbMovement.Type.ToString();
			movementModifierText.text = player.Inventory.VerbMovement.Modifier.ToString();

			verbAText.text = player.Inventory.VerbA?.Type.ToString();
			modifierAText.text = player.Inventory.VerbA?.Modifier.ToString();

			verbBText.text = player.Inventory.VerbB?.Type.ToString();
			modifierBText.text = player.Inventory.VerbB?.Modifier.ToString();
		}
		
		public override void Show() {
			movementVerbText.gameObject.SetActive(true);
			movementModifierText.gameObject.SetActive(true);
			verbAText.gameObject.SetActive(true);
			modifierAText.gameObject.SetActive(true);
			verbBText.gameObject.SetActive(true);
			modifierBText.gameObject.SetActive(true);
		}

		public override void Hide() {
			movementVerbText.gameObject.SetActive(false);
			movementModifierText.gameObject.SetActive(false);
			verbAText.gameObject.SetActive(false);
			modifierAText.gameObject.SetActive(false);
			verbBText.gameObject.SetActive(false);
			modifierBText.gameObject.SetActive(false);
		}
	}
}