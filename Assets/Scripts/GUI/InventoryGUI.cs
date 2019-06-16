using Elementure.GameLogic;
using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Elementure.GUI {
	public enum InventoryState {
		Idle, ChoosingVerb, ChoosingModifier
	}

	public class InventoryGUI : MenuElement {

		[Header("Controllers")]
		[SerializeField] protected PlayerStateGUI playerStateGUI;

		[Header("Labels")]
		[SerializeField] protected Text movementVerbText;
		[SerializeField] protected Text movementModifierText;
		[SerializeField] protected Text verbAText;
		[SerializeField] protected Text modifierAText;
		[SerializeField] protected Text verbBText;
		[SerializeField] protected Text modifierBText;

		public InventoryState State { get; protected set; }
		protected VerbTypes targetVerb;
		protected ModifierTypes targetModifier;

		protected float chooseDelay = 0.5f;
		protected float countdown;
		
		public override void StorePlayer(Agent player) {
			base.StorePlayer(player);
			player.Inventory.GiveGuiControl(this);
		}

		private void Update() {
			if (player == null) {
				return;
			}

			UpdateTexts();

			if (State == InventoryState.Idle) {
				return;
			}

			countdown -= Time.deltaTime;
			if (countdown > 0) {
				return;
			}

			if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0) {
				ChooseMovement();
				return;
			}

			if (Input.GetButtonDown("A")) {
				ChooseA();
				return;
			}

			if (Input.GetButtonDown("B")) {
				ChooseB();
				return;
			}

			if (Input.GetButtonDown("Start")) {
				EndChoosingState();
			}
		}

		private void UpdateTexts() {
			UpdateVerbText(player.Inventory.VerbMovement, movementVerbText, movementModifierText);
			UpdateVerbText(player.Inventory.VerbA, verbAText, modifierAText);
			UpdateVerbText(player.Inventory.VerbB, verbBText, modifierBText);
		}

		private void UpdateVerbText(Verb verb, Text verbText, Text modifierText) {
			verbText.text = verb != null ? verb.Type.ToString() : "None";
			modifierText.text = verb != null ? verb.Modifier.ToString() : "None";
		}
		
		public void OpenToChoose(VerbTypes newVerb) {
			SetChoosingState(InventoryState.ChoosingVerb);
			targetVerb = newVerb;
			playerStateGUI.DiaryLog.text = targetVerb.ToString();
		}

		public void OpenToChoose(ModifierTypes newModifier) {
			SetChoosingState(InventoryState.ChoosingModifier);
			targetModifier = newModifier;
			playerStateGUI.DiaryLog.text = targetModifier.ToString();
		}

		private void SetChoosingState(InventoryState newState) {
			GameMaster.PauseGame();
			Show();
			Block();
			playerStateGUI.Block();
			State = newState;
			countdown = chooseDelay;
		}

		public void ChooseMovement() {
			if (State == InventoryState.Idle) {
				return;
			}

			if (State == InventoryState.ChoosingModifier) {
				player.Inventory.ChangeMovementModifier(targetModifier);
			} else {
				player.Inventory.ChangeMovementVerb(targetVerb);
			}
			
			EndChoosingState();
		}

		public void ChooseA() {
			if (State == InventoryState.Idle) {
				return;
			}

			if (State == InventoryState.ChoosingModifier) {
				player.Inventory.ChangeModifierA(targetModifier);
			} else {
				player.Inventory.ChangeVerbA(targetVerb);
			}

			EndChoosingState();
		}

		public void ChooseB() {
			if (State == InventoryState.Idle) {
				return;
			}

			if (State == InventoryState.ChoosingModifier) {
				player.Inventory.ChangeModifierB(targetModifier);
			} else {
				player.Inventory.ChangeVerbB(targetVerb);
			}

			EndChoosingState();
		}

		public void EndChoosingState() {
			if (State == InventoryState.Idle) {
				return;
			}

			State = InventoryState.Idle;
			Unblock();
			playerStateGUI.Unblock();
			Hide();
			GameMaster.ResumeGame();
		}
	}
}