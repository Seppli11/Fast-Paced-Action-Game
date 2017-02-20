using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    private InputType _currentInput;
    public InputType currentInput
    {
        get { return _currentInput; }
    }

    private float _horizontalAxis = 0f;
    public float horzontalAxis {
        get { return _horizontalAxis; } }

    private float _verticalAxis = 0f;
    public float verticalAxis
    {
        get { return _verticalAxis; }
    }

    private bool _shootButton1;
    public bool shootButton1{
        get { return _shootButton1; }
    }

    private bool _shootButton2;
    public bool shootButton2
    {
        get { return _shootButton2; }
    }

    private bool _teleportButton;
    public bool teleportButton
    {
        get { return _teleportButton; }
    }

    private bool _inventoryButton;
    public bool inventoryButton
    {
        get { return _inventoryButton; }
    }

    private bool _menuButton;
    public bool menuButton
    {
        get { return _menuButton; }
    }
	// Use this for initialization
	void Start () {
        _currentInput = InputType.KEYBOARD;
	}
	
	// Update is called once per frame
	void Update () {
        CheckInputType();
        switch(currentInput)
        {
            case InputType.KEYBOARD:
                //if(Input.GetButton)
                break;
            case InputType.CONTROLLER:
                break;
            case InputType.TOUCHE:
                break;
        }
	}

    private void CheckInputType()
    {
        //check if mouse or keyboard is the current input method
        if (isKeyboard()) _currentInput = InputType.KEYBOARD;
    }

    private bool isKeyboard()
    {
        return true;
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
        KEYBOARD,
        CONTROLLER,
        TOUCHE
    }
}
