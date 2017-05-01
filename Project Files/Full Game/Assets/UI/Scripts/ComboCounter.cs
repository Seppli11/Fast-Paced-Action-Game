using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ComboCounter : MonoBehaviour
{
	public static ComboCounter comboCounter
	{
		get;
		private set;
	}

	public int combo;
	private float lastHit;
	public float comboExpieringTime;

	private Text text;

	private void Awake()
	{
		comboCounter = this;
	}

	// Use this for initialization
	void Start ()
	{
		text = GetComponent<Text>();
		text.text = "0x";
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastHit > comboExpieringTime)
		{
			combo = 0;
			text.text = combo.ToString() + "x";
		}
	}

	public void RegisterHit()
	{
		combo++;
		lastHit = Time.time;
		text.text = combo.ToString() + "x";
	}
}
