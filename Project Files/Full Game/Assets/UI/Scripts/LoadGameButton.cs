using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadGameButton : MonoBehaviour {
	private Button button;
	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		button.interactable = SaveStateEntry.selectedSaveStateEntry != null;
	}
}
