using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elementure.GUI {

	public class MenuController : MonoBehaviour {
		[Header("Environment")]
		[SerializeField] protected GameObject playerPrefab;
		[SerializeField] protected Transform playerSpawnPoint;

		[Header("Menu Controllers")]
		[SerializeField] protected MenuElement mainScreen;
		[SerializeField] protected PlayerStateGUI playerStateGUI;
		[SerializeField] protected InventoryGUI inventoryGUI;
		[SerializeField] protected ControlGUI controlGUI;
		[SerializeField] protected EndGUI endGUI;
		
		public Agent Player { get; protected set; }

		private void Start() {
			ShowMainMenu();
		}

		private void Update() {
			if (Input.GetButtonDown("Start")) {
				inventoryGUI.Toggle();
			}
		}

		public void NewGame() {
			SpawnPlayer();
			Player.OnDead.AddListener(delegate(Agent agent) { GameEnded(false); });

			playerStateGUI.StorePlayer(Player);
			inventoryGUI.StorePlayer(Player);
			controlGUI.StorePlayer(Player);

			playerStateGUI.Show();
			controlGUI.EnableButtons();
			controlGUI.Show();

			mainScreen.Hide();
		}

		void SpawnPlayer() {
			GameObject playerObject = Instantiate(playerPrefab);
			playerObject.transform.position = playerSpawnPoint.transform.position;

			Player = playerObject.GetComponent<Agent>();
		}

		public void GameEnded(bool won) {
			controlGUI.DisableButtons();
			if (won) {
				endGUI.ShowGameWon();
			} else {
				endGUI.ShowGameOver();
			}
		}

		public void GoToMainMenu() {
			Player.Disappear();
			ShowMainMenu();
		}

		void ShowMainMenu() {
			mainScreen.Show();
			playerStateGUI.Hide();
			inventoryGUI.Hide();
			controlGUI.Hide();
			controlGUI.DisableButtons();
			endGUI.Hide();
		}

		public void Quit() {
			Application.Quit();
		}
	}

}