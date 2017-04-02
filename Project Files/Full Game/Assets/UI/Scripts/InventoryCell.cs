using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryCell : MonoBehaviour {
	public Item item;

	private Image image;

	public Sprite gridSprite, selectedGridSprite;

	public ContextMenu contextMenu;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointEnter() {
		Debug.Log("onPointerEnter");
		image.sprite = selectedGridSprite;
	}

	public void OnPointerExit()
	{
		Debug.Log("onPointerExit");
		image.sprite = gridSprite;
	}

	public void OnPointerClick(BaseEventData data)
	{
		PointerEventData eData = data as PointerEventData;
		if(eData.button == PointerEventData.InputButton.Right)
		{
			contextMenu.OpenContext();
		}
	}
}
