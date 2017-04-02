

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Canvas))]
public class InventoryMenu : MonoBehaviour {
	public Camera blurCamera;
	private Camera changeBackToCamera;
	public Canvas canvas;

	public bool showInventory;
	// Use this for initialization
	void Start () {
		canvas.enabled = false;
		blurCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Controls.StaticControls.InventoryButton)
		{
			showInventory = !showInventory;
			if(showInventory)
			{
				canvas.enabled = true;
				blurCamera.enabled = true;
				blurCamera.transform.position = Camera.current.transform.position;
				blurCamera.CopyFrom(Camera.current);

				changeBackToCamera = Camera.current;
				Camera.current.enabled = false;
			} else
			{
				canvas.enabled = false;
				blurCamera.enabled = false;

				changeBackToCamera.enabled = true;
			}
		}
	}
}
