using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public static MainMenu sMainMenu;

	[Language]
	public string startnewGameNameFieldIsEmtpyError = "Name is empty!";

	public Animator mainMenuAnimator;
	public Animator startNewGameMenuAnimator;
	public Animator loadGameMenuAnimator;
	public GameObject parentOfSaveStateEntries;

	public GameObject saveStateEntryPrefab;

	public InputField saveStateName;
	public Text errorConsoleForStartNewGame;

	private void Awake()
	{
		sMainMenu = this;
		if(GameControl.gameControl != null)
			GameControl.gameControl.shouldLoadScene = false;
		GameControl.currentSaveState = null;
		Time.timeScale = 1;
	}

	// Use this for initialization
	void Start () {
		LoadSaveStateEntries();
		Debug.Log(Application.persistentDataPath);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadSaveStateEntries()
	{
		GameControl.gameControl.LoadSaveStateList();

		for (int i = 0; i < parentOfSaveStateEntries.transform.childCount; i++)
		{
			Destroy(parentOfSaveStateEntries.transform.GetChild(i).gameObject);
		}
		foreach (var ss in GameControl.saveStates)
		{
			GameObject go = Instantiate(saveStateEntryPrefab, parentOfSaveStateEntries.transform);
			go.GetComponent<SaveStateEntry>().saveState = ss;
		}
	}

	public void MainMenu_Btn(int b)
	{
		mainMenuAnimator.SetTrigger("Left");
		switch(b)
		{
			case 0: //go to StartnewMenu
				startNewGameMenuAnimator.SetTrigger("Left");
				break;
			case 1: //go to load menu
				loadGameMenuAnimator.SetTrigger("Left");
				break;
			case 2: //go to option menu
				break;
			case 3: //exit game
				StartCoroutine(IsDone(mainMenuAnimator, "1LeftMainMenu", () => Application.Quit()));
				break;
		}
	}

	public void StartNewGame_Btn(int b)
	{
		switch(b)
		{
			case 0: // go the new game
				if(saveStateName.text == "")
				{
					errorConsoleForStartNewGame.text = startnewGameNameFieldIsEmtpyError;
					return;
				}
				startNewGameMenuAnimator.SetTrigger("Left");
				StartCoroutine(IsDone(startNewGameMenuAnimator, "2LeftMenuStartNewGame", () =>
				{
					SaveState ss = GameControl.gameControl.CreateNewSaveState(saveStateName.text);
					GameControl.currentSaveState = ss;
					GameControl.gameControl.shouldLoadScene = true;
					SceneManager.LoadScene("GameScene");
				}));
				break;
			case 1: //go back to main menu
				startNewGameMenuAnimator.SetTrigger("Right");
				mainMenuAnimator.SetTrigger("Right");
				break;
		}
	}

	public void LoadGame_Btn(int b)
	{
		switch(b)
		{
			case 0: //Go Back to main menu
				loadGameMenuAnimator.SetTrigger("Right");
				mainMenuAnimator.SetTrigger("Right");
				break;
			case 1: // delete save state
				GameControl.gameControl.DeleteSaveState(SaveStateEntry.selectedSaveStateEntry.saveState);
				LoadSaveStateEntries();
				break;
			case 2: //load game
				loadGameMenuAnimator.SetTrigger("Left");
				StartCoroutine(IsDone(loadGameMenuAnimator, "2LeftLoadGameMenu", () =>
				{
					GameControl.currentSaveState = SaveStateEntry.selectedSaveStateEntry.saveState;
					GameControl.gameControl.shouldLoadScene = true;
					SceneManager.LoadScene("GameScene");
				}));
				break;
		}
	}

	IEnumerator IsDone(Animator animator, string animationName, Action a)
	{
		while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) )
		{
			yield return new WaitForSeconds(0.5f);
		}
		a();
	}
}

