using Elementure.Audio;
using Elementure.GameLogic;
using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Elementure.GUI {

	public class MenuController : MonoBehaviour {
		[Header("Environment")]
		[SerializeField] protected CameraControl cameraControl;
		[SerializeField] protected GameObject roomsParent;
		[SerializeField] protected GameObject playerPrefab;
		[SerializeField] protected Transform playerSpawnPoint;
		[SerializeField] protected string mainMenuMusic = "";
		[SerializeField] protected string gameMusic = "";

		[Header("Menu Controllers")]
		[SerializeField] protected MenuElement mainScreen;
		[SerializeField] protected PlayerStateGUI playerStateGUI;
		[SerializeField] protected InventoryGUI inventoryGUI;
		[SerializeField] protected ControlGUI controlGUI;
		[SerializeField] protected EndGUI endGUI;
		
		public Agent Player { get; protected set; }
		public bool GameEnded { get; protected set; }

		private void Awake() {
			roomsParent.SetActive(false);
		}

		private void Start() {
			ShowMainMenu();
		}
		
		public void NewGame() {
			SpawnPlayer();
			Player.OnDead.AddListener(delegate(Agent agent) { EndGame(false); });
			roomsParent.SetActive(true);

			cameraControl.StorePlayer(Player);
			playerStateGUI.StorePlayer(Player);
			inventoryGUI.StorePlayer(Player);
			controlGUI.StorePlayer(Player);

			playerStateGUI.Show();
			controlGUI.EnableButtons();
			controlGUI.Show();

			mainScreen.Hide();

			AudioManager.Loop(gameMusic);
		}

		void SpawnPlayer() {
			GameObject playerObject = Instantiate(playerPrefab);
			playerObject.transform.position = playerSpawnPoint.transform.position;

			Player = playerObject.GetComponent<Agent>();
		}

		public void EndGame(bool won) {
			GameEnded = true;
			controlGUI.DisableButtons();
			if (won) {
				endGUI.ShowGameWon();
			} else {
				endGUI.ShowGameOver();
			}
		}

		public void GoToMainMenu() {
			GameEnded = false;
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
			AudioManager.Loop(mainMenuMusic);
		}

		public void Restart() {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public void Quit() {
			Application.Quit();
		}
	}

}