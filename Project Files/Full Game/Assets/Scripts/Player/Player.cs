using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public static GameObject player;
	// Use this for initialization
	private void Awake()
	{
		Player.player = gameObject;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
