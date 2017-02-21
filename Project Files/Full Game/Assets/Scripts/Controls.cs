using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public ControlProfile CurrentControlProfile = new ControlProfile("Hy");
    public ControlProfile KeyboardControlProfile = new ControlProfile("Keyboard Profile", InputType.Keyboard);
    public ControlProfile ControllerControlProfile = new ControlProfile("Controller Profile", InputType.Controller, KeyCode.JoystickButton4, KeyCode.JoystickButton5, KeyCode.JoystickButton0,
        KeyCode.JoystickButton6, KeyCode.JoystickButton7, "cLeftStickX", "cLeftStickY");

    public InputType CurrentInput
    {
        get { return CurrentControlProfile.InputType; }
    }

    private float _horizontalAxis = 0f;
    public float HorizontalAxis
    {
        get { return _horizontalAxis; }
    }

    private float _verticalAxis = 0f;
    public float VerticalAxis
    {
        get { return _verticalAxis; }
    }

    private bool _shootButton1;
    public bool ShootButton1
    {
        get { return _shootButton1; }
    }

    private bool _shootButton2;
    public bool ShootButton2
    {
        get { return _shootButton2; }
    }

    private bool _teleportButton;
    public bool TeleportButton
    {
        get { return _teleportButton; }
    }

    private bool _inventoryButton;
    public bool InventoryButton
    {
        get { return _inventoryButton; }
    }

    private bool _menuButton;

    public bool MenuButton
    {
        get { return _menuButton;}
    }

    public bool menuButton
    {
        get { return _menuButton; }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputType();
        switch (CurrentInput)
        {
            case InputType.Keyboard:
			    if(Input.GetKey(CurrentControlProfile.Shoot1))
			    {
			        _shootButton1 = true;
			    } else if (Input.GetKeyUp(CurrentControlProfile.Shoot1))
			    {
			        _shootButton1 = false;
                }

                if (Input.GetKey(CurrentControlProfile.Shoot2))
                {
                    _shootButton2 = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Shoot2))
                {
                    _shootButton2 = false;
                }

                if (Input.GetKey(CurrentControlProfile.Teleport))
                {
                    _teleportButton = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Teleport))
                {
                    _teleportButton = false;
                }

                if (Input.GetKey(CurrentControlProfile.Inventory))
                {
                    _inventoryButton = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Inventory))
                {
                    _inventoryButton = false;
                }

                if (Input.GetKey(CurrentControlProfile.Menu))
                {
                    _menuButton = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Menu))
                {
                    _menuButton = false;
                }

                _horizontalAxis = Input.GetAxisRaw(CurrentControlProfile.MoveHorizontal);
                _verticalAxis = Input.GetAxisRaw(CurrentControlProfile.MoveVertical);

                break;
            case InputType.Touche:
                break;
        }
    }

    private void CheckInputType()
    {
        if (CurrentControlProfile.IsSelected()) return;
        if (KeyboardControlProfile.IsSelected())
        {
            CurrentControlProfile = KeyboardControlProfile;
        } else if (ControllerControlProfile.IsSelected())
        {
            CurrentControlProfile = ControllerControlProfile;
        }
        else
        {
            return;
        }
        Debug.Log("Change ControlProfile to " + CurrentControlProfile.name);
    }

    private bool isKeyboard()
    {
        if (Input.GetAxisRaw("MouseX") != 0) return true;
        if (Input.GetAxisRaw("MouseY") != 0) return true;
        //if (Input.anyKey) return true;
        return false;
    }

    private bool isController()
    {
        // joystick buttons
        if (Input.GetKey(KeyCode.Joystick1Button0) ||
           Input.GetKey(KeyCode.Joystick1Button1) ||
           Input.GetKey(KeyCode.Joystick1Button2) ||
           Input.GetKey(KeyCode.Joystick1Button3) ||
           Input.GetKey(KeyCode.Joystick1Button4) ||
           Input.GetKey(KeyCode.Joystick1Button5) ||
           Input.GetKey(KeyCode.Joystick1Button6) ||
           Input.GetKey(KeyCode.Joystick1Button7) ||
           Input.GetKey(KeyCode.Joystick1Button8) ||
           Input.GetKey(KeyCode.Joystick1Button9) ||
           Input.GetKey(KeyCode.Joystick1Button10) ||
           Input.GetKey(KeyCode.Joystick1Button11) ||
           Input.GetKey(KeyCode.Joystick1Button12) ||
           Input.GetKey(KeyCode.Joystick1Button13) ||
           Input.GetKey(KeyCode.Joystick1Button14) ||
           Input.GetKey(KeyCode.Joystick1Button15) ||
           Input.GetKey(KeyCode.Joystick1Button16) ||
           Input.GetKey(KeyCode.Joystick1Button17) ||
           Input.GetKey(KeyCode.Joystick1Button18) ||
           Input.GetKey(KeyCode.Joystick1Button19))
        {
            return true;
        }

        // joystick axis
        /*if (Input.GetAxis("cLeftStickX") != 0.0f ||
           Input.GetAxis("cLeftStickY") != 0.0f ||
           Input.GetAxis("cTriggers") != 0.0f ||
           Input.GetAxis("cRightStickX") != 0.0f ||
           Input.GetAxis("cRightStickY") != 0.0f)
        {
            return true;
        }*/
        return false;
    }

    private bool isTouche()
    {
        return false;
    }

    float GetHorizontalAxis()
    {
        return 0;
    }

    float GetVerticalAxis()
    {
        return 0;
    }

    public enum InputType
    {
        Keyboard,
        Controller,
        Touche
    }



}

public class ControlProfile
{
    public string name;
    public Controls.InputType InputType;

    public KeyCode Shoot1;
	public KeyCode Shoot2;
	public KeyCode Teleport;
	public KeyCode Inventory;
	public KeyCode Menu;

	public string MoveHorizontal;
	public string MoveVertical;

    public ControlProfile(
        string name = "Keyboard Default Profile", 
        Controls.InputType inputType = Controls.InputType.Keyboard,
        KeyCode shoot1 = KeyCode.J, 
        KeyCode shoot2 = KeyCode.K, 
        KeyCode teleport = KeyCode.Space, 
        KeyCode inventory = KeyCode.E, 
        KeyCode menu = KeyCode.Escape, 
        string moveHorizontal = "Horizontal", 
        string moveVertical = "Vertical")
    {
        this.name = name;
        this.InputType = inputType;
        this.Shoot1 = shoot1;
        this.Shoot2 = shoot2;
        this.Teleport = teleport;
        this.Inventory = inventory;
        this.Menu = menu;
        this.MoveHorizontal = moveHorizontal;
        this.MoveVertical = moveVertical;
    }

    public bool IsSelected()
    {
        if (
            Input.GetKey(Shoot1) ||
            Input.GetKey(Shoot2) ||
            Input.GetKey(Teleport) ||
            Input.GetKey(Inventory) ||
            Input.GetKey(Menu))
        {
            return true;
        }
        if (Input.GetAxisRaw(MoveHorizontal) != 0)
        {
            return true;
        }
        if (Input.GetAxisRaw(MoveVertical) != 0)
        {
            return true;
        }
        return false;
    }
}