using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour {
	public ProgressBar progressBar;
	public Sprite weaponSprite
	{
		get { return image.sprite; }
		set { image.sprite = value; }
	}

	public Image image;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
