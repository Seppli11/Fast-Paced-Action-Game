using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close_Btn()
	{
		MenuManager.sMainMenu.ShowMenu(Menu.GameUi, 1); 
	}
}
