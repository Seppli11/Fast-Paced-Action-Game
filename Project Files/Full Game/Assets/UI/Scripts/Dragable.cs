using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region drag&drop variables
	public static GameObject currentlyDraging;
	private Vector2 startDragPosition;
	private Transform startParent;
	private Transform parentOfParent;
	#endregion

	public Item item;

	private void Start()
	{
		//sets the parentOfparent variable.
		//I know, its a huge MESS!
		if (transform.parent != null)
			if (transform.parent.parent != null)
				if (transform.parent.parent.parent)
					parentOfParent = transform.parent.parent.parent;
				else parentOfParent = transform.parent.parent;
			else parentOfParent = transform.parent;
		else parentOfParent = transform;
	}

	#region Drag and Drop Handlers
	public void OnBeginDrag(PointerEventData eventData)
	{
		currentlyDraging = gameObject;
		startDragPosition = transform.position;
		startParent = transform.parent;
		transform.SetParent(parentOfParent);
		startParent.GetComponent<InventoryCell>().item = null;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		currentlyDraging = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent == parentOfParent)
		{
			transform.position = startDragPosition;
			transform.SetParent(startParent);
			startParent.GetComponent<InventoryCell>().item = item;
		}
	}
	#endregion
}
