using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour {
	public Sprite defaultSprite;
	public ProgressBar progressBar;
	public Sprite weaponSprite
	{
		get { return image.sprite; }
		set {
			image.sprite = value;
			if (value == null)
				image.sprite = defaultSprite;
		}
	}

	public Image image;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
