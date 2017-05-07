using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Animator))]
public class Movement : MonoBehaviour {
	public string animatorWalkingBool = "walking";
	public string animatorWalkingX = "x";
	public string animatorWalkingY = "y";

	public bool rotationCopiesLastDirection = true;
	private Vector2 _rotation;
	public Vector2 rotation
	{
		get
		{
			if (rotationCopiesLastDirection) return lastDirection;
			return _rotation;
		}
		set
		{
			_rotation = value;
		}
	}
	
	public Vector2 velocity;
	public float multiplier = 1;

	private Vector2 _lastDirection;
	public Vector2 lastDirection
	{
		get { return _lastDirection; }
	}

	public bool lockMovement = false;

	//public BoxCollider2D colider;
	private Rigidbody2D rigidbody2d;
	private Animator animator;
	// Use this for initialization
	void Start () {
		rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//sets the walk bool
		if (lockMovement) return;
		if(rigidbody2d.velocity != Vector2.zero)
		{
			_lastDirection = velocity.normalized;
			if(animator.GetBool(animatorWalkingBool) == false)
				animator.SetBool(animatorWalkingBool, true);
		} else
		{
			if (animator.GetBool(animatorWalkingBool) == true)
				animator.SetBool(animatorWalkingBool, false);
		}

		//sets the x and y var of the animator
		/*if(GetComponent<Player>() == null)
			Debug.Log("LastDirection: " + lastDirection + ", rotation: " + _rotation);*/
		velocity *= multiplier;
		rigidbody2d.velocity = velocity;
		animator.SetFloat(animatorWalkingX, rotation.x);
		animator.SetFloat(animatorWalkingY, rotation.y);
	}
}
