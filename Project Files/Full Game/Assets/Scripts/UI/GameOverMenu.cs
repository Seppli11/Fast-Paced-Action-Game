using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
				GameControl.gameControl.shouldLoadScene = true;
				break;
			case 1: //back to main menu
				GameControl.gameControl.shouldLoadScene = false;
				SceneManager.LoadScene("MainMenu");
				break;
		}
	}
}
