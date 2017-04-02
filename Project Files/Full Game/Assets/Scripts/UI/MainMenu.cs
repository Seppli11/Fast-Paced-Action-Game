using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public bool isActivated = false;
    public Camera menuCamera;

    public Canvas mainMenu;

    private Camera changeBackToCamera;
    // Use this for initialization
	void Start () {
        changeBackToCamera = Camera.main;
	    mainMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Controls.StaticControls.MenuButton)
	    {
	        isActivated = !isActivated;
	    }

	    if (isActivated)
	    {
	        if (!menuCamera.enabled)
	        {
	            changeBackToCamera = Camera.main;

	            mainMenu.enabled = true;

	            menuCamera.transform.position = changeBackToCamera.transform.position;
                menuCamera.CopyFrom(changeBackToCamera);
	            changeBackToCamera.enabled = false;
	            menuCamera.enabled = true;

	            Time.timeScale = 0;
	        }
	    }
	    else
	    {
	        if (menuCamera.enabled)
	        {
                mainMenu.enabled = false;

                changeBackToCamera.enabled = true;
                menuCamera.enabled = false;

	            Time.timeScale = 1;
	        }
	    }
	}

    public void Resume_Btn()
    {
        isActivated = false;
    }

    public void Option_Btn()
    {
        
    }

    public void Exit_Btn()
    {
        Application.Quit();
    }
}
