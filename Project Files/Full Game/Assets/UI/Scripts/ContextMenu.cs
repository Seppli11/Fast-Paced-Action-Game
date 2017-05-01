using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// handels the contextmenu, also known as the hoverText.
/// </summary>
public class ContextMenu : MonoBehaviour {
	public static ContextMenu contextMenu;

	private RectTransform rectTransform;
	public GameObject thisGameObject;

	private bool _visible = false;
	/// <summary>
	/// Shows or Closes the contextmenu/hovertext.
	/// </summary>
	public bool visible {
		get { return _visible; }
		set
		{
			_visible = value;
			thisGameObject.SetActive(visible);
		}
	}

	public Text hoverText;

	private void Awake()
	{
		contextMenu = this;
	}

	void Start () {
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(visible)
		{
			rectTransform.position = Input.mousePosition; //updates the poistion of the hovertext/contextmenu to the mouse position
		}
	}
}
