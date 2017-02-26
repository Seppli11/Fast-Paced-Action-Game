using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private TimerManager timerManager = new TimerManager();

    private Controls controls;
    public float Speed;
    public float TeleportSpeed;

    public float NonTeleportTime;

    private bool cannotTeleport = false;
	void Start ()
	{
	    controls = Controls.StaticControls;
	}
	
	void Update () {
        timerManager.Update();

	    if (controls.HorizontalAxis > 0)
	    {
	        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y);
	    } else if (controls.HorizontalAxis < 0)
	    {
            transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y);
        }

        if (controls.VerticalAxis > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Speed * Time.deltaTime);
        }
        else if (controls.VerticalAxis < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Speed * Time.deltaTime);
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
