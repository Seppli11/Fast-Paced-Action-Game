using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SaveStateEntry : MonoBehaviour, IPointerClickHandler {
	public static SaveStateEntry selectedSaveStateEntry = null;

	public SaveState saveState;

	public Image background;
	public Text saveStateName, lastSaved;

	

	// Use this for initialization
	void Start () {
		saveStateName.text = saveState.name;
		lastSaved.text = saveState.lastSaved;
	}
	
	// Update is called once per frame
	void Update () {
		if(selectedSaveStateEntry == this)
		{
			background.color = new Color(0.5f, 0.5f, 0.5f, 250);
		} else
		{
			background.color = new Color(1, 1, 1, 100);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		selectedSaveStateEntry = this;
	}
}
