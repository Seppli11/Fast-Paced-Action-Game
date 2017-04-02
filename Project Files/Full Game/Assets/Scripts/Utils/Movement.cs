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

	private Vector2 _lastDirection;
	public Vector2 lastDirection
	{
		get { return _lastDirection; }
	}


	private Rigidbody2D rigidbody;
	private Animator animator;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(rigidbody.velocity != Vector2.zero)
		{
			_lastDirection = rigidbody.velocity.normalized;
			if(animator.GetBool(animatorWalkingBool) == false)
				animator.SetBool(animatorWalkingBool, true);
		} else
		{
			if (animator.GetBool(animatorWalkingBool) == true)
				animator.SetBool(animatorWalkingBool, false);
			
		}
		
		rigidbody.velocity = velocity;
		animator.SetFloat(animatorWalkingX, lastDirection.x);
		animator.SetFloat(animatorWalkingY, lastDirection.y);
	}

	
}
