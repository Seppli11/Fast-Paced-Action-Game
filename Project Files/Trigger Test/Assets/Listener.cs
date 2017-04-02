using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("EnterTrigger 2");
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		Debug.Log("TriggerStay 2");
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("TriggerExit 2");
	}
}
