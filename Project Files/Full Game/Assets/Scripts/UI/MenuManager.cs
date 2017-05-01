using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public static MenuManager sMainMenu;

	public static Camera currentCamera;

	public static Menu currentMenu;

	public GameObject gameUi;
	public GameObject controlsMenu;
	public GameObject inventoryMenu;
	public GameObject mainMenu;
	public GameObject optionMenu;
	public GameObject gameOverMenu;

	[Space(20)]
	public Camera mainCamera;
	public Camera bluryCamera;

	private Stack<Menu> lastMenus = new Stack<Menu>();
	private Stack<float> lastTimeScales = new Stack<float>();

	private void Awake()
	{
		sMainMenu = this;
	}

	// Use this for initialization
	void Start ()
	{
		currentCamera = mainCamera;
		currentMenu = Menu.MainMenu;
		MenuManager.sMainMenu.ShowMenu(Menu.ControlsMenu, 0, false);
	}

	// Update is called once per frame
	void Update () {
		if(Controls.StaticControls.InventoryButton)
		{
			if(currentMenu == Menu.GameUi)
			{
				ShowMenu(Menu.InventoryMenu, 0);
			}else if(currentMenu == Menu.InventoryMenu)
			{
				RevertMenu();
				InventoryMenu.inventoryMenu.weapon1 = InventoryMenu.inventoryMenu.weapon1Cell.item as Weapon;
				InventoryMenu.inventoryMenu.weapon2 = InventoryMenu.inventoryMenu.weapon2Cell.item as Weapon;
			}
		}

		if (Controls.StaticControls.MenuButton)
		{
			if (currentMenu == Menu.GameUi)
			{
				ShowMenu(Menu.MainMenu, 0f);
			} else if (currentMenu == Menu.MainMenu)
			{
				RevertMenu();
			} else if (currentMenu == Menu.OptionMenu)
			{
				RevertMenu();
			} else if(currentMenu == Menu.ControlsMenu)
			{
				ShowMenu(Menu.GameUi, 1);
			}
		}
	}

	public void ChangeCamera(Camera cam)
	{
		mainCamera.enabled = false;
		bluryCamera.enabled = false;
		//if (cam == currentCamera) return;
		if (currentCamera == null)
		{
			cam.CopyFrom(currentCamera);
		}
		currentCamera = cam;
		currentCamera.enabled = true;
		//Debug.Log(currentCamera);
	}

	public void RevertMenu()
	{
		ShowMenu(lastMenus.Pop(), lastTimeScales.Pop(), false);
	}

	public void ShowMenu(Menu menu, float timeScale = 1, bool registerInHistory = true)
	{
		if (currentMenu == menu)
		{
			Debug.LogError("tryed to show " + menu);
			return;
		}
		Debug.Log("ShowMenu " + menu);
		if(registerInHistory) lastMenus.Push(currentMenu);
		currentMenu = menu;

		if(registerInHistory) lastTimeScales.Push(Time.timeScale);
		Time.timeScale = timeScale;

		DisableAllMenus();

		switch (menu)
		{
			case Menu.GameUi:
				gameUi.SetActive(true);
				ChangeCamera(mainCamera);
				break;
			case Menu.ControlsMenu:
				controlsMenu.SetActive(true);
				ChangeCamera(bluryCamera);
				break;
			case Menu.InventoryMenu:
				inventoryMenu.SetActive(true);
				ChangeCamera(bluryCamera);
				break;
			case Menu.MainMenu:
				mainMenu.SetActive(true);
				ChangeCamera(bluryCamera);
				break;
			case Menu.OptionMenu:
				optionMenu.SetActive(true);
				ChangeCamera(bluryCamera);
				break;
			case Menu.GameOverMenu:
				GameControl.gameControl.SaveSaveState(GameControl.currentSaveState);
				gameOverMenu.SetActive(true);
				ChangeCamera(bluryCamera);
				break;
			default:
				Debug.LogError("Unkown Menu '" + menu + "'!");
				break;
		}
	}

	void DisableAllMenus()
	{
		gameUi.SetActive(false);
		controlsMenu.SetActive(false);
		inventoryMenu.SetActive(false);
		mainMenu.SetActive(false);
		optionMenu.SetActive(false);
		gameOverMenu.SetActive(false);
	}

}

public enum Menu
{
	GameUi,
	ControlsMenu,
	InventoryMenu,
	MainMenu,
	OptionMenu,
	GameOverMenu
}
