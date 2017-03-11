using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private TimerManager timerManager;

    private Animator animator;

    private Controls controls;
    public float Speed;
    public float TeleportSpeed;

    public float NonTeleportTime;
    public float TeleportTime;

    private bool cannotTeleport = false;
    private bool currentlyTeleporting = false;

    private SpriteRenderer spriteRenderer;

    private Sprite lastSprite = null;
    private Vector3 lastPosition;
    public GameObject PlayerShadow;

    private Rigidbody2D rigidbody;
	void Start ()
	{
        timerManager = TimerManager.STimerManager;;
	    controls = Controls.StaticControls;
        animator = GetComponent<Animator>();
	    spriteRenderer = GetComponent<SpriteRenderer>();
	    rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        /*Vector2 direction = Vector2.zero;
	    if (controls.HorizontalAxis > 0)
	    {
	        //transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime, transform.position.y);
            //rigidbody.MovePosition(rigidbody.position + new Vector2(2, 0));
	        direction.x = 1;

	    } else if (controls.HorizontalAxis < 0)
	    {
            //transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, transform.position.y);
            direction.x = -1;
        }
        else
        {
            
        }

        if (controls.VerticalAxis > 0)
        {
            direction.y = -1;
        }
        else if (controls.VerticalAxis < 0)
        {
            direction.y = 1;
        } else
        {
        }
        */

	   /* if (controls.ShootButton1)
	    {
	        animator.SetBool("walking with a sword", true);
	    }
	    else
	    {
            animator.SetBool("walking with a sword", false);
        }*/

	    if (controls.TeleportButton)
	    {
            if(cannotTeleport) return;
	        //transform.position += new Vector3(controls.LastDirection.x*TeleportSpeed, controls.LastDirection.y * TeleportSpeed);
	        currentlyTeleporting = true;
	        cannotTeleport = true;
            timerManager.CreateTimer(TeleportTime, timer => currentlyTeleporting = false);
	        lastSprite = GetComponent<SpriteRenderer>().sprite;
	        lastPosition = transform.position;
	        timerManager.CreateTimer(0.1f, timer =>
	        {
	           /* GameObject shadow = Instantiate(PlayerShadow);
	            shadow.transform.position = lastPosition;
	            shadow.GetComponent<SpriteRenderer>().sprite = lastSprite;
	            timerManager.CreateTimer(0.3f, timer1 => Destroy(shadow));*/

	            lastSprite = spriteRenderer.sprite;
	            lastPosition = transform.position;

                Debug.Log(timer.RepeatedCount + "/" + timer.TimesToRepeat);
                if(!currentlyTeleporting) timer.Stop();
	        }, true);
	        timerManager.CreateTimer(NonTeleportTime, timer => {
                cannotTeleport = false;
	        });
	    }
	    if (currentlyTeleporting)
	    {
            rigidbody.velocity = new Vector2(controls.LastDirection.x*TeleportSpeed*Time.deltaTime, controls.LastDirection.y*TeleportSpeed * Time.deltaTime);
	    }
	    else
	    {
	        //direction = direction.normalized;
	        Vector2 direction = controls.CurrentDirection * Speed * Time.deltaTime;
	        rigidbody.velocity = new Vector2(Mathf.Lerp(0, direction.x, 0.8f), Mathf.Lerp(0, direction.y, 0.8f));

            if (direction == Vector2.zero)
            {
                animator.SetBool("walking", false);
            }
            else
            {
                animator.SetBool("walking", true);
                animator.SetFloat("x", direction.x);
                animator.SetFloat("y", direction.y);
            }
           
        }
	}
}
