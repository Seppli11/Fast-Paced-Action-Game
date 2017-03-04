using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private TimerManager timerManager = new TimerManager();

    private Animator animator;

    private Controls controls;
    public float Speed;
    public float TeleportSpeed;

    public float NonTeleportTime;

    private bool cannotTeleport = false;
	void Start ()
	{
	    controls = Controls.StaticControls;
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        timerManager.Update();

	    if (controls.HorizontalAxis > 0)
	    {
	        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y);
            animator.SetInteger("x", 1);

	    } else if (controls.HorizontalAxis < 0)
	    {
            transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y);
            animator.SetInteger("x", -1);
        }
        else
        {
            animator.SetInteger("x", 0);
        }

        if (controls.VerticalAxis > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Speed * Time.deltaTime);
            animator.SetInteger("y", 1);
        }
        else if (controls.VerticalAxis < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime);
            animator.SetInteger("y", -1);
        } else
        {
            animator.SetInteger("y", 0);
        }

	    if (controls.TeleportButton)
	    {
            if(cannotTeleport) return;
	        transform.position += new Vector3(controls.LastDirection.x*TeleportSpeed, controls.LastDirection.y * TeleportSpeed);
	        cannotTeleport = true;
	        timerManager.CreateTimer(NonTeleportTime, timer => cannotTeleport = false);
	    }
        
    }
}
