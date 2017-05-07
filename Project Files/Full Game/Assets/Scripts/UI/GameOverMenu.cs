using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {
	public Text scoreText;
	// Use this for initialization
	void Start () {
		
	}

	private void OnEnable()
	{
		Leaderboard.sLeaderboard.AddNewHighscore(Leaderboard.currentUserName, InventoryInfo.inventoryInfo.currencyLeft);
		scoreText.text = "Score: " + InventoryInfo.inventoryInfo.currencyLeft;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void GameOverMenu_Btn(int b)
	{
		switch(b)
		{
			case 0: //tryAgain
				SceneManager.LoadScene("GameScene");
				//GameControl.gameControl.shouldLoadScene = true;
				break;
			case 1: //back to main menu
				//GameControl.gameControl.shouldLoadScene = false;
				SceneManager.LoadScene("MainMenu");
				break;
		}
	}
}
