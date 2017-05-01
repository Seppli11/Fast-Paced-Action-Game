using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMainMenu : MonoBehaviour
{
	private bool isSetted = false;
    // Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(InventoryMenu.inventoryMenu.showInventory);

	}

    public void Resume_Btn()
    {
        MenuManager.sMainMenu.RevertMenu();
    }

	public void Save_Btn()
	{
		if (GameControl.currentSaveState != null)
			GameControl.gameControl.SaveSaveState(GameControl.currentSaveState);
		MenuManager.sMainMenu.RevertMenu();
	}

    public void Option_Btn()
    {
        MenuManager.sMainMenu.ShowMenu(Menu.OptionMenu, 0);
    }

	public void BackToMainMenu_Btn()
	{
		if(GameControl.currentSaveState != null)
			GameControl.gameControl.SaveSaveState(GameControl.currentSaveState);
		GameControl.gameControl.shouldLoadScene = false;
		GameControl.currentSaveState = null;
		SceneManager.LoadScene("MainMenu");
	}

    public void Exit_Btn()
    {
        Application.Quit();
    }
}
