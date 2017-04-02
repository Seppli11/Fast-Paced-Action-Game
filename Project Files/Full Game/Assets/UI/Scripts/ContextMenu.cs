using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]
public class ContextMenu : MonoBehaviour {
	// Use this for initialization
	public RectTransform transformRec;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenContext()
	{
		Debug.Log(PlayerCamera.currentCamera + ", " + Input.mousePosition);
		gameObject.SetActive(true);
		transformRec.position = Input.mousePosition;
	}
}
