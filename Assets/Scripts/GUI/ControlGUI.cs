using Elementure.GameLogic;
using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GUI { 
	public class ControlGUI : MenuElement {

		[Header("Menu Controllers")]
		[SerializeField] protected MenuController menuController;
		[SerializeField] protected InventoryGUI inventoryGUI;

		[Header("Labels")]
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
			if (inventoryGUI.IsShowing) {
				inventoryGUI.ChooseMovement();
				return;
			}
			input.AddToVerticalAxis(1);
		}

		public void Down() {
			if (inventoryGUI.IsShowing) {
				inventoryGUI.ChooseMovement();
				return;
			}
			input.AddToVerticalAxis(-1);
		}

		public void Left() {
			if (inventoryGUI.IsShowing) {
				inventoryGUI.ChooseMovement();
				return;
			}
			input.AddToHorizontalAxis(-1);
		}

		public void Right() {
			if (inventoryGUI.IsShowing) {
				inventoryGUI.ChooseMovement();
				return;
			}

			input.AddToHorizontalAxis(1);
		}
		
		public void ButtonA() {
			if (inventoryGUI.IsShowing) {
				inventoryGUI.ChooseA();
				return;
			}

			input.TriggerA();
		}

		public void ButtonB() {
			if (inventoryGUI.IsShowing) {
				inventoryGUI.ChooseB();
				return;
			}

			input.TriggerB();
		}
		
		public void PushedStart() {
			if (menuController.GameEnded) {
				menuController.Restart();

			} else if (inventoryGUI.IsShowing) {
				inventoryGUI.EndChoosingState();

			} else {
				inventoryGUI.Toggle();
			}
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
			if (Input.GetButtonDown("Start")) {
				PushedStart();
			}

			if (player == null) {
				return;
			}

			UpdateVerbText(player.Inventory.VerbMovement, movementVerbText, movementModifierText);
			UpdateVerbText(player.Inventory.VerbA, verbAText, modifierAText);
			UpdateVerbText(player.Inventory.VerbB, verbBText, modifierBText);
		}
		
		private void UpdateVerbText(Verb verb, Text verbText, Text modifierText) {
			verbText.text = verb != null ? verb.Type.ToString() : "None";
			modifierText.text = verb != null ? verb.Modifier.ToString() : "None";
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