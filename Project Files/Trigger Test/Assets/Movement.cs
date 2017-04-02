using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	private Rigidbody2D rigidbody2d;
	// Use this for initialization
	void Start () {
		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 moveDirection = Vector2.zero;
		if(Input.GetKey("w"))
		{
			moveDirection = Vector2.up;
		} else if(Input.GetKey("s"))
		{
			moveDirection = Vector2.down;
		}

		if (Input.GetKey("a"))
		{
			moveDirection = Vector2.left;
		}
		else if (Input.GetKey("d"))
		{
			moveDirection = Vector2.right;
		}

		moveDirection = moveDirection.normalized * 1500 * Time.deltaTime;
		rigidbody2d.velocity = moveDirection;
	}

}
