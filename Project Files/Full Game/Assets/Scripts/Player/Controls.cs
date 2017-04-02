using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private static Controls _controls;


    public static Controls StaticControls
    {
        get { return _controls;}
    }

    public ControlProfile CurrentControlProfile = new ControlProfile("Hy");
    public ControlProfile KeyboardControlProfile = new ControlProfile("Keyboard Profile", InputType.Keyboard);
    public ControlProfile ControllerControlProfile = new ControlProfile("Controller Profile", InputType.Controller, KeyCode.JoystickButton4, KeyCode.JoystickButton5, KeyCode.JoystickButton0,
        KeyCode.Joystick1Button2, KeyCode.JoystickButton6, KeyCode.JoystickButton7);

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

    public Vector2 LastDirection = Vector2.up;
    public Vector2 CurrentDirection= Vector2.zero;

    private bool _shootButton1;
    public bool ShootButton1
    {
        get
        {
            if (_shootButton1)
            {
                _shootButton1 = false;
                return true;
            }
            return false;
        }
        set { _shootButton1 = value; }
    }

    private bool _shootButton2;
    public bool ShootButton2
    {
        get
        {
            if (_shootButton2)
            {
                _shootButton2 = false;
                return true;
            }
            return false;
        }
        set { _shootButton2 = value; }
    }

    private bool _teleportButton;
    public bool TeleportButton
    {
        get
        {
            if (_teleportButton)
            {
                _teleportButton = false;
                return true;
            }
            return false;
        }
    }

	private bool _rollButton;
	public bool RollButton
	{
		get
		{
			if (_rollButton)
			{
				_rollButton = false;
				return true;
			}
			return false;
		}
	}

	private bool _inventoryButton;
    public bool InventoryButton
    {
        get { return _inventoryButton; }
    }

    private bool _menuButton;

    public bool MenuButton
    {
        get {
            if (_menuButton)
            {
                _menuButton = false;
                return true;
            }
            return false;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    void Awake()
    {
        _controls = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputType();
        switch (CurrentInput)
        {
            case InputType.Keyboard:
            case InputType.Controller:

                if(Input.GetKeyDown(CurrentControlProfile.Shoot1))
			    {
			        _shootButton1 = true;
			    } else if (Input.GetKeyUp(CurrentControlProfile.Shoot1))
			    {
			        _shootButton1 = false;
                }

                if (Input.GetKeyDown(CurrentControlProfile.Shoot2))
                {
                    _shootButton2 = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Shoot2))
                {
                    _shootButton2 = false;
                }

                if (Input.GetKeyDown(CurrentControlProfile.Teleport))
                {
                    _teleportButton = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Teleport))
                {
                    _teleportButton = false;
                }

				if (Input.GetKeyDown(CurrentControlProfile.Roll))
				{
					_rollButton = true;
				}
				else if (Input.GetKeyUp(CurrentControlProfile.Roll))
				{
					_rollButton = false;
				}

				if (Input.GetKeyDown(CurrentControlProfile.Inventory))
                {
                    _inventoryButton = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Inventory))
                {
                    _inventoryButton = false;
                }

                if (Input.GetKeyDown(CurrentControlProfile.Menu))
                {
                    _menuButton = true;
                }
                else if (Input.GetKeyUp(CurrentControlProfile.Menu))
                {
                    _menuButton = false;
                }

                _horizontalAxis = Input.GetAxisRaw(CurrentControlProfile.MoveHorizontal);    
                _verticalAxis = Input.GetAxisRaw(CurrentControlProfile.MoveVertical);

                CurrentDirection = new Vector2(_horizontalAxis, _verticalAxis).normalized;

                if (HorizontalAxis != 0 | VerticalAxis != 0) //TODO implement DeadZones to this if-statement
                {
                    LastDirection.x = _horizontalAxis;
                    LastDirection.y = -_verticalAxis;

                    LastDirection = LastDirection.normalized;
                }

                break;
            case InputType.Touche:
                break;
        }
    }

    private void resetInput()
    {
        _horizontalAxis = 0;
        _verticalAxis = 0;
        _teleportButton = false;
        _shootButton1 = false;
        _shootButton2 = false;
    }

    private void CheckInputType()
    {
        if (CurrentControlProfile.IsSelected()) return;

        if (KeyboardControlProfile.IsSelected())
        {
            CurrentControlProfile = KeyboardControlProfile;
            resetInput();
        }
        else if (ControllerControlProfile.IsSelected())
        {
            CurrentControlProfile = ControllerControlProfile;
            resetInput();
        }
        else
        {
            return;
        }
        Debug.Log("Change ControlProfile to " + CurrentControlProfile.name);
    }

    public enum InputType
    {
        Keyboard,
        Controller,
        Touche
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
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
	public KeyCode Roll;
	public KeyCode Menu;

	public string MoveHorizontal;
	public string MoveVertical;

    public ControlProfile(
        string name = "Keyboard Default Profile", 
        Controls.InputType inputType = Controls.InputType.Keyboard,
        KeyCode shoot1 = KeyCode.J, 
        KeyCode shoot2 = KeyCode.K, 
        KeyCode teleport = KeyCode.Space, 
		KeyCode roll = KeyCode.LeftShift,
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
		this.Roll = roll;
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
			Input.GetKey(Roll) ||
			Input.GetKey(Inventory) ||
            Input.GetKey(Menu))
        {
            return true;
        }
        return false;
    }
}